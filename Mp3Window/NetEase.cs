using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lsj.Util.Collections;
using Lsj.Util.Net.Web;
using System.Net;
using Lsj.Util.JSON;
using System.Security.Cryptography;
using Lsj.Util.Text;
using System.Numerics;
using System.Globalization;

namespace NeteaseCloud
{
    public class NetEase
    {
        private readonly WebHttpClient webClient;
        private readonly SafeDictionary<string, string> header;

        public NetEase()
        {
            this.webClient = new WebHttpClient();
            this.webClient.AddCookie(new Cookie { Name = "appver", Value = "1.5.2", Domain = ".163.com" });
            this.webClient.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.152 Safari/537.36";
            this.header = new SafeDictionary<string, string>
            {
               { "Accept","*/*" },
               { "Accept-Encoding","" },
               { "Accept-Language","zh-CN,zh;q=0.8,gl;q=0.6,zh-TW;q=0.4" },
               { "Referer","http://music.163.com/search/" },
            };
        }

        /// <summary>
        /// 搜索音乐
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public dynamic Search(string str)
        {
            try
            {
                var data = $@"s={str}&type=1&offset=0&total=true&limit=10";
                var result = webClient.Post("http://music.163.com/api/search/get", data.ConvertToBytes(Encoding.UTF8), "application/x-www-form-urlencoded", this.header).ConvertFromBytes(Encoding.UTF8);
                return JSONParser.Parse(result);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取音乐详细信息
        /// </summary>
        /// <param name="id">音乐ID</param>
        /// <returns></returns>
        public dynamic GetMusicDetail(int id)
        {
            try
            {
                var csrf = this.webClient.GetCookie(new Uri("http://music.163.com")).OfType<Cookie>().Where(c => c.Name == "__csrf").FirstOrDefault();
                var text = new
                {
                    csrf_token = csrf,
                    ids = new int[] { id },
                    br = 320000
                };
                var jsonStr = JSONConverter.ConvertToJSONString(text);
                var x = EncryptRequest(jsonStr);
                var result = webClient.Post($"http://music.163.com/weapi/song/enhance/player/url?csrf_token={csrf}", x.ConvertToBytes(Encoding.UTF8), "application/x-www-form-urlencoded", this.header).ConvertFromBytes(Encoding.UTF8);
                return JSONParser.Parse(result);
            }
            catch
            {
                return null;
            }
        }


        private string EncryptRequest(string jsonStr)
        {
            var secKey = "be88887d941ea4b7";
            var nonce = "0CoJUm6Qyw8W8jud";
            var encText = AesEncrypt(AesEncrypt(jsonStr, nonce), secKey);
            var encSecKey = RsaEncrypt(secKey);
            encText = encText.Replace("+", "%2B");
            return $"params={encText}&encSecKey={encSecKey}";
        }
        private string AesEncrypt(string text, string secKey)
        {
            var pad = 16 - text.Length % 16;
            for (int i = 0; i < pad; i++)
            {
                text = text + (char)pad;
            }
            var len = text.Length;
            using (var aes = Aes.Create())
            {
                aes.Key = secKey.ConvertToBytes();
                aes.Mode = CipherMode.CBC;
                aes.IV = "0102030405060708".ConvertToBytes();
                aes.Padding = PaddingMode.None;
                using (var encryptor = aes.CreateEncryptor())
                {
                    var buffer = text.ConvertToBytes();
                    var result = encryptor.TransformFinalBlock(buffer, 0, buffer.Length);
                    return Convert.ToBase64String(result);
                }

            }
        }
        private string RsaEncrypt(string text)
        {
            var modulus = "00e0b509f6259df8642dbc35662901477df22677ec152b5ff68ace615bb7b725152b3ab17a876aea8a5aa76d2e417629ec4ee341f56135fccf695280104e0312ecbda92557c93870114af6c9d05c4f7f0c3685b7a46bee255932575cce10b424d813cfe4875d3e82047b97ddef52741d546b8e289dc6935b3ece0462db0a22b8e7";
            text = new String(text.Reverse().ToArray());
            var sb = new StringBuilder();
            foreach (var x in text)
            {
                sb.Append(Convert.ToString(x, 16));
            }
            var temp = BigInteger.ModPow(BigInteger.Parse(sb.ToString(), NumberStyles.AllowHexSpecifier), 65537, BigInteger.Parse(modulus, NumberStyles.AllowHexSpecifier)).ToString("x");
            while (temp.StartsWith("0"))
            {
                temp = temp.Remove(0, 1);
            }
            return temp.PadLeft(256);
        }
    }
}

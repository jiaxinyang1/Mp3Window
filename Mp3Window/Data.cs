using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Lsj.Util.JSON;
using Microsoft.Win32;

namespace Mp3Window
{
    public class Data
    {


        //所有歌单信息
        public static List<MusicList> MusicLists = new List<MusicList>();
        //要显示的是哪一个歌单
          public   static string selectName { set; get; }
        /// <summary>
        /// 保存到json文件
        /// </summary>
        public static void SaveData()
            {
                string text = JSONConverter.ConvertToJSONString(MusicLists);
                Save(ref text, @"data\MusicListView.json");
            }
        /// <summary>
        /// 从json中读入数据
        /// </summary>
        public static void ReadData()
            {
                string text = "";
                Read(ref text, @"data\MusicListView.json");
                MusicLists = JSONParser.Parse<List<MusicList>>(text);
            }
        /// <summary>
        ///  按名称找到被选中的歌单
        /// </summary>
        /// <returns></returns>
        public static MusicList FindElectMusicList()
        {
            foreach (var musicList in MusicLists)
            {
                if (musicList.Name.Equals(selectName))
                {
                    return musicList;
                }
            }
            return null;
        }
        /// <summary>
        ///  按名称找到被选中的歌单
        /// </summary>
        /// <returns></returns>
        public static MusicList FindElectMusicList(string name)
        {
            foreach (var musicList in MusicLists)
            {
                if (musicList.Name.Equals(name))
                {
                    return musicList;
                }
            }
            return null;
        }
        //一些异常处理待做
        public static void Save(ref string text,string path)
        {
            StreamWriter sw = File.CreateText(path);
            sw.Write(text);
            sw.Close();
        }

        public static void Read(ref string   text, string path)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            StreamReader sr = File.OpenText(path);
            text = sr.ReadToEnd();
            sr.Close();
        }
    }

 
}

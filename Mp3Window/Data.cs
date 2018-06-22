using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Lsj.Util.JSON;
using Microsoft.Win32;
using NeteaseCloud;

namespace Mp3Window
{
    //歌单名字
    public class ListName 
    {
        public string Name { get; set; }
        public ListName()    {   }
        public ListName(string name)
        {
            Name = name;
        }
    }
    public class Data
    {
        //网络交互
        public static NetEase net;
        //被选中的歌曲
        public static Music SelcetMusic { get; set; }
        //正在播放的歌曲名称
        public static String PlayingSong;
        //存放歌单名字的列表
        public static ObservableCollection<ListName> MusicListName;
        //所有歌单信息
        public static List<MusicList> MusicLists = new List<MusicList>();
        //要显示的是哪一个歌单
        public   static string selectName { set; get; }

        /// <summary>
        /// 保存所有歌单信息到json文件
        /// </summary>
        public static void SaveData()
        {
                string text = JSONConverter.ConvertToJSONString(MusicLists);
                Save(ref text, @"data\MusicListView.json");
        }
        /// <summary>
        /// 从json中读入所有歌单信息
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
        /// 左边导航栏歌单名字列表读入
        /// </summary>
        public static void ReadName()
        {
            string text = "";
            Data.Read(ref text, @"data\LeftList.json");
            MusicListName = JSONParser.Parse<ObservableCollection<ListName>>(text);
        }
        /// <summary>
        /// 左边导航栏歌单名字列表保存
        /// </summary>
        public static void SaveName()
        {
            string text = JSONConverter.ConvertToJSONString(MusicListName);
            Data.Save(ref text, @"data\LeftList.json");

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
        /// <summary>
        /// 存储
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void Save(ref string text,string path)
        {
            try
            {
                var sw = File.CreateText(path);
                sw.Write(text);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        
        }
        /// <summary>
        /// 读入
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public static void Read(ref string   text, string path)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            try
            {
                var sr = File.OpenText(path);
                text = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
      
        }
    }

 
}

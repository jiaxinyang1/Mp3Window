﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Mp3Window
{
    public class Music
    {
        public Music()
        {
        }
        //构造函数
        public Music(string songName, string singer, string time, string url)
        {
            SongName = songName;
            Singer = singer;
            Time = time;
            Url = url;
        }
        public string SongName { get; set; }//歌曲名称
        public  string Singer { get; set; }//歌手
        public  string Time { get; set; }//歌曲时长
        public string Url { get; set; }// 歌曲路径

        //else...

    }
    public class MusicList:ICloneable
    {
      

        public string Author { get; set; }//创建者

        public string Time { get; set; }//创建时间

        public string Tag { get; set; } //标签

        public string Brief { get; set; }//简介

        public string Name { get; set; }//歌单名称

        public string CoverUrl { get; set; }//封面路径

        public ObservableCollection<Music> Songs { get; set; }//歌曲

        /// <summary>
        /// 一些初始化工作
        /// </summary>
        public MusicList()
        {
            Songs = new ObservableCollection<Music>();
        }
        //构造函数
        public MusicList(string author, string time, string tag, string brief, string name, string coverUrl) : this()
        {

            Author = author;
            Time = time;
            Tag = tag;
            Brief = brief;
            Name = name;
            CoverUrl = coverUrl;
        }
        /// <summary>
        /// 设置歌单名称
        /// </summary>
        /// <param name="label"></param>
        public void SetName(Label label)   {  label.Content = Name;  }
        /// <summary>
        /// 设置作者名称
        /// </summary>
        /// <param name="label"></param>
        public void SetAuthor(Label label) { label.Content = Author; }
        /// <summary>
        /// 设置创建时间，选择由系统时间指定还是用户自定
        /// </summary>
        /// <param name="label"></param>
        public void SetTime(Label label, bool auto=true)   {  label.Content = auto ? DateTime.Now.ToString() : Time;  }
        /// <summary>
        /// 设置标签内容
        /// </summary>
        /// <param name="label"></param>
        public void SetTag(Label label)  {  label.Content += Tag;  }
        /// <summary>
        /// 设置简介内容
        /// </summary>
        /// <param name="label"></param>
        public void SetBrief(Label label)  {  label.Content += Brief;  }

        /// <summary>
        /// 设置封面路径
        /// </summary>
        /// <param name="url"></param>
        public void SetCoverUrl(BitmapImage url)
        {
            Uri bUri = new Uri(CoverUrl);
            url.UriSource = bUri;     

        }
        /// <summary>
        /// 把歌曲信息添加到ListView中
        /// </summary>
        /// <param name="listView"></param>
        public void AddMusic(ListView listView)
        {
            listView.ItemsSource = Songs;
        }

        public object Clone()
        {
           return  new MusicList(this.Author,this.Time,this.Tag,this.Brief,this.Name,this.CoverUrl);
        }
    }
}

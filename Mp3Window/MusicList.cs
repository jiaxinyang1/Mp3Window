using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mp3Window
{
    public class MusicList
    {
        public string Author { get; set; }//创建者

        public string Time { get; set; }//创建时间

        public string Tag { get; set; } //标签

        public string Brief { get; set; }//简介

        public string Name { get; set; }//歌单名称

        public string CoverUrl { get; set; }//封面路径

        /// <summary>
        /// 设置歌单名称
        /// </summary>
        /// <param name="label"></param>
        public void SetName(Label label)
        {
            label.Content = Name;
        }
        /// <summary>
        /// 设置作者名称
        /// </summary>
        /// <param name="label"></param>
        public void SetAuthor(Label label)
        {
            label.Content = Author;
        }
    }
}

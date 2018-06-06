using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lsj.Util;
using Lsj.Util.JSON;

namespace Mp3Window
{
    /// <summary>
    /// MusicList.xaml 的交互逻辑
    /// </summary>
    public partial class MusicListPage : Page
    {
        //所有歌单信息
          List<MusicList>  MusicLists =new List<MusicList>();
     
        //指定父窗口的指针
        public MainWindow ParentWindow { set; get; }

        public string selectName { set; get; }
        //被选中的歌曲
        private Music selcetMusic;

        //正在播放的歌曲
        private Music song;
        public MusicListPage()
        {
            InitializeComponent();

 
            
        }


        /// <summary>
        /// 初始化相关
        /// </summary>
        public void Init()
        {
            Console.WriteLine(selectName);
            MusicList one= new MusicList(
                "Hakurei",
                null,
                "没有标签",
                "这个人很懒，什么都没有写",
                "【东方纯音乐】春至之日，萬花盛放",
                @"image\test.png");
            one.Songs.Add(new Music(
                "DAOKO - 打上花火",
                "米津玄師",
                "04:49",
                @"music\DAOKO - 打上花火.mp3"));
            MusicLists.Add(one);
        /*    string text = JSONConverter.ConvertToJSONString(MusicLists);
            Data.Save(ref text, @"data\MusicListView.json");*/
   
            //先根据json把歌单信息转成class 未做

            /***********************************************/
            //查找到被选中的歌单
            
            MusicList selectList = FindElectMusicList();

               //设置相关信息
               selectList.SetName(ListName);

               selectList.SetAuthor(Creator);
               selectList.SetTime(CreatTime);

               selectList.SetTag(Tag);
               selectList.SetBrief(Brief);

           /*    selectList.SetCoverUrl(CoverImage);
               selectList.SetCoverUrl(BackImage);
               selectList.SetCoverUrl(CreatorImage);*/
               //添加歌曲
               selectList.AddMusic(MusicListView);
       
        }
            
    

        /// <summary>
        ///  按名称找到被选中的歌单
        /// </summary>
        /// <returns></returns>
        public MusicList FindElectMusicList()
        {

            
            foreach (var musicList in MusicLists )
            {
                if (musicList.Name.Equals(selectName))
                {
                    return musicList;
                }
            }

            return null;
        }
      
    }
}


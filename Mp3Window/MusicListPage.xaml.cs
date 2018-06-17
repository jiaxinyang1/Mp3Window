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

        public MusicList selectList { get; set; }
        //被选中的歌曲
        public Music SelcetMusic { get; set; }

        //正在播放的歌曲
        public String PlayingSong;

        public MusicListPage()
        {
            InitializeComponent();
            PlayingSong = "1";
        }
        /// <summary>
        /// 初始化相关
        /// </summary>
        public void Init()
        {
   
            ReadData();
            
            //查找到被选中的歌单
             selectList = FindElectMusicList();
            //设置页面信息
  
            selectList.SetName(ListName);

            selectList.SetAuthor(Creator);
            selectList.SetTime(CreatTime);

            selectList.SetTag(Tag);
            selectList.SetBrief(Brief);

            //不能工作，待解决
          /*  selectList.SetCoverUrl(CoverImage);
            selectList.SetCoverUrl(BackImage);
            selectList.SetCoverUrl(CreatorImage);*/

          selectList.AddMusic(MusicListView); 



        }
        /// <summary>
        /// 播放
        /// </summary>
        public void PlaySong()
        {
            var  Play = new MusicPlay();
            if (SelcetMusic.SongName!= PlayingSong)
            {
                PlayingSong= SelcetMusic.SongName;
                Play.Play(SelcetMusic.Url);
            }
        }
          
        /// <summary>
        /// 返回给父窗口自己
        /// </summary>
        /// <returns></returns>
        public MusicListPage ReturnPage()
        {
            return this;

        }
        /// <summary>
        /// 歌单中添加歌曲
        /// </summary>
        /// <param name="item"></param>
        public void AddSong(Music item)
        {
           selectList.Songs.Add(item);
            SaveData();//保存一下数据
        }
        /// <summary>
        /// 添加歌单信息
        /// </summary>
        /// <param name="item"></param>
        public void AddMusicList(MusicList  item)
        {
            MusicLists.Add(item);
            SaveData();//保存一下数据
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

        /// <summary>
        /// 保存到json文件
        /// </summary>
        public void SaveData()
        {
            string text = JSONConverter.ConvertToJSONString(MusicLists);
            Data.Save(ref text, @"data\MusicListView.json");
        }
        /// <summary>
        /// 从json中读入数据
        /// </summary>
        public void ReadData()
        {
            string text = "";
            Data.Read(ref text, @"data\MusicListView.json");
            MusicLists = JSONParser.Parse<List<MusicList>>(text);
        }
        /// <summary>
        /// 选择项改变时候更改一些信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SelcetMusic = (MusicListView.SelectedItem as Music);

            ParentWindow.SongName.Content = (MusicListView.SelectedItem as Music)?.SongName;
            ParentWindow.SongTime.Content = (MusicListView.SelectedItem as Music)?.Time;
        }
        /// <summary>
        /// 右键菜单添加歌曲到歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenu_Clicked(object sender, RoutedEventArgs e)
        {
            //需要弹出一窗口来选择需要添加的歌单
            Window choseWindow =new ChooseListWindow();
            (choseWindow as ChooseListWindow)._listNames = ParentWindow.MusicListName;
            choseWindow.Show();
        }
    }
}


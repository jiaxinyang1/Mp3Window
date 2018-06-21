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

        //指定父窗口的指针
        public MainWindow ParentWindow { set; get; }
        //在所有歌单信息中找到需要被显示的那一个歌单
        public MusicList selectList { get; set; }


        public MusicListPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初始化相关
        /// </summary>
        public void Init()
        {
         
      
            //得到需要显示的页面名字
            Data.selectName=(ParentWindow.MusicListListView.SelectedItem as ListName)?.Name;
            //查找到被选中的歌单
            selectList = Data.FindElectMusicList();
            //设置页面信息
  
            selectList.SetName(ListName);

            selectList.SetAuthor(Creator);
            selectList.SetTime(CreatTime);

            selectList.SetTag(Mark);
            selectList.SetBrief(Brief);

            //不能工作，待解决
            /*  selectList.SetCoverUrl(CoverImage);
              selectList.SetCoverUrl(BackImage);
              selectList.SetCoverUrl(CreatorImage);*/
           
            selectList.AddMusic(MusicListView); 
        }

        /// <summary>
        /// 歌单中添加歌曲
        /// </summary>
        /// <param name="item"></param>
        public void AddSong(Music item)
        {
            selectList.Songs.Add(item);
            Data.SaveData();//保存一下数据
        }
        /// <summary>
        /// 添加歌单信息
        /// </summary>
        /// <param name="item"></param>
        public void AddMusicList(MusicList  item)
        {
            Data.MusicLists.Add(item);
            Data.SaveData();//保存一下数据
        }
    
    
        /// <summary>
        /// 选择项改变时候更改一些信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            Data.SelcetMusic = (MusicListView.SelectedItem as Music);
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
            (choseWindow as ChooseListWindow)._listNames = Data.MusicListName ;
            (choseWindow as ChooseListWindow).song=MusicListView.SelectedItem as Music;
            choseWindow.Show();
        }
    }
}


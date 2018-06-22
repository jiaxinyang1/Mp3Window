using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using NeteaseCloud;

namespace Mp3Window
{
    /// <summary>
    /// Search.xaml 的交互逻辑
    /// </summary>
    public partial class Search : Page
    {
        public List<Music> SongsList;
        
      
 
        public Search()
        {
            InitializeComponent();
       
        }
        /// <summary>
        /// 搜索按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SongsList = new List<Music>();
            dynamic songSearch =  Data.net.Search(searchText.Text);
            foreach (var song in songSearch.result.songs)
            {
                
                int id=(int)song.id;
                string songName = song.name;
                string songArtist="";
                string url="";
                string album="null";
                foreach (var artist in song.artists)
                {
                    songArtist +=artist.name;
                    songArtist += "/";
                }

              dynamic songDetail  = Data.net.GetMusicDetail(id);
                if (songDetail!=null)
                {
                    foreach (var detail in songDetail.data)
                    {
                        url = detail.url;
                    }
                }

                album = song.album.name;
                if(url!=null)
                    SongsList.Add(new Music(songName,songArtist, album, id.ToString()));
            }

            listViewSearch.ItemsSource = SongsList;

        }

        private void ContextMenu_Clicked(object sender, RoutedEventArgs e)
        {
            //需要弹出一窗口来选择需要添加的歌单
            Window choseWindow = new ChooseListWindow();
            (choseWindow as ChooseListWindow)._listNames = Data.MusicListName;
            (choseWindow as ChooseListWindow).song = listViewSearch.SelectedItem as Music;
            choseWindow.Show();
        }

  
         
   
    }
}

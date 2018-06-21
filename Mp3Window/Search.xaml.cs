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
        public List<Music> SongsList=new List<Music>();
        System.Windows.Media.MediaPlayer media = new MediaPlayer();
        private NetEase net=new NetEase();
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
         dynamic songSearch =  net.Search(searchText.Text);          
            foreach (var song in songSearch.result.songs)
            {
                int id=(int)song.id;
                string songName = song.name;
                string songArtist="";
                string url="";
                foreach (var artist in song.artists)
                {
                    songArtist +=artist.name;
                    songArtist += "/";
                }

              dynamic songDetail  =net.GetMusicDetail(id);
                foreach (var detail in songDetail.data)
                {
                    url = detail.url;
                }
              SongsList.Add(new Music(songName,songArtist,"null",url));
            }
            
            InitList();

     
        }

        public void InitList()
        {
            foreach (var song in SongsList)
            {
                listViewSearch.Items.Add(song);
            }

        }

 
        private void listViewSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            media.Open(new Uri((listViewSearch.SelectedItem as Music).Url));
            media.Play();

        }
    }
}

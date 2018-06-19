using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Mp3Window
{
    /// <summary>
    /// ChooseListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseListWindow : Window
    {
        //列表显示的类容
        public List<ListName> _listNames { get; set; }
        //将要被添加的歌曲
        public Music song;
        public ChooseListWindow()
        {
            InitializeComponent();
            this.Loaded += Load;
        }


        /// <summary>
        /// 初始化工作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Load(object sender, RoutedEventArgs e)
        {
            foreach (var name in _listNames)
            {
                MusicListListView.Items.Add(name);
            }
        }
        /// <summary>
        /// 选择歌单后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicListListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = Data.FindElectMusicList( (MusicListListView.SelectedItem as ListName)?.Name);

            list.Songs.Add(song);
            Data.SaveData();
            this.Close();
        }

     
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
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
        List<MusicList> MusicLists =new List<MusicList>();
        //当前显示歌单
        private string electMusicList
        //指定父窗口的指针
        private MainWindow ParentWindow;
        public MusicListPage()
        {
            InitializeComponent();
           

        }
        /// <summary>
        ///  按名称找到被选中的歌单
        /// </summary>
        /// <returns></returns>
        public MusicList FindElectMusicList()
        {
            string elect = ParentWindow.MusicListListView.SelectedItem as string;

            foreach (var musicList in MusicLists )
            {
                if (musicList.Name.Equals(elect))
                {
                    return musicList;
                }
            }

            return null;
        }
      
    }
}

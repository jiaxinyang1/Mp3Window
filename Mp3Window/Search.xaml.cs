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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mp3Window
{
    /// <summary>
    /// Search.xaml 的交互逻辑
    /// </summary>
    public partial class Search : Page
    {
        public List<Music> SongsList=new List<Music>();
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

        }

        public void InitList()
        {

        
        }
    }
}

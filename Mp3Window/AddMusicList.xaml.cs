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
    /// AddMusicList.xaml 的交互逻辑
    /// </summary>
    public partial class AddMusicListxaml : Window
    {
  
        public AddMusicListxaml()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MusicList  newMusicList = new MusicList();
            newMusicList = Data.MusicLists[0].Clone() as MusicList;
            newMusicList.Name = TextBox.Text;
            newMusicList.Time = DateTime.Now.ToString();
            Data.MusicListName.Add(new ListName(TextBox.Text));
            Data.MusicLists.Add(newMusicList);
            Data.SaveName();
            Data.SaveData();
            this.Close();
        }
    }
}

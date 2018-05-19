using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mp3Window
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button CloseButton;
        private Button MinButton;
        private TextBlock WindowTitleTbl;
        private Button MaxButton;
        public MainWindow()
        {
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            ControlTemplate MainWindowTemplate
                = Application.Current.Resources["MainWindowTemplate"] as ControlTemplate;

            if (MainWindowTemplate!=null)
            {
                CloseButton = MainWindowTemplate.FindName("CloseWinButton", this) as Button;
                MinButton = MainWindowTemplate.FindName("MinWinButton", this) as Button;
                MaxButton = MainWindowTemplate.FindName("MaxWinButton",this)as Button;
                CloseButton.Click += CloseButton_Click;
                MinButton.Click += MinButton_Click;
                MaxButton.Click += MaxButton_Click;
                WindowTitleTbl = MainWindowTemplate.FindName("WindowTitleTbl", this) as TextBlock;
            }

          }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
           //最大化处理;
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }

        private void MinButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        /// <summary>
        /// 实现窗体移动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            DragMove();

            base.OnMouseLeftButtonDown(e);
        }

    }



}

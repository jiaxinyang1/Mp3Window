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


        /// <summary>
        /// 最大化不覆盖任务栏
        private bool isMaxWindow = false;//定义窗口的状态
        Rect rcnormal;//定义一个全局rect记录还原状态下窗口的位置和大小。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
        
            if (!isMaxWindow)
            {
                //最大化处理;
                rcnormal = new Rect(this.Left, this.Top, this.Width, this.Height);//保存下当前位置与大小
                Rect rc = SystemParameters.WorkArea;
                this.Left = 0;//设置位置
                this.Top = 0;
                this.Width = rc.Width;
                this.Height = rc.Height;
                isMaxWindow = true;//设置窗口状态标识符
            }
            else
            {
                this.Left = rcnormal.Left;
                this.Top = rcnormal.Top;
                this.Width = rcnormal.Width;
                this.Height = rcnormal.Height;
                isMaxWindow = false; ;//设置窗口状态标识符
            }
          
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

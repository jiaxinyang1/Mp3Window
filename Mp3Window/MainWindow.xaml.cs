using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Mp3Window
{

    class Trsm 
    {
        private string Name;
        private string v;
        private string c;
        private int d;

        public Trsm(string a, string v, string c)
        {
            this.Name = a;
            this.v = v;
            this.c = c;
        }

    
    }

    public class PopopHelper
    {
        //实现了浮动层跟随移动
    public static DependencyObject GetPopupPlacementTarget(DependencyObject obj)
        {
            return (DependencyObject)obj.GetValue(PopupPlacementTargetProperty);
        }

        public static void SetPopupPlacementTarget(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(PopupPlacementTargetProperty, value);
        }

        // Using a DependencyProperty as the backing store for PopupPlacementTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupPlacementTargetProperty =
            DependencyProperty.RegisterAttached("PopupPlacementTarget", typeof(DependencyObject), typeof(PopopHelper), new PropertyMetadata(null, OnPopupPlacementTargetChanged));

        private static void OnPopupPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DependencyObject popupPopupPlacementTarget = e.NewValue as DependencyObject;
                Popup pop = d as Popup;

                Window w = Window.GetWindow(popupPopupPlacementTarget);
                if (null != w)
                {
                    w.LocationChanged += delegate
                    {
                        var offset = pop.HorizontalOffset;
                        pop.HorizontalOffset = offset + 1;
                        pop.HorizontalOffset = offset;
                    };
                }
            }
        }

    }
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

            listView1.Items.Add(new {SongName = "男",Singer="hhhh",Time="11:20"});
            listView1.Items.Add(new { SongName = "weq", Singer = "2h", Time = "11:20" });
            listView1.Items.Add(new { SongName = "2", Singer = "hhhh", Time = "11:20" });
            listView1.Items.Add(new { SongName = "fdf", Singer = "fqhh", Time = "11:20" });
            
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
       /* //添加歌曲条目的一个方法
        //对xaml进行深度复制
        private void Button_Click(object sender, RoutedEventArgs e)
        {
   
            string xaml1 = System.Windows.Markup.XamlWriter.Save(list);
            StackPanel oPanel= System.Windows.Markup.XamlReader.Parse(xaml1) as StackPanel;
     
            ListPanel.Children.Add(oPanel ?? throw new InvalidOperationException());
        }*/


        //显示浮动层
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Popup1.IsOpen = false;
            Popup1.IsOpen = true;
        }
    }



}

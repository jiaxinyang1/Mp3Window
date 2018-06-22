using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Lsj.Util.JSON;
using NeteaseCloud;

namespace Mp3Window
{
  
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
  
        private MusicListPage _musicListPage;//指向子窗口

        System.Windows.Media.MediaPlayer media = new MediaPlayer();//播放类

        private bool isPlaying=false;//是否正在播放
        private BitmapImage playingImage = new BitmapImage(new Uri("image/play.png", UriKind.Relative));
        private BitmapImage pauseBitmapImage= new BitmapImage(new Uri("image/pause.png", UriKind.Relative));

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
          
            //加载数据
            
            Data.ReadData();
          
            Data.ReadName();

            Data.net=new NetEase();
            InitLeftMusicListView();
            MusicListListView.SelectedItem = Data.MusicListName[0];
            MusicListListView_SelectionChanged(this,null);
            //初始化左边的歌单名称列表

        }
        /// <summary>
        /// 初始化左边导航栏ListView
        /// 一些异常处理待做
        /// </summary>
        public  void InitLeftMusicListView()
        {
            MusicListListView.ItemsSource = Data.MusicListName;
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
            try
            {
                DragMove();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            base.OnMouseLeftButtonDown(e);
        }
        /// <summary>
        /// 显示音量调节滑动条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Popup1.IsOpen = false;
            Popup1.IsOpen = true;
           
        }
        /// <summary>
        /// 显示搜索页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //清空列表框选择
            MusicListListView.SelectedItem = null;
            Search search =new Search();
            ContentControl.Content = new Frame() {Content = search};
        }
        /// <summary>
        /// 歌单导航栏选择项目被改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MusicListListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (MusicListListView.SelectedItem!=null)
            {
                _musicListPage = new MusicListPage();
                _musicListPage.ParentWindow = this;
                _musicListPage.Init();
                ContentControl.Content = new Frame() { Content = _musicListPage };
            }
           
        }
        /// <summary>
        /// 播放按钮被按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!isPlaying)
            {
                isPlaying = true;
                Buttonplay.Source = pauseBitmapImage;
                media.Open(new Uri(GetUri()));
                while (!media.NaturalDuration.HasTimeSpan)
                {
                    Thread.Sleep(10);
                }
                media.Play();

                System.Windows.Threading.DispatcherTimer myTimer = new System.Windows.Threading.DispatcherTimer();
                myTimer.Tick += new EventHandler(TimeChanged);
                myTimer.Interval = new TimeSpan(0, 0, 0, 1);
                myTimer.Start();
            }
            else
            {
                media.Pause();
                isPlaying = false;
                Buttonplay.Source = playingImage;
            }
      


      

        

            
       
        }

        /// <summary>
        /// 获取歌曲时间长度
        /// </summary>
        private void GetTime()
        {

            string strMinutes;
            string strSeconds;
            var minutes = media.NaturalDuration.TimeSpan.Minutes;
            var seconds = media.NaturalDuration.TimeSpan.Seconds;
            if (minutes < 10)
                strMinutes = "0" + minutes;
            else
                strMinutes = minutes.ToString();
            if (seconds < 10)
                strSeconds = "0" + seconds;
            else
                strSeconds = seconds.ToString();
            SongTime.Content = "/" + strMinutes + ":" + strSeconds;
        }
        /// <summary>
        /// 更新时间戳
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeChanged(object sender,EventArgs e )
        {

            SongTimeChanged.Content = media.Position.ToString(@"mm\:ss");

        }
        /// <summary>
        /// 网易会更新uri 得实时拿
        /// </summary>
        /// <param name="song"></param>
        public  string GetUri()
        {
            string url="null";
            int id =Int32.Parse(Data.SelcetMusic.Url) ;
     
            dynamic songDetail = Data.net.GetMusicDetail(id);
            if (songDetail != null)
            {
                foreach (var detail in songDetail.data)
                {
                    url = detail.url;
                }
            }

            return url;
        }
        /// <summary>
        /// 创建歌单按钮被按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Window addmuscilistWindow = new AddMusicListxaml();
            addmuscilistWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addmuscilistWindow.Owner = this;
            addmuscilistWindow.ShowDialog();

        }
    }

}

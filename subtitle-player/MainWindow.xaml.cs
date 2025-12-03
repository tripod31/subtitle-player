using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace subtitle_player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class Settings
    {
        /*
         * メイン画面と設定画面の間の設定値の受け渡し用
         */
        public System.Windows.Media.FontFamily? FontFamily;
        public double FontSize;
        public System.Windows.Media.Brush? Foreground;
        public System.Windows.Media.Brush? Background;

        public Settings()
        {
        }
    }

    public partial class MainWindow : Window
    {
        private SrtCtrl _ctrl = new SrtCtrl();
        private DateTime start_time;
        private Settings _settings = new Settings();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            // アプリケーション設定を読み込む
            // ウィンドウ位置とサイズの設定
            Top     = Properties.Settings.Default.top;
            Left    = Properties.Settings.Default.left;
            Width   = Properties.Settings.Default.width;
            Height  = Properties.Settings.Default.height;

            //歌詞表示部のフォント設定
            text_lyric.FontFamily = new System.Windows.Media.FontFamily(Properties.Settings.Default.FontFamily);
            text_lyric.FontSize = Properties.Settings.Default.FontSize;

            string colorString = Properties.Settings.Default.font_color;
            System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString);
            text_lyric.Foreground = new SolidColorBrush(color);

            // 歌詞表示部の背景色の設定
            colorString = Properties.Settings.Default.back_color;
            color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(colorString);
            SolidColorBrush brush = new SolidColorBrush(color);
            text_lyric.Background = brush;
            text_lyric.Background.Opacity = Properties.Settings.Default.opacity;

            text_lyric.Content ="ここに歌詞が表示されます";
        }

        private void main_Closed(object sender, EventArgs e)
        {
            // 状態をアプリケーション設定に保存
            Properties.Settings.Default.top     = Top;
            Properties.Settings.Default.left    = Left;
            Properties.Settings.Default.width   = Width;
            Properties.Settings.Default.height  = Height;

            Properties.Settings.Default.FontFamily  = text_lyric.FontFamily.ToString();
            Properties.Settings.Default.FontSize    = text_lyric.FontSize;

            Properties.Settings.Default.font_color = text_lyric.Foreground.ToString();
            Properties.Settings.Default.back_color = text_lyric.Background.ToString();
   
            Properties.Settings.Default.opacity = text_lyric.Background.Opacity;

            Properties.Settings.Default.Save();

            Close();
        }
        public void start_disp()
        {
            /*
             * 字幕表示開始
             */
            start_time = DateTime.Now;
            timer.Interval = TimeSpan.FromSeconds(1); // 1秒ごと
#pragma warning disable CS8622 
            // パラメーターの型における参照型の NULL 値の許容が、ターゲット デリゲートと一致しません。
            // おそらく、NULL 値の許容の属性が原因です。
            timer.Tick += Timer_Tick;
#pragma warning restore CS8622 
            // パラメーターの型における参照型の NULL 値の許容が、ターゲット デリゲートと一致しません。
            // おそらく、NULL 値の許容の属性が原因です。
            timer.Start();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // 読み込みボタン
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Text files (*.srt)|*.srt|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;

            // ダイアログを表示
            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;
                text_file.Content = System.IO.Path.GetFileName(path);
                _ctrl.read(path);
                if (_ctrl.errs.Count > 0)
                {
                    string msg = string.Join("\r\n", _ctrl.errs);
                    System.Windows.MessageBox.Show(msg);
                }
            }
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // 再生ボタン
            if (_ctrl.arr.Count == 0)
            {
                System.Windows.MessageBox.Show("SRTファイルが読み込まれていません");
                return;
            }
            Window1 window = new Window1(this);
            window.ShowDialog();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            /*
              * タイマーイベント
              * 現在秒に合致する字幕を表示
              */
            TimeSpan ts = DateTime.Now - start_time;
            text_sec.Content = $"{ts.Minutes:D2}:{ts.Seconds:D2}";
            List<string> subtitles = _ctrl.GetSubTitles(ts);
            string s = String.Join("\r\n", subtitles);
            if (text_lyric.Content.ToString() != s)
                text_lyric.Content = s;
            if (ts > _ctrl.last)
            {
                timer.Stop();
                text_sec.Content = "";
            }
        }


        private void button3_Click(object sender, RoutedEventArgs e)
        {
            // 停止ボタン
            timer.Stop();
            text_lyric.Content = "";
            text_sec.Content = "";
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            // 設定ボタン
            Window2 window = new Window2(this);
            window.Owner = this; // 親ウィンドウを指定
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                this.DragMove(); // ウィンドウをドラッグで移動
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
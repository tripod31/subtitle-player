using System;
using System.Collections.Generic;
using System.Drawing;       // Fontクラス用
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms; // WinFormsの名前空間
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace subtitle_player
{
    /// <summary>
    /// Window2.xaml の相互作用ロジック
    /// </summary>
    public partial class Window2 : Window
    {
        //設定画面

        private MainWindow _parent;
        public Window2(MainWindow parent)
        {
            this._parent = parent;
            InitializeComponent();

            // メイン画面の設定を反映
            slider_opacity.Value = _parent.text_lyric.Background is SolidColorBrush bgBrush2
                ? bgBrush2.Opacity
                : 1.0;
            slider_width.Value = _parent.Width;
            slider_width.Minimum = _parent.MinWidth;
            slider_width.Maximum = SystemParameters.PrimaryScreenWidth;

            slider_height.Value = _parent.Height;
            slider_height.Minimum = _parent.MinHeight;
            slider_height.Maximum = SystemParameters.PrimaryScreenHeight;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //字幕フォントボタン
            using (FontDialog fontDialog = new FontDialog())
            {
                fontDialog.ShowColor = true;

                // WPFのFontFamily, FontSize → System.Drawing.Font へ変換

                // CS8602 対策: nullチェックを追加
                string font_name = _parent.text_lyric.FontFamily != null
                    ? _parent.text_lyric.FontFamily.Source
                    : "Segoe UI"; // デフォルト値

                double font_size = _parent.text_lyric.FontSize * 72.0 / 96.0; // DIP → pt変換

                // ダイアログの初期フォント設定
                System.Drawing.Font font = new System.Drawing.Font(font_name, (float)font_size);
                fontDialog.Font = font;

                // foregroundがSolidColorBrushかつnullでない場合のみColorを取得
                System.Windows.Media.Color foreColor = Colors.Black;
                if (_parent.text_lyric.Foreground is SolidColorBrush solidColorBrush && solidColorBrush != null)
                {
                    foreColor = solidColorBrush.Color;
                }
                fontDialog.Color = System.Drawing.Color.FromArgb(
                    foreColor.A,
                    foreColor.R,
                    foreColor.G,
                    foreColor.B);

                if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 選択されたフォントを取得
                    Font selectedFont = fontDialog.Font;
                    System.Drawing.Color selectedColor = fontDialog.Color;

                    // WPF用に変換
                    System.Windows.Media.FontFamily fontFamily = new System.Windows.Media.FontFamily(selectedFont.Name);
                    double fontSize = selectedFont.Size * 96.0 / 72.0; // pt → DIP変換

                    // foregroundがSolidColorBrushかつnullでない場合のみColorを設定
                    if (_parent.text_lyric.Foreground is SolidColorBrush solidColorBrushSet && solidColorBrushSet != null)
                    {
                        solidColorBrushSet.Color = System.Windows.Media.Color.FromArgb(
                            selectedColor.A,
                            selectedColor.R,
                            selectedColor.G,
                            selectedColor.B
                        );
                    }

                    _parent.text_lyric.FontFamily = fontFamily;
                    _parent.text_lyric.FontSize = fontSize;
                }
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //字幕背景色ボタン
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = System.Drawing.Color.FromArgb(
                    _parent.text_lyric.Background is SolidColorBrush solidColorBrush ?
                    solidColorBrush.Color.A : (byte)255,
                    _parent.text_lyric.Background is SolidColorBrush solidColorBrushR ?
                    solidColorBrushR.Color.R : (byte)0,
                    _parent.text_lyric.Background is SolidColorBrush solidColorBrushG ?
                    solidColorBrushG.Color.G : (byte)0,
                    _parent.text_lyric.Background is SolidColorBrush solidColorBrushB ?
                    solidColorBrushB.Color.B : (byte)0);

                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    System.Drawing.Color selectedColor = colorDialog.Color;
                    // foregroundがSolidColorBrushかつnullでない場合のみColorを設定
                    if (_parent.text_lyric.Background is SolidColorBrush solidColorBrushSet && solidColorBrushSet != null)
                    {
                        solidColorBrushSet.Color = System.Windows.Media.Color.FromArgb(
                            selectedColor.A,
                            selectedColor.R,
                            selectedColor.G,
                            selectedColor.B
                        );
                    }
                }
            }
        }

        private void slider_opacity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // nullチェックを追加してCS8602を回避
            if (_parent.text_lyric.Background != null)
            {
                _parent.text_lyric.Background.Opacity = slider_opacity.Value;
            }
        }
 
        private void slider_width_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //ウィンドウ幅スライダー
           _parent.Width = slider_width.Value;
        }

        private void slider_height_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //ウィンドウ高さスライダー
            _parent.Height = slider_height.Value;
        }
    }
}
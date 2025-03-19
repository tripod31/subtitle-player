using Microsoft.VisualBasic;

namespace subtitle_player
{
    public partial class Form1 : Form
    {
        private SrtCtrl _ctrl = new SrtCtrl();
        private DateTime start_time;
        private Settings _settings = new Settings();
        public Settings settings
        {
            get { return _settings; }
        }

        public Form1()
        {
            InitializeComponent();

            // 保存された設定を適用
            Size = (Size)Properties.Settings.Default.size;
            Location = (Point)Properties.Settings.Default.location;
            textBox1.Font = (Font)Properties.Settings.Default.font;
            textBox1.ForeColor = (Color)Properties.Settings.Default.font_color;
            textBox1.BackColor = (Color)Properties.Settings.Default.back_color;
            Opacity = Properties.Settings.Default.opacity;
        }

        public void start_disp()
        {
            /*
             * 字幕表示開始
             */
            start_time = DateTime.Now;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
             * 「再生」ボタン
             */
            if (_ctrl.arr.Count == 0)
            {
                MessageBox.Show("SRTファイルが読み込まれていません");
                return;
            }
            Form form = new Form2(this);
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
             * 「読み込み」ボタン
             */
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                label_file.Text = System.IO.Path.GetFileName(path);
                _ctrl.read(path);
                if (_ctrl.errs.Count > 0)
                {
                    string msg = string.Join("\r\n", _ctrl.errs);
                    MessageBox.Show(msg);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*
             * タイマーイベント
             * 開始秒に合致する字幕を表示
             */
            TimeSpan ts = DateTime.Now - start_time;
            label_sec.Text = $"{ts.Minutes:D2}:{ts.Seconds:D2}";
            List<string> subtitles = _ctrl.GetSubTitles(ts);
            string s = String.Join("\r\n", subtitles);
            if (textBox1.Text != s)
                textBox1.Text = s;
            if (ts > _ctrl.last)
            {
                timer1.Stop();
                label_sec.Text = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
             * 設定を保存
             * form.StartPositionをManualにしないとLocationが反映されない
             */
            Properties.Settings.Default.size = Size;
            Properties.Settings.Default.location = Location;
            Properties.Settings.Default.font = textBox1.Font;
            Properties.Settings.Default.font_color = textBox1.ForeColor;
            Properties.Settings.Default.back_color = textBox1.BackColor;
            Properties.Settings.Default.opacity = this.Opacity;

            Properties.Settings.Default.Save();
        }

        //private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            /*
              * 「設定」ボタン
              */
            // 設定受け渡し用のsettingsに設定を保存
            _settings.font = textBox1.Font;
            _settings.font_color = textBox1.ForeColor;
            _settings.back_color = textBox1.BackColor;
            _settings.opacity = Opacity;

            Form3 form = new Form3(this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = _settings.font;
                textBox1.ForeColor = _settings.font_color;
                textBox1.BackColor = _settings.back_color;
                Opacity = _settings.opacity;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*
             * 「停止」ボタン
             */
            timer1.Stop();
            textBox1.Text = "";
        }
    }
}

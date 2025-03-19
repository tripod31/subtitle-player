using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace subtitle_player
{
    public partial class Form3 : Form
    {
        private Form1 parent;
        public Form3(Form1 parent)
        {
            InitializeComponent();

            this.parent = parent;

            // メイン画面の設定を反映
            label_sample.Font = parent.settings.font;
            label_sample.ForeColor = parent.settings.font_color;
            label_sample.BackColor = parent.settings.back_color;
            textBox1.Text = parent.settings.opacity.ToString("F2");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
              * フォント選択
              */
            fontDialog1.Font = label_sample.Font;
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                label_sample.Font = fontDialog1.Font;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
             * フォント色設定
             */
            colorDialog1.Color=label_sample.ForeColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label_sample.ForeColor = colorDialog1.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*
             * OKボタン
             */

            //設定受け渡し
            parent.settings.font = label_sample.Font;
            parent.settings.font_color = label_sample.ForeColor;
            parent.settings.back_color = label_sample.BackColor;
            parent.settings.opacity = double.Parse(textBox1.Text);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*
             * 字幕背景色
             */
            colorDialog1.Color = label_sample.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label_sample.BackColor = colorDialog1.Color;
            }
        }
    }
}

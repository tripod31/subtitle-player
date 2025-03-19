namespace subtitle_player
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_sample = new Label();
            button1 = new Button();
            colorDialog1 = new ColorDialog();
            fontDialog1 = new FontDialog();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            label2 = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // label_sample
            // 
            label_sample.AutoSize = true;
            label_sample.BorderStyle = BorderStyle.FixedSingle;
            label_sample.Location = new Point(156, 38);
            label_sample.Name = "label_sample";
            label_sample.Size = new Size(96, 22);
            label_sample.TabIndex = 0;
            label_sample.Text = "フォントサンプル";
            // 
            // button1
            // 
            button1.Location = new Point(31, 31);
            button1.Name = "button1";
            button1.Size = new Size(107, 34);
            button1.TabIndex = 1;
            button1.Text = "字幕フォント";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(31, 89);
            button2.Name = "button2";
            button2.Size = new Size(107, 31);
            button2.TabIndex = 2;
            button2.Text = "字幕フォント色";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.DialogResult = DialogResult.OK;
            button3.Location = new Point(31, 249);
            button3.Name = "button3";
            button3.Size = new Size(107, 36);
            button3.TabIndex = 3;
            button3.Text = "OK";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button4.DialogResult = DialogResult.Cancel;
            button4.Location = new Point(156, 249);
            button4.Name = "button4";
            button4.Size = new Size(90, 36);
            button4.TabIndex = 4;
            button4.Text = "キャンセル";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(31, 144);
            button5.Name = "button5";
            button5.Size = new Size(107, 31);
            button5.TabIndex = 5;
            button5.Text = "字幕背景色";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 201);
            label2.Name = "label2";
            label2.Size = new Size(226, 20);
            label2.TabIndex = 6;
            label2.Text = "透明度（0～1.0、0=完全に透明）";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(270, 198);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 27);
            textBox1.TabIndex = 7;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 301);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label_sample);
            Name = "Form3";
            StartPosition = FormStartPosition.CenterParent;
            Text = "設定";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_sample;
        private Button button1;
        private ColorDialog colorDialog1;
        private FontDialog fontDialog1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label2;
        private TextBox textBox1;
    }
}
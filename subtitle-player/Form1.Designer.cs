namespace subtitle_player
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            textBox1 = new TextBox();
            timer1 = new System.Windows.Forms.Timer(components);
            button2 = new Button();
            openFileDialog1 = new OpenFileDialog();
            label_sec = new Label();
            fontDialog1 = new FontDialog();
            colorDialog1 = new ColorDialog();
            button3 = new Button();
            label_file = new Label();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(93, 422);
            button1.Name = "button1";
            button1.Size = new Size(87, 27);
            button1.TabIndex = 2;
            button1.Text = "再生";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.AccessibleRole = AccessibleRole.SplitButton;
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.BackColor = SystemColors.ActiveBorder;
            textBox1.Enabled = false;
            textBox1.Font = new Font("Yu Gothic UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBox1.ForeColor = Color.Salmon;
            textBox1.Location = new Point(0, 0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(777, 414);
            textBox1.TabIndex = 1;
            textBox1.TabStop = false;
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(12, 422);
            button2.Name = "button2";
            button2.Size = new Size(75, 27);
            button2.TabIndex = 1;
            button2.Text = "読み込み";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // label_sec
            // 
            label_sec.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label_sec.AutoSize = true;
            label_sec.Location = new Point(306, 425);
            label_sec.Name = "label_sec";
            label_sec.Size = new Size(54, 20);
            label_sec.TabIndex = 3;
            label_sec.Text = "再生秒";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.Location = new Point(690, 420);
            button3.Name = "button3";
            button3.Size = new Size(75, 30);
            button3.TabIndex = 4;
            button3.Text = "設定";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label_file
            // 
            label_file.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label_file.AutoSize = true;
            label_file.Location = new Point(377, 425);
            label_file.Name = "label_file";
            label_file.Size = new Size(81, 20);
            label_file.TabIndex = 5;
            label_file.Text = "再生ファイル";
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button4.Location = new Point(186, 422);
            button4.Name = "button4";
            button4.Size = new Size(75, 27);
            button4.TabIndex = 3;
            button4.Text = "停止";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(777, 462);
            Controls.Add(button4);
            Controls.Add(label_file);
            Controls.Add(button3);
            Controls.Add(label_sec);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Name = "Form1";
            Opacity = 0.5D;
            StartPosition = FormStartPosition.Manual;
            Text = "字幕プレイヤー";
            TopMost = true;
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private Button button2;
        private OpenFileDialog openFileDialog1;
        private Label label_sec;
        private FontDialog fontDialog1;
        private ColorDialog colorDialog1;
        private Button button3;
        private Label label_file;
        private Button button4;
    }
}

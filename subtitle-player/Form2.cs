using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace subtitle_player
{
    public partial class Form2 : Form
    {
        Form1 parent;

        public Form2(Form1 parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void Form2_Deactivate(object sender, EventArgs e)
        {
            this.parent.start_disp();
            this.Close();
        }
    }
}

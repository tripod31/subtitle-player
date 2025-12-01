using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace subtitle_player
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow parent;
        public Window1(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.parent.start_disp();
            this.Close();
        }
    }
}

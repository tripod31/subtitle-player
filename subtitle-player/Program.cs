using System.Diagnostics;

namespace subtitle_player
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //test();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static private void test()
        {
            SrtCtrl ctrl= new SrtCtrl();
            ctrl.read("D:\\yoshi\\Videos\\ブラジルゴスペル\\ブログ用srt\\test.srt");
            TimeSpan ts = new TimeSpan(0, 0, 20);
            List<string> subtitles = ctrl.GetSubTitles(ts);
            string s = String.Join("\r\n", subtitles);
            Debug.WriteLine(s);
        }
    }
}
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace subtitle_player
{

    class SrtData
    {
        /*
         * SRTファイル中の１件のデータ
         */
        public int index;
        public TimeSpan start;
        public TimeSpan end;
        public List<String> subtitles;

        public SrtData()
        {
            index = 0;
            start = new TimeSpan(0, 0, 0);
            end   = new TimeSpan(0, 0, 0);
            this.subtitles = [];
        }

        public SrtData(int index ,
            int s_min,
            int s_sec,
            int e_min,
            int e_sec,
            List<String> subtitles)
        {
            this.index = index;
            start  = new TimeSpan(0, s_min, s_sec);
            end    = new TimeSpan(0, e_min, e_sec);

            this.subtitles = subtitles;
        }
    }

    class SrtCtrl
    {
        /*
         * SrtDataを操作する
         */

        private int _index = 0; //字幕の通番

        private List<SrtData> _arr;
        public List<SrtData> arr
        {
            get {  return _arr; }
        }
        
        private List<string> _errs = new List<string>();
        public List<string> errs
        {
            get { return _errs; }
        }

        public TimeSpan last
        {
            get { return _arr[_arr.Count - 1].end; }
        }   // 再生が終わる秒数


        public SrtCtrl()
        {
            this._arr = [];
        }


        public void read(string path)
        {
            /*
             * SRTファイルからSrtDataの配列に読み込む
             */
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
            string data = sr.ReadToEnd();
            sr.Close();
            string[] lines = data.Replace("\r\n", "\n").Split(['\n', '\r']);

            _errs.Clear();
            _index = 0; 
            List<string> buf = [];
            foreach (string line in lines)
            {
                if (line.Length == 0) { 
                    // 空行
                    if (buf.Count > 0)
                    {
                        parse_block(buf);
                        buf = [];
                    }
                }
                else
                {
                    buf.Add(line);
                }

            }
        }

        private void parse_block(List<string> lines) { 
            // 通番行
            SrtData data = new SrtData();

            if (lines.Count >= 3) {
                // 字幕文字列
                data.subtitles = lines[2..];
            }
            else
            {
                _errs.Add($"字幕データに字幕がない: {lines[0]}");
            }

            Match match = Regex.Match(lines[0], @"^(\d+)$");
            if (match.Success) {
                data.index = int.Parse(match.Value);
            }
            else
            {
                _errs.Add($"字幕データの１行目が通番でない: {lines[0]}");
            }

            // 開始秒 --> 終了秒行
            match = Regex.Match(lines[1], @"^(\d{2}):(\d{2}):(\d{2}),\d{3} --> (\d{2}):(\d{2}):(\d{2}),\d{3}$");
            if (match.Success)
            {
                data.start = new TimeSpan(
                    int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                data.end   = new TimeSpan(
                    int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));
            }
            else
            {
                _errs.Add($"字幕データの２行目が時間指定行でない: {lines[1]}");
            }
            _arr.Add(data);
        }

        public List<string> GetSubTitles(TimeSpan t)
        {
            /*
             * 開始秒を指定する。合致する字幕を返す。
             */
            List<string> ret = [];
            // 後ろから探す
            // 字幕の終了秒数と次の字幕の開始秒数が同じ場合、次の字幕を適用するため
            for (int i = _arr.Count - 1; i >= 0; i--)
            {
                var data = _arr[i];

                if (t >= data.start && t <= data.end)
                {
                    ret = data.subtitles;
                    break;
                }
            }

            return ret;
        }
    }

}

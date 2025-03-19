using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace subtitle_player
{
    public class Settings
    {
        /*
         * メイン画面と設定画面の間の設定値の受け渡し用
         */
        public Font font;
        public Color font_color;
        public Color back_color;
        public double opacity;

        public Settings()
        {
            font = (Font)Properties.Settings.Default.font;
        }
    }

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
            this.subtitles = [];
        }

        public SrtData(int index,int s_min,int s_sec,int e_min,int e_sec, List<String> subtitles)
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

        private SrtData _data = new SrtData();  //現在読み込んでいる１つのデータ
        // SRTファイルから１セクション読み込み時に各項目を読み込んだかチェック用
        private List<string> _flags= new List<string>();                    //読み込んだ項目名のリスト
        private string[]    _items = ["index", "time_span", "subtitles"];   //SRTの１セクション中の項目名

        public SrtCtrl()
        {
            this._arr = [];
        }

        private List<string> check_flags()
        {
            /*
             * 引数：
             *      flags:  List<string:読み込んだ項目値>
             * 戻り値：
             *      List<string:読み込まれていない項目名>
             */
            List<string> ret=new List<string>();
            foreach (string item in _items)
            {
                if (!_flags.Contains(item))
                {
                    ret.Add(item);
                    break;
                }
            }
            return ret;
        }

        private void add_data()
        {
            /*
             * SrtDataの配列に１つ追加
             */
            List<string>missing = this.check_flags();
            if (missing.Count == 0)
            {
                // SrtDataの配列に追加
                _arr.Add(_data);
            }
            else
            {
                //読み込む項目が欠けている場合
                string msg = $"{_data.index}：以下の項目が欠けています："+ String.Join(",", missing.ToArray());
                _errs.Add(msg);
            }
            _data = new SrtData();
            _flags.Clear();
        }


        public void read(string path)
        {
            /*
             * SRTファイルからSrtDataの配列に読み込む
             */
            StreamReader sr = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
            string data = sr.ReadToEnd();
            sr.Close();
            string[] lines = data.Replace("\r\n", "\n").Split([ '\n', '\r' ]);
            
            _errs.Clear();
            _data = new SrtData();
            int idx = 0;
            foreach (string line in lines)
            {
                idx++;
                if (line.Length == 0)
                    // 空行はスキップ
                    continue;

                // 通番
                Match match = Regex.Match(line, @"^(\d+)$");
                if (match.Success) {
                    if ( _flags.Contains("index")){
                        add_data(); // 最初の通番行でなければ、データを１件追加
                    }

                    _data.index = int.Parse(match.Value);
                    _flags.Add("index");
                    continue;
                }

                // 開始秒 --> 終了秒
                match = Regex.Match(line, @"^(\d{2}):(\d{2}):(\d{2}),\d{3} --> (\d{2}):(\d{2}):(\d{2}),\d{3}$");
                if (match.Success)
                {
                    _data.start = new TimeSpan(
                        int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                    _data.end   = new TimeSpan(
                        int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value), int.Parse(match.Groups[6].Value));
                    _flags.Add("time_span");
                    continue;
                }

                // 字幕文字列
                _data.subtitles.Add(line);
                _flags.Add("subtitles");
            }
            // 最後のデータを追加
            add_data();
        }

        public List<string> GetSubTitles(TimeSpan t)
        {
            /*
             * 開始秒を指定する。合致する字幕を返す。
             */
            List<string> ret = [];

            for (int i = this._arr.Count -1; i >=0 ; i--)    // 後ろから見ていく
            {
                SrtData data = this._arr[i];
                if (t>=data.start && t <= data.end)
                {
                    ret =  data.subtitles;
                    break;
                }
            }
            return ret;
        }
    }

}

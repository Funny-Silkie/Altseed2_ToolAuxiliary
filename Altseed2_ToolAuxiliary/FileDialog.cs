using System;
using System.IO;
using System.Text;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ファイルダイアログの基底クラス
    /// </summary>
    [Serializable]
    public abstract class FileDialog : DialogBase
    {
        /// <summary>
        /// 選択したファイルが存在するかどうかを取得または設定する
        /// </summary>
        public bool CheckFileExists { get; set; }
        /// <summary>
        /// 選択するファイル名を取得または設定する
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// フィルターを取得または設定する
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// <see cref="FileDialog"/>の新しいインスタンスを生成する
        /// </summary>
        protected FileDialog() { }
        private protected static bool CheckFilePath(string str, out string log)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                log = "Path is conposed by white space";
                return false;
            }
            if (str.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                log = "Path has invalid char";
                return false;
            }
            var directory = Path.GetDirectoryName(str);
            if (!Directory.Exists(directory))
            {
                log = $"Directory is not exists\nPath: {str}";
                return false;
            }
            log = null;
            return true;
        }
        /// <summary>
        /// <see cref="Filter"/>に複数の拡張子を登録する
        /// </summary>
        /// <param name="extensions">登録する拡張子</param>
        /// <exception cref="ArgumentNullException"><paramref name="extensions"/>がnull</exception>
        public void SetFilter(params string[] extensions)
        {
            if (extensions == null) throw new ArgumentNullException(nameof(extensions), "引数がnullです");
            if (extensions.Length == 0)
            {
                Filter = string.Empty;
                return;
            }
            var builder = new StringBuilder();
            for (int i = 0; i < extensions.Length; i++)
            {
                if (i != 0) builder.Append(',');
                builder.Append(extensions[i].TrimStart('.'));
            }
            Filter = builder.ToString();
        }
    }
}

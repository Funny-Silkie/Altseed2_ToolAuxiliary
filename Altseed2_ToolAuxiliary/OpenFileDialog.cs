using System;
using System.IO;
using System.Threading;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 1つの読み込むファイルを選択するダイアログ
    /// </summary>
    [Serializable]
    public class OpenFileDialog : FileDialog
    {
        /// <summary>
        /// 選択されたすべてのファイルパスを取得する
        /// </summary>
        public string[] FileNames { get; private set; }
        /// <summary>
        /// 複数選択をするかどうかを取得または設定する
        /// </summary>
        public bool MultiSelect { get; set; }
        /// <summary>
        /// <see cref="OpenFileDialog"/>の新しいインスタンスを生成する
        /// </summary>
        public OpenFileDialog() { }
        public override bool ShowDialog(out string log)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) throw new InvalidOperationException("Main関数にSTAThreadAttributeが設定されていることを確認してください");
            if (MultiSelect)
            {
                var path = Engine.Tool.OpenDialogMultiple(Filter ?? string.Empty, Directory.Exists(InitialDirectory) ? InitialDirectory : string.Empty);
                if (string.IsNullOrEmpty(path))
                {
                    log = null;
                    return false;
                }
                var array = path.Split(',');
                if (CheckFileExists)
                    for (int i = 0; i < array.Length; i++)
                        if (!Engine.File.Exists(array[i]))
                        {
                            log = $"File does not exists\nPath: {array[i]}";
                            return false;
                        }
                if (CheckPathExists)
                    for (int i = 0; i < array.Length; i++)
                        if (!CheckFilePath(array[i], out log)) return false;
                log = null;
                FileName = path;
                FileNames = array;
                return true;
            }
            else
            {
                var str = Engine.Tool.OpenDialog(Filter ?? string.Empty, Directory.Exists(InitialDirectory) ? InitialDirectory : string.Empty);
                if (CheckFileExists && !Engine.File.Exists(str))
                {
                    log = $"File does not exists\nPath: {str}";
                    return false;
                }
                if (CheckPathExists && !CheckFilePath(str, out log)) return false;
                log = null;
                FileName = str;
                FileNames = new[] { str };
                return true;
            }
        }
    }
}

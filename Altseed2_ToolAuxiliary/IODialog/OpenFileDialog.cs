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
                if (AddExtension && !string.IsNullOrEmpty(DefaultExtension))
                    for (int i = 0; i < array.Length; i++)
                        if (array[i].IndexOfAny(Path.GetInvalidPathChars()) < 0 && !string.IsNullOrWhiteSpace(array[i]))
                        {
                            var ex = $".{DefaultExtension.TrimStart('.')}";
                            if (Path.GetExtension(array[i]).Length == 0) array[i] += ex;
                        }
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
                var path = Engine.Tool.OpenDialog(Filter ?? string.Empty, Directory.Exists(InitialDirectory) ? InitialDirectory : string.Empty);
                if (string.IsNullOrEmpty(path))
                {
                    log = null;
                    return false;
                }
                if (AddExtension && !string.IsNullOrEmpty(DefaultExtension) && path.IndexOfAny(Path.GetInvalidPathChars()) < 0 && !string.IsNullOrWhiteSpace(path))
                {
                    var ex = $".{DefaultExtension.TrimStart('.')}";
                    if (Path.GetExtension(path).Length == 0) path += ex;
                }
                if (CheckFileExists && !Engine.File.Exists(path))
                {
                    log = $"File does not exists\nPath: {path}";
                    return false;
                }
                if (CheckPathExists && !CheckFilePath(path, out log)) return false;
                log = null;
                FileName = path;
                FileNames = new[] { path };
                return true;
            }
        }
    }
}

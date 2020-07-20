using System;
using System.IO;
using System.Threading;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 1つの保存するファイルを選択するダイアログ
    /// </summary>
    [Serializable]
    public class SaveFileDialog : FileDialog
    {
        /// <summary>
        /// <see cref="SaveFileDialog"/>の新しいインスタンスを生成する
        /// </summary>
        public SaveFileDialog() { }
        public override bool ShowDialog(out string log)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) throw new InvalidOperationException("Main関数にSTAThreadAttributeが設定されていることを確認してください");
            var path = Engine.Tool.SaveDialog(Filter ?? string.Empty, Directory.Exists(InitialDirectory) ? InitialDirectory : string.Empty);
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
            return true;
        }
    }
}

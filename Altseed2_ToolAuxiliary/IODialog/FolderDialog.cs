using System;
using System.IO;
using System.Threading;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// フォルダーを選択するダイアログのクラス
    /// </summary>
    [Serializable]
    public class FolderDialog : DialogBase
    {
        /// <summary>
        /// 選択されたフォルダーのパスを取得または設定する
        /// </summary>
        public string SelectedPath { get; set; }
        /// <summary>
        /// <see cref="FolderDialog"/>の新しいインスタンスを生成する
        /// </summary>
        public FolderDialog() { }
        public override bool ShowDialog(out string log)
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA) throw new InvalidOperationException("Main関数にSTAThreadAttributeが設定されていることを確認してください");
            var path = Engine.Tool.PickFolder(Directory.Exists(InitialDirectory) ? InitialDirectory : string.Empty);
            if (string.IsNullOrEmpty(path))
            {
                log = null;
                return false;
            }
            if (CheckPathExists && !Directory.Exists(path))
            {
                log = $"Directory does not exist\nPath: {path}";
                return false;
            }
            log = null;
            SelectedPath = path;
            return true;
        }
    }
}

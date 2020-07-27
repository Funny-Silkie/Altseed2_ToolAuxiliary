using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ダイアログの基底クラス
    /// </summary>
    [Serializable]
    public abstract class DialogBase
    {
        /// <summary>
        /// 選択したパスが存在するかどうかを取得または設定する
        /// </summary>
        public bool CheckPathExists { get; set; }
        /// <summary>
        /// 開始ディレクトリを取得または設定する
        /// </summary>
        public string InitialDirectory { get; set; }
        /// <summary>
        /// <see cref="DialogBase"/>の新しいインスタンスを生成する
        /// </summary>
        protected DialogBase() { }
        /// <summary>
        /// <see cref="InitialDirectory"/>をカレントディレクトリに設定する
        /// </summary>
        public void SetCurrentDirectory()
        {
            InitialDirectory = Environment.CurrentDirectory;
        }
        /// <summary>
        /// ダイアログを開く
        /// </summary>
        /// <exception cref="InvalidOperationException">Main関数に<see cref="STAThreadAttribute"/>が設定されていない</exception>
        /// <returns>パスが更新されたらtrue，それ以外でfalse</returns>
        public bool ShowDialog() => ShowDialog(out _);
        /// <summary>
        /// ダイアログを開く
        /// </summary>
        /// <param name="log">エラーメッセージがある場合は出力されます</param>
        /// <exception cref="InvalidOperationException">Main関数に<see cref="STAThreadAttribute"/>が設定されていない</exception>
        /// <returns>パスが更新されたらtrue，それ以外でfalse</returns>
        public abstract bool ShowDialog(out string log);
    }
}

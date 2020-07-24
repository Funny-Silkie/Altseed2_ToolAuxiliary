using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="MenuBar"/>に登録されるアイテムのクラス
    /// </summary>
    [Serializable]
    public class MenuItem : ToolComponent
    {
        /// <summary>
        /// 選択可能かどうかを取得または設定する
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// チェックが付いているかどうかを取得または設定する
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// ショートカットを取得または設定する
        /// </summary>
        public string ShortCut { get; set; }
        /// <summary>
        /// クリックされた時に実行
        /// </summary>
        public event EventHandler Clicked;
        /// <summary>
        /// 既定の文字列とショートカットを持つ<see cref="MenuItem"/>の新しいインスタンスを生成する
        /// </summary>
        public MenuItem() : this(string.Empty, string.Empty) { }
        /// <summary>
        /// 指定した文字列とショートカットを持つ<see cref="MenuItem"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="shortCut">ショートカット</param>
        public MenuItem(string label, string shortCut)
        {
            Label = label;
            ShortCut = shortCut;
        }
        /// <summary>
        /// クリックされた時に実行
        /// </summary>
        /// <param name="e">与えられる<see cref="EventArgs"/>のインスタンス</param>
        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
        internal override void Update()
        {
            if (!Engine.Tool.MenuItem(Label ?? string.Empty, ShortCut ?? string.Empty, Selected, Enabled)) return;
            OnClicked(EventArgs.Empty);
        }
    }
}

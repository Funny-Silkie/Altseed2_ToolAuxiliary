using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// チェックボックスのクラス
    /// </summary>
    [Serializable]
    public class CheckBox : ToolComponent
    {
        /// <summary>
        /// チェックされているかどうかを取得または設定する
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 既定の情報を持つ<see cref="CheckBox"/>の新しいインスタンスを生成する
        /// </summary>
        public CheckBox() : this(string.Empty, false) { }
        /// <summary>
        /// 指定した情報を持つ<see cref="CheckBox"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="check">チェックされているかどうか</param>
        public CheckBox(string label, bool check)
        {
            Checked = check;
            Label = label;
        }
        /// <summary>
        /// <see cref="Checked"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<bool>> ChangeChecked;
        /// <summary>
        /// <see cref="Checked"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Checked"/>の変更前後が与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnChangeChecked(ToolValueEventArgs<bool> e)
        {
            ChangeChecked?.Invoke(this, e);
        }
        internal override void Update()
        {
            var c = Checked;
            if (!Engine.Tool.CheckBox(Label ?? string.Empty, ref c)) return;
            if (c == Checked) return;
            var old = Checked;
            Checked = c;
            OnChangeChecked(new ToolValueEventArgs<bool>(old, c));
        }
    }
}

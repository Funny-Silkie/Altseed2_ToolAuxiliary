using System;
using System.Collections.Generic;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 単一のラジオボタンのクラス
    /// </summary>
    [Serializable]
    public class SingleRadioButton : ButtonBase
    {
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 選択されているかどうかを取得または設定する
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// 既定の文字列を持つ<see cref="SingleRadioButton"/>の新しいインスタンスを生成する
        /// </summary>
        public SingleRadioButton() : this(string.Empty) { }
        /// <summary>
        /// 指定した文字列を持つ<see cref="SingleRadioButton"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        public SingleRadioButton(string label)
        {
            Label = label;
        }
        /// <summary>
        /// <see cref="Selected"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<bool>> ChangeSelected;
        /// <summary>
        /// <see cref="Selected"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Selected"/>の変更前後が与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnChangeSelected(ToolValueEventArgs<bool> e)
        {
            ChangeSelected?.Invoke(this, e);
        }
        internal override void Update()
        {
            if (!Engine.Tool.RadioButton(Label ?? string.Empty, Selected)) return;
            OnClicked(EventArgs.Empty);
            Selected = !Selected;
            OnChangeSelected(new ToolValueEventArgs<bool>(!Selected, Selected));
        }
    }
}

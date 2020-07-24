using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ボタンのツールコンポーネントの基底クラス
    /// </summary>
    [Serializable]
    public abstract class ButtonBase : ToolComponent
    {
        /// <summary>
        /// <see cref="ButtonBase"/>の新しいインスタンスを生成する
        /// </summary>
        protected ButtonBase() { }
        /// <summary>
        /// ボタンが押された時に実行
        /// </summary>
        public event EventHandler Clicked;
        /// <summary>
        /// ボタンが押された時に実行
        /// </summary>
        /// <param name="e">与えられる<see cref="EventArgs"/>のインスタンス</param>
        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
    }
}

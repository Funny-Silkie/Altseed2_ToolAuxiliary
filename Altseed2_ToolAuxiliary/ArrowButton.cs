using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ボタンのツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class ArrowButton : ButtonBase
    {
        /// <summary>
        /// 矢印の方向を取得または設定する
        /// </summary>
        public ToolDir Direction { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 既定の文字列と方向を持つ<see cref="ArrowButton"/>の新しいインスタンスを生成する
        /// </summary>
        public ArrowButton() : this(string.Empty, ToolDir.Right) { }
        /// <summary>
        /// 指定した文字列と方向を持つ<see cref="ArrowButton"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="direction">矢印の方向</param>
        public ArrowButton(string label, ToolDir direction)
        {
            Direction = direction;
            Label = label;
        }
        internal override void Update()
        {
            if (Direction == ToolDir.None) return;
            var result = Engine.Tool.ArrowButton(Label ?? string.Empty, Direction);
            if (result) OnClicked(EventArgs.Empty);
        }
    }
}

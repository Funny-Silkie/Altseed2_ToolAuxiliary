using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="Altseed2.Color"/>を設定するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class ColorEdit : ToolComponent
    {
        /// <summary>
        /// アルファ値も設定するかどうかを取得または設定する
        /// </summary>
        public bool EditAlpha { get; set; } = true;
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// 諸設定を取得または設定する
        /// </summary>
        public ToolColorEdit Flags { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 指定された色と文字列を持つ<see cref="ColorEdit"/>の新しいインスタンスを生成する
        /// </summary>
        public ColorEdit() : this(string.Empty, new Color(255, 255, 255, 255)) { }
        /// <summary>
        /// 指定された色と文字列を持つ<see cref="ColorEdit"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="value">色</param>
        public ColorEdit(string label, Color value)
        {
            Color = value;
            Label = label;
        }
        /// <summary>
        /// <see cref="Color"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<Color>> ColorChanged;
        /// <summary>
        /// <see cref="Color"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Color"/>の変更前後が与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnColorChanged(ToolValueEventArgs<Color> e)
        {
            ColorChanged?.Invoke(this, e);
        }
        internal override void Update()
        {
            var color = Color;
            var result = EditAlpha ? Engine.Tool.ColorEdit4(Label ?? string.Empty, ref color, Flags) : Engine.Tool.ColorEdit3(Label ?? string.Empty, ref color, Flags);
            if (!result) return;
            if (Color == color) return;
            var old = Color;
            Color = color;
            OnColorChanged(new ToolValueEventArgs<Color>(old, color));
        }
    }
}

using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// RGBAを設定するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public sealed class ColorEdit4 : ToolComponent
    {
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
        /// <see cref="Color"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<Color>> ColorChanged;
        /// <summary>
        /// 指定された色と文字列を持つ<see cref="ColorEdit4"/>の新しいインスタンスを生成する
        /// </summary>
        public ColorEdit4() : this(string.Empty, new Color(255, 255, 255, 255)) { }
        /// <summary>
        /// 指定された色と文字列を持つ<see cref="ColorEdit4"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="value">色</param>
        public ColorEdit4(string label, Color value)
        {
            Color = value;
            Label = label;
        }
        internal override void Update()
        {
            var color = Color;
            if (!Engine.Tool.ColorEdit4(Label ?? string.Empty, ref color, Flags)) return;
            if (Color == color) return;
            ColorChanged?.Invoke(this, new ToolValueEventArgs<Color>(Color, color));
            Color = color;
        }
    }
}

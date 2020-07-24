using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を1つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class DragFloat : SliderFloatBase
    {
        /// <summary>
        /// スライドの速さを取得または設定する
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 値を取得または設定する
        /// </summary>
        public float Value { get; set; }
        /// <summary>
        /// <see cref="Value"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> ValueChanged;
        /// <summary>
        /// 既定の値を用いて<see cref="DragFloat"/>の新しいインスタンスを生成する
        /// </summary>
        public DragFloat() : this(string.Empty, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="DragFloat"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value">初期値</param>
        public DragFloat(string label, float value)
        {
            Label = label;
            Value = value;
        }
        internal override void Update()
        {
            var value = Value;
            if (!Engine.Tool.DragFloat(Label ?? string.Empty, ref value, Speed, Min, Max)) return;
            if (Value == value) return;
            ValueChanged?.Invoke(this, new ToolValueEventArgs<float>(Value, value));
            Value = value;
        }
    }
}

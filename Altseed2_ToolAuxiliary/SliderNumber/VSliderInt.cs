using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="int"/>型の数字を格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class VSliderInt : SliderIntBase
    {
        /// <summary>
        /// サイズを取得または設定する
        /// </summary>
        public Vector2F Size { get; set; }
        /// <summary>
        /// 値を取得または設定する
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// 既定の値を用いて<see cref="VSliderInt"/>の新しいインスタンスを生成する
        /// </summary>
        public VSliderInt() : this(string.Empty, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="VSliderInt"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value">初期値</param>
        public VSliderInt(string label, int value)
        {
            Label = label;
            Value = value;
        }
        /// <summary>
        /// <see cref="Value"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<int>> ValueChanged;
        /// <summary>
        /// <see cref="Value"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Value"/>の変更前後を与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnValueChanged(ToolValueEventArgs<int> e)
        {
            ValueChanged?.Invoke(this, e);
        }
        internal override void Update()
        {
            var value = Value;
            if (!Engine.Tool.VSliderInt(Label ?? string.Empty, Size, ref value, Min, Max)) return;
            if (Value == value) return;
            var old = Value;
            Value = value;
            OnValueChanged(new ToolValueEventArgs<int>(old, value));
        }
    }
}

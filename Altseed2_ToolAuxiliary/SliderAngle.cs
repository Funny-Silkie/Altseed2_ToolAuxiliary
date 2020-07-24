using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 角度を格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class SliderAngle : ToolComponent
    {
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 値を取得または設定する
        /// </summary>
        public float Value
        {
            get => _value;
            set
            {
                if (_value == value) return;
                _value = MathHelper.Clamp(value, 360f, -360f);
            }
        }
        private float _value;
        /// <summary>
        /// 既定の値を用いて<see cref="SliderAngle"/>の新しいインスタンスを生成する
        /// </summary>
        public SliderAngle() : this(string.Empty, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="SliderAngle"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value">初期値</param>
        public SliderAngle(string label, float value)
        {
            Label = label;
            Value = value;
        }
        /// <summary>
        /// <see cref="Value"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> ValueChanged;
        /// <summary>
        /// <see cref="Value"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Value"/>の変更前後を与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnValueChanged(ToolValueEventArgs<float> e)
        {
            ValueChanged?.Invoke(this, e);
        }
        internal override void Update()
        {
            var value = Value;
            if (!Engine.Tool.SliderAngle(Label ?? string.Empty, ref value)) return;
            if (Value == value) return;
            var old = Value;
            Value = value;
            OnValueChanged(new ToolValueEventArgs<float>(old, value));
        }
    }
}

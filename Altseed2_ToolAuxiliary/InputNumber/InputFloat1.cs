using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を1つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class InputFloat1 : ToolComponent
    {
        /// <summary>
        /// 最大値を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Min"/>未満</exception>
        public float Max
        {
            get => _max;
            set
            {
                if (_max == value) return;
                if (value < _min) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Min)}未満です\n許容される範囲：{_min}～float.MaxValue({float.MaxValue})\n実際の値：{value}");
                _max = value;
            }
        }
        private float _max = float.MaxValue;
        /// <summary>
        /// 最小値を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Max"/>を超える</exception>
        public float Min
        {
            get => _min;
            set
            {
                if (_min == value) return;
                if (_max < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Max)}を超えます\n許容される範囲：float.MinValue({float.MinValue})～{_max}\n実際の値：{value}");
                _min = value;
            }
        }
        private float _min = float.MinValue;
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 値を取得または設定する
        /// </summary>
        public float Value { get; set; }
        /// <summary>
        /// 既定の値を用いて<see cref="InputFloat1"/>の新しいインスタンスを生成する
        /// </summary>
        public InputFloat1() : this(string.Empty, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="InputFloat1"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value">初期値</param>
        public InputFloat1(string label, float value)
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
            if (!Engine.Tool.InputFloat(Label ?? string.Empty, ref value)) return;
            value = MathHelper.Clamp(value, _max, _min);
            if (Value == value) return;
            var old = Value;
            Value = value;
            OnValueChanged(new ToolValueEventArgs<float>(old, value));
        }
    }
}

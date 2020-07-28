using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を4つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class InputFloat4 : ToolComponent
    {
        /// <summary>
        /// 最大値1を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Min1"/>未満</exception>
        public float Max1
        {
            get => _max1;
            set
            {
                if (_max1 == value) return;
                if (value < _min1) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Min1)}未満です\n許容される範囲：{_min1}～float.MaxValue({float.MaxValue})\n実際の値：{value}");
                _max1 = value;
            }
        }
        private float _max1 = float.MaxValue;
        /// <summary>
        /// 最大値2を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Min2"/>未満</exception>
        public float Max2
        {
            get => _max2;
            set
            {
                if (_max2 == value) return;
                if (value < _min2) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Min2)}未満です\n許容される範囲：{_min2}～float.MaxValue({float.MaxValue})\n実際の値：{value}");
                _max2 = value;
            }
        }
        private float _max2 = float.MaxValue;
        /// <summary>
        /// 最大値3を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Min3"/>未満</exception>
        public float Max3
        {
            get => _max3;
            set
            {
                if (_max3 == value) return;
                if (value < _min3) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Min3)}未満です\n許容される範囲：{_min3}～float.MaxValue({float.MaxValue})\n実際の値：{value}");
                _max3 = value;
            }
        }
        private float _max3 = float.MaxValue;
        /// <summary>
        /// 最大値4を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Min4"/>未満</exception>
        public float Max4
        {
            get => _max4;
            set
            {
                if (_max4 == value) return;
                if (value < _min4) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Min4)}未満です\n許容される範囲：{_min4}～float.MaxValue({float.MaxValue})\n実際の値：{value}");
                _max4 = value;
            }
        }
        private float _max4 = float.MaxValue;
        /// <summary>
        /// 最小値1を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Max1"/>を超える</exception>
        public float Min1
        {
            get => _min1;
            set
            {
                if (_min1 == value) return;
                if (_max1 < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Max1)}を超えます\n許容される範囲：float.MinValue({float.MinValue})～{_max1}\n実際の値：{value}");
                _min1 = value;
            }
        }
        private float _min1 = float.MinValue;
        /// <summary>
        /// 最小値2を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Max2"/>を超える</exception>
        public float Min2
        {
            get => _min2;
            set
            {
                if (_min2 == value) return;
                if (_max2 < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Max2)}を超えます\n許容される範囲：float.MinValue({float.MinValue})～{_max2}\n実際の値：{value}");
                _min2 = value;
            }
        }
        private float _min2 = float.MinValue;
        /// <summary>
        /// 最小値3を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Max3"/>を超える</exception>
        public float Min3
        {
            get => _min3;
            set
            {
                if (_min3 == value) return;
                if (_max3 < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Max3)}を超えます\n許容される範囲：float.MinValue({float.MinValue})～{_max3}\n実際の値：{value}");
                _min3 = value;
            }
        }
        private float _min3 = float.MinValue;
        /// <summary>
        /// 最小値4を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が<see cref="Max4"/>を超える</exception>
        public float Min4
        {
            get => _min4;
            set
            {
                if (_min4 == value) return;
                if (_max4 < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が{nameof(Max4)}を超えます\n許容される範囲：float.MinValue({float.MinValue})～{_max4}\n実際の値：{value}");
                _min4 = value;
            }
        }
        private float _min4 = float.MinValue;
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 値1を取得または設定する
        /// </summary>
        public float Value1 { get; set; }
        /// <summary>
        /// 値2を取得または設定する
        /// </summary>
        public float Value2 { get; set; }
        /// <summary>
        /// 値3を取得または設定する
        /// </summary>
        public float Value3 { get; set; }
        /// <summary>
        /// 値4を取得または設定する
        /// </summary>
        public float Value4 { get; set; }
        /// <summary>
        /// 既定の値を用いて<see cref="InputFloat4"/>の新しいインスタンスを生成する
        /// </summary>
        public InputFloat4() : this(string.Empty, default, default, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="InputFloat4"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value1">初期値1</param>
        /// <param name="value2">初期値2</param>
        /// <param name="value3">初期値3</param>
        /// <param name="value4">初期値4</param>
        public InputFloat4(string label, float value1, float value2, float value3, float value4)
        {
            Label = label;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
        }
        /// <summary>
        /// <see cref="Value1"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> Value1Changed;
        /// <summary>
        /// <see cref="Value2"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> Value2Changed;
        /// <summary>
        /// <see cref="Value3"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> Value3Changed;
        /// <summary>
        /// <see cref="Value4"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> Value4Changed;
        /// <summary>
        /// <see cref="Value1"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Value1"/>の変更前後を与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnValue1Changed(ToolValueEventArgs<float> e)
        {
            Value1Changed?.Invoke(this, e);
        }
        /// <summary>
        /// <see cref="Value2"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Value2"/>の変更前後を与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnValue2Changed(ToolValueEventArgs<float> e)
        {
            Value2Changed?.Invoke(this, e);
        }
        /// <summary>
        /// <see cref="Value3"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Value3"/>の変更前後を与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnValue3Changed(ToolValueEventArgs<float> e)
        {
            Value3Changed?.Invoke(this, e);
        }
        /// <summary>
        /// <see cref="Value4"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Value4"/>の変更前後を与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnValue4Changed(ToolValueEventArgs<float> e)
        {
            Value4Changed?.Invoke(this, e);
        }
        internal override void Update()
        {
            var array = new[] { Value1, Value2, Value3, Value4 };
            if (!Engine.Tool.InputFloat4(Label ?? string.Empty, array)) return;
            array[0] = MathHelper.Clamp(array[0], _max1, _min1);
            array[1] = MathHelper.Clamp(array[1], _max2, _min2);
            array[2] = MathHelper.Clamp(array[2], _max3, _min3);
            array[3] = MathHelper.Clamp(array[3], _max4, _min4);
            if (Value1 != array[0])
            {
                var old = Value1;
                Value1 = array[0];
                OnValue1Changed(new ToolValueEventArgs<float>(old, array[0]));
            }
            if (Value2 != array[1])
            {
                var old = Value2;
                Value2 = array[1];                
                OnValue2Changed(new ToolValueEventArgs<float>(old, array[1]));
            }
            if (Value3 != array[2])
            {
                var old = Value3;
                Value3 = array[2];                
                OnValue3Changed(new ToolValueEventArgs<float>(old, array[2]));
            }
            if (Value4 != array[3])
            {
                var old = Value4;
                Value4 = array[3];                
                OnValue4Changed(new ToolValueEventArgs<float>(old, array[3]));
            }
        }
    }
}

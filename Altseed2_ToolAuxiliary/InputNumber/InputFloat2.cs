﻿using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を2つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class InputFloat2 : ToolComponent
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
        /// 既定の値を用いて<see cref="InputFloat2"/>の新しいインスタンスを生成する
        /// </summary>
        public InputFloat2() : this(string.Empty, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="InputFloat2"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value1">初期値1</param>
        /// <param name="value2">初期値2</param>
        public InputFloat2(string label, float value1, float value2)
        {
            Label = label;
            Value1 = value1;
            Value2 = value2;
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
        internal override void Update()
        {
            var array = new[] { Value1, Value2 };
            if (!Engine.Tool.InputFloat2(Label ?? string.Empty, array)) return;
            array[0] = MathHelper.Clamp(array[0], _max1, _min1);
            array[1] = MathHelper.Clamp(array[1], _max2, _min2);
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
        }
    }
}

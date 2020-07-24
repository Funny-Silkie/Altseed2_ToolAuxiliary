﻿using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="int"/>型の数字を1つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class SliderInt1 : SliderIntBase
    {
        /// <summary>
        /// スライドの速さを取得または設定する
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 値を取得または設定する
        /// </summary>
        public int Value { get; set; }
        /// <summary>
        /// <see cref="Value"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<int>> ValueChanged;
        /// <summary>
        /// 既定の値を用いて<see cref="SliderInt1"/>の新しいインスタンスを生成する
        /// </summary>
        public SliderInt1() : this(string.Empty, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="SliderInt1"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value">初期値</param>
        public SliderInt1(string label, int value)
        {
            Label = label;
            Value = value;
        }
        internal override void Update()
        {
            var value = Value;
            if (!Engine.Tool.SliderInt(Label ?? string.Empty, ref value, Speed, Min, Max)) return;
            if (Value == value) return;
            ValueChanged?.Invoke(this, new ToolValueEventArgs<int>(Value, value));
            Value = value;
        }
    }
}

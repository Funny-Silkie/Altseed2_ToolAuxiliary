﻿using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を2つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class SliderFloat2 : SliderFloatBase
    {
        /// <summary>
        /// スライドの速さを取得または設定する
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 値1を取得または設定する
        /// </summary>
        public float Value1 { get; set; }
        /// <summary>
        /// 値2を取得または設定する
        /// </summary>
        public float Value2 { get; set; }
        /// <summary>
        /// 既定の値を用いて<see cref="SliderFloat2"/>の新しいインスタンスを生成する
        /// </summary>
        public SliderFloat2() : this(string.Empty, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="SliderFloat2"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value1">初期値1</param>
        /// <param name="value2">初期値2</param>
        public SliderFloat2(string label, float value1, float value2)
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
            if (!Engine.Tool.SliderFloat2(Label ?? string.Empty, array, Speed, Min, Max)) return;
            array[0] = MathHelper.Clamp(array[0], Max, Min);
            array[1] = MathHelper.Clamp(array[1], Max, Min);
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

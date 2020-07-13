using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を3つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public sealed class SliderFloat3 : SliderFloatBase
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
        /// 値3を取得または設定する
        /// </summary>
        public float Value3 { get; set; }
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
        /// 既定の値を用いて<see cref="SliderFloat3"/>の新しいインスタンスを生成する
        /// </summary>
        public SliderFloat3() : this(string.Empty, default, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="SliderFloat3"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value1">初期値1</param>
        /// <param name="value2">初期値2</param>
        /// <param name="value3">初期値3</param>
        public SliderFloat3(string label, float value1, float value2, float value3)
        {
            Label = label;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }
        internal override void Update()
        {
            var array = new[] { Value1, Value2, Value3 };
            if (!Engine.Tool.SliderFloat3(Label ?? string.Empty, array, Speed, Min, Max)) return;
            if (Value1 != array[0])
            {
                Value1Changed?.Invoke(this, new ToolValueEventArgs<float>(Value1, array[0]));
                Value1 = array[0];
            }
            if (Value2 != array[1])
            {
                Value2Changed?.Invoke(this, new ToolValueEventArgs<float>(Value2, array[1]));
                Value2 = array[1];
            }
            if (Value3 != array[2])
            {
                Value3Changed?.Invoke(this, new ToolValueEventArgs<float>(Value3, array[2]));
                Value3 = array[2];
            }
        }
    }
}

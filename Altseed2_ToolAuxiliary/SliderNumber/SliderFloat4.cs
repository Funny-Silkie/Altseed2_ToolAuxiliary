using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の数字を4つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class SliderFloat4 : SliderFloatBase
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
        /// 値4を取得または設定する
        /// </summary>
        public float Value4 { get; set; }
        /// <summary>
        /// 既定の値を用いて<see cref="SliderFloat4"/>の新しいインスタンスを生成する
        /// </summary>
        public SliderFloat4() : this(string.Empty, default, default, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="SliderFloat4"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value1">初期値1</param>
        /// <param name="value2">初期値2</param>
        /// <param name="value3">初期値3</param>
        /// <param name="value4">初期値4</param>
        public SliderFloat4(string label, float value1, float value2, float value3, float value4)
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
            if (!Engine.Tool.SliderFloat4(Label ?? string.Empty, array, Speed, Min, Max)) return;
            array[0] = MathHelper.Clamp(array[0], Max, Min);
            array[1] = MathHelper.Clamp(array[1], Max, Min);
            array[2] = MathHelper.Clamp(array[2], Max, Min);
            array[3] = MathHelper.Clamp(array[3], Max, Min);
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

using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="int"/>型の数字を2つ格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public sealed class SliderInt2 : SliderIntBase
    {
        /// <summary>
        /// スライドの速さを取得または設定する
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 値1を取得または設定する
        /// </summary>
        public int Value1 { get; set; }
        /// <summary>
        /// 値2を取得または設定する
        /// </summary>
        public int Value2 { get; set; }
        /// <summary>
        /// <see cref="Value1"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<int>> Value1Changed;
        /// <summary>
        /// <see cref="Value2"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<int>> Value2Changed;
        /// <summary>
        /// 既定の値を用いて<see cref="SliderInt2"/>の新しいインスタンスを生成する
        /// </summary>
        public SliderInt2() : this(string.Empty, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="SliderInt2"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="value1">初期値1</param>
        /// <param name="value2">初期値2</param>
        public SliderInt2(string label, int value1, int value2)
        {
            Label = label;
            Value1 = value1;
            Value2 = value2;
        }
        internal override void Update()
        {
            var array = new[] { Value1, Value2 };
            if (!Engine.Tool.SliderInt2(Label ?? string.Empty, array, Speed, Min, Max)) return;
            if (Value1 != array[0])
            {
                Value1Changed?.Invoke(this, new ToolValueEventArgs<int>(Value1, array[0]));
                Value1 = array[0];
            }
            if (Value2 != array[1])
            {
                Value2Changed?.Invoke(this, new ToolValueEventArgs<int>(Value2, array[1]));
                Value2 = array[1];
            }
        }
    }
}

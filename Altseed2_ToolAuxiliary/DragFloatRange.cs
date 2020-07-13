using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="float"/>型の範囲を格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public sealed class DragFloatRange : SliderFloatBase
    {
        /// <summary>
        /// スライドの速さを取得または設定する
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// 大きい方の値を取得または設定する
        /// </summary>
        public float CurrentMax
        {
            get => _currentMax;
            set
            {
                if (_currentMax == value) return;
                _currentMax = MathHelper.Clamp(value, Max, _currentMin);
            }
        }
        private float _currentMax = RuledMaximum;
        /// <summary>
        /// 小さい方の値を取得または設定する
        /// </summary>
        public float CurrentMin
        {
            get => _currentMin;
            set
            {
                if (_currentMin == value) return;
                _currentMin = MathHelper.Clamp(value, _currentMax, Min);
            }
        }
        private float _currentMin = RuledMinimum;
        /// <summary>
        /// <see cref="CurrentMax"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> CurrentMaxChanged;
        /// <summary>
        /// <see cref="CurrentMin"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<float>> CurrentMinChanged;
        /// <summary>
        /// 既定の値を用いて<see cref="DragFloatRange"/>の新しいインスタンスを生成する
        /// </summary>
        public DragFloatRange() : this(string.Empty, default, default) { }
        /// <summary>
        /// 指定した値を用いて<see cref="DragFloatRange"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="currentMin">小さい方の値</param>
        /// <param name="currentMax">大きい方の値</param>
        public DragFloatRange(string label, float currentMin, float currentMax)
        {
            Label = label;
            CurrentMin = currentMin;
            CurrentMax = currentMax;
        }
        internal override void Update()
        {
            var currentMin = _currentMin;
            var currentMax = _currentMax;
            if (!Engine.Tool.DragFloatRange2(Label ?? string.Empty, ref currentMin, ref currentMax, Speed, Min, Max)) return;
            if (_currentMin != currentMin)
            {
                CurrentMinChanged?.Invoke(this, new ToolValueEventArgs<float>(_currentMin, currentMin));
                _currentMin = currentMin;
            }
            if (_currentMax != currentMax)
            {
                CurrentMaxChanged?.Invoke(this, new ToolValueEventArgs<float>(_currentMax, currentMax));
                _currentMax = currentMax;
            }
        }
    }
}

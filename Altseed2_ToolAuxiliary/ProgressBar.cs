using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ゲージを表示するバーのクラス
    /// </summary>
    [Serializable]
    public class ProgressBar : ToolComponent
    {
        private const float NegativeValue = -100;
        private const float PositiveValue = 100;
        /// <summary>
        /// 表示する文字列で<see cref="Progress"/>の値も含めるかどうかを取得または設定する
        /// </summary>
        public bool AddProgressValue { get; set; }
        /// <summary>
        /// 1フレーム毎に増減する値(百分率)を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が-100未満または100より大きい</exception>
        public float Function
        {
            get => _function;
            set
            {
                if (_function == value) return;
                if (value < NegativeValue || PositiveValue < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が許容範囲外です\n許容される範囲：-100～100\n実際の値：{value}");
                _function = value;
            }
        }
        private float _function;
        /// <summary>
        /// 進行度合い(百分率)を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が0未満または100より大きい</exception>
        public float Progress
        {
            get => _progress * PositiveValue;
            set
            {
                if (_progress == value) return;
                if (value < 0 || PositiveValue < value) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が許容範囲外です\n許容される範囲：0～100\n実際の値：{value}");
                _progress = value / PositiveValue;
            }
        }
        private float _progress;
        /// <summary>
        /// 大きさを取得または設定する
        /// </summary>
        public Vector2F Size { get; set; }
        /// <summary>
        /// 表示する文字列を取得または設定する
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// <see cref="Progress"/>がフレーム毎に<see cref="Function"/>分変化するかどうかを取得または設定する
        /// </summary>
        public bool ValueChanged { get; set; } = true;
        /// <summary>
        /// 既定の文字列と値，増加率を備えた<see cref="ProgressBar"/>の新しいインスタンスを生成する
        /// </summary>
        public ProgressBar() : this(string.Empty, 0f, 1f) { }
        /// <summary>
        /// 指定した文字列と値，増加率を備えた<see cref="ProgressBar"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="text">表示する文字列</param>
        /// <param name="initValue">初期の値 [0,100]</param>
        /// <param name="function">増加量 [-100,100]</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="initValue"/>または<paramref name="function"/>が許容範囲外</exception>
        public ProgressBar(string text, float initValue, float function)
        {
            Function = function;
            Progress = initValue;
            Text = text;
        }
        internal override void Update()
        {
            Engine.Tool.ProgressBar(_progress, Size, AddProgressValue ? $"{Text ?? string.Empty} - {MathF.Round(_progress * PositiveValue, 1)}%" : (Text ?? string.Empty));
            if (ValueChanged) _progress += _function / PositiveValue;
            _progress = MathHelper.Clamp(_progress, 1f, 0f);
        }
    }
}

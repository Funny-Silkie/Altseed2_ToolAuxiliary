using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 文字を格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class InputText : ToolComponent
    {
        /// <summary>
        /// 描画時の設定を取得または設定する
        /// </summary>
        public ToolInputText Flags { get; set; }
        /// <summary>
        /// 表示される表題の文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 文字列の最大長を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が0未満</exception>
        public int MaxLength
        {
            get => _maxLength;
            set
            {
                if (_maxLength == value) return;
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が0未満です\n設定しようとした値：{value}");
                _maxLength = value;
            }
        }
        private int _maxLength = int.MaxValue;
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 既定の文字列を表示する<see cref="InputText"/>の新しいインスタンスを生成する
        /// </summary>
        public InputText() : this(string.Empty, string.Empty) { }
        /// <summary>
        /// 指定した文字列を表示する<see cref="InputText"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表題の文字列</param>
        /// <param name="text">表示する文字列</param>
        public InputText(string label, string text)
        {
            Label = label;
            Text = text;
        }
        /// <summary>
        /// <see cref="Text"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<string>> TextChanged;
        /// <summary>
        /// <see cref="Text"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Text"/>の変更前後が与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnTextChanged(ToolValueEventArgs<string> e)
        {
            TextChanged?.Invoke(this, e);
        }
        internal override void Update()
        {
            var text = Text ?? string.Empty;
            text = Engine.Tool.InputText(Label ?? string.Empty, text, _maxLength, Flags);
            if (text == null || Text == text) return;
            var old = Text;
            Text = text;
            OnTextChanged(new ToolValueEventArgs<string>(old, text));
        }
    }
}

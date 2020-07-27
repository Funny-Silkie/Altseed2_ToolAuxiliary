using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 文字を格納するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class InputText : ToolComponent, IToolInputText
    {
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
        #region IToolInputText
        private bool flagChanged = true;
        /// <summary>
        /// マウスでフォーカスしたときテキスト全体を選択するかどうか取得または設定する
        /// </summary>
        public bool AutoSelectAll
        {
            get => _autoSelectAll;
            set
            {
                if (_autoSelectAll == value) return;
                _autoSelectAll = value;
                flagChanged = true;
            }
        }
        private bool _autoSelectAll;
        /// <summary>
        /// 入力できる文字の種類を取得または設定する
        /// </summary>
        public IToolInputText.InputTextCharType CharType
        {
            get => _charType;
            set
            {
                if (_charType == value) return;
                _charType = value;
                flagChanged = true;
            }
        }
        private IToolInputText.InputTextCharType _charType;
        /// <summary>
        /// 横方向のスクロールが行われるかどうかを取得または設定する
        /// </summary>
        public bool EnableHorizontalScroll
        {
            get => _enableHorizonalScroll;
            set
            {
                if (_enableHorizonalScroll == value) return;
                _enableHorizonalScroll = value;
                flagChanged = true;
            }
        }
        private bool _enableHorizonalScroll = true;
        internal ToolInputTextFlags Flags
        {
            get
            {
                if (flagChanged)
                {
                    _flags = FlagCalculator.CalcToolInputText(this);
                    flagChanged = false;
                }
                return _flags;
            }
        }
        private ToolInputTextFlags _flags;
        ToolInputTextFlags IToolInputText.Flags => Flags;
        /// <summary>
        /// 上書きモードかどうかを取得または設定する
        /// </summary>
        /// <remarks>falseの時は挿入モード</remarks>
        public bool IsOverwriteMode
        {
            get => _isOverrideMode;
            set
            {
                if (_isOverrideMode == value) return;
                _isOverrideMode = value;
                flagChanged = true;
            }
        }
        private bool _isOverrideMode;
        /// <summary>
        /// 入力した文字列を*で隠すかどうかを取得または設定する
        /// </summary>
        public bool IsPasswordMode
        {
            get => _isPasswordMode;
            set
            {
                if (_isPasswordMode == value) return;
                _isPasswordMode = value;
                flagChanged = true;
            }
        }
        private bool _isPasswordMode;
        /// <summary>
        /// 読み取り専用かどうかを取得または設定する
        /// </summary>
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                if (_isReadOnly == value) return;
                _isReadOnly = value;
                flagChanged = true;
            }
        }
        private bool _isReadOnly;
        /// <summary>
        /// スペースやタブスペースを禁じるかどうかを取得または設定する
        /// </summary>
        /// <remarks>trueの時は<see cref="TabSpace"/>よりも優先される</remarks>
        public bool NoBlank
        {
            get => _noBlank;
            set
            {
                if (_noBlank == value) return;
                _noBlank = value;
                flagChanged = true;
            }
        }
        private bool _noBlank;
        /// <summary>
        /// Tabキーを押したときタブスペースを挿入するかどうかを取得または設定する
        /// </summary>
        public bool TabSpace
        {
            get => _tabSpace;
            set
            {
                if (_tabSpace == value) return;
                _tabSpace = value;
                flagChanged = true;
            }
        }
        private bool _tabSpace;
        /// <summary>
        /// 小文字を大文字に自動変換するかどうかを取得または設定する
        /// </summary>
        public bool UpperCaseFilter
        {
            get => _upperCaseFilter;
            set
            {
                if (_upperCaseFilter == value) return;
                _upperCaseFilter = value;
                flagChanged = true;
            }
        }
        private bool _upperCaseFilter;
        #endregion
    }
}

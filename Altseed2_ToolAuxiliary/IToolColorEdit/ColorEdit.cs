using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="Altseed2.Color"/>を設定するツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class ColorEdit : ToolComponent, IToolColorEdit
    {
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 指定された色と文字列を持つ<see cref="ColorEdit"/>の新しいインスタンスを生成する
        /// </summary>
        public ColorEdit() : this(string.Empty, new Color(255, 255, 255, 255)) { }
        /// <summary>
        /// 指定された色と文字列を持つ<see cref="ColorEdit"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="value">色</param>
        public ColorEdit(string label, Color value)
        {
            Color = value;
            Label = label;
        }
        /// <summary>
        /// <see cref="Color"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<Color>> ColorChanged;
        /// <summary>
        /// <see cref="Color"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Color"/>の変更前後が与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnColorChanged(ToolValueEventArgs<Color> e)
        {
            ColorChanged?.Invoke(this, e);
        }
        internal override void Update()
        {
            var color = Color;
            if (!Engine.Tool.ColorEdit4(Label ?? string.Empty, ref color, Flags)) return;
            if (Color == color) return;
            var old = Color;
            Color = color;
            OnColorChanged(new ToolValueEventArgs<Color>(old, color));
        }
        #region IToolColorEdit
        private bool flagChanged = true;
        /// <summary>
        /// アルファ値を編集するかどうかを取得または設定する
        /// </summary>
        public bool EditAlpha
        {
            get => _editAlpha;
            set
            {
                if (_editAlpha == value) return;
                _editAlpha = value;
                flagChanged = true;
            }
        }
        private bool _editAlpha = true;
        internal ToolColorEdit Flags
        {
            get
            {
                if (flagChanged)
                {
                    _flag = FlagCalculator.CalcToolColorEdit(this);
                    flagChanged = false;
                }
                return _flag;
            }
        }
        private ToolColorEdit _flag;
        ToolColorEdit IToolColorEdit.Flags => Flags;
        /// <summary>
        /// ウィンドウに表示されるインプットの種類を取得または設定する
        /// </summary>
        public IToolColorEdit.ColorEditInputType InputType
        {
            get => _inputType;
            set
            {
                if (_inputType == value) return;
                _inputType = value;
                flagChanged = true;
            }
        }
        private IToolColorEdit.ColorEditInputType _inputType = IToolColorEdit.ColorEditInputType.All;
        /// <summary>
        /// 最初に表示されるカラーエディタの種類を取得または設定する
        /// </summary>
        public IToolColorEdit.ColorEditPickerType PickerType
        {
            get => _pickerType;
            set
            {
                if (_pickerType == value) return;
                _pickerType = value;
                flagChanged = true;
            }
        }
        private IToolColorEdit.ColorEditPickerType _pickerType;
        /// <summary>
        /// アルファ値のバーを表示するかどうかを取得または設定する
        /// </summary>
        public bool ShowAlphaBar
        {
            get => _showAlphaBar;
            set
            {
                if (_showAlphaBar == value) return;
                _showAlphaBar = value;
                flagChanged = true;
            }
        }
        private bool _showAlphaBar = true;
        /// <summary>
        /// 入力を右クリックしたときにオプションを表示するかどうかを取得または設定する
        /// </summary>
        public bool ShowInputOption
        {
            get => _showInputOption;
            set
            {
                if (_showInputOption == value) return;
                _showInputOption = value;
                flagChanged = true;
            }
        }
        private bool _showInputOption = true;
        /// <summary>
        /// テキストラベルを表示するかどうかを取得または設定する
        /// </summary>
        public bool ShowLabel
        {
            get => _showLabel;
            set
            {
                if (_showLabel == value) return;
                _showLabel = value;
                flagChanged = true;
            }
        }
        private bool _showLabel = true;
        /// <summary>
        /// 詳細を設定するウィンドウを表示するかどうかを取得または設定する
        /// </summary>
        public bool ShowPicker
        {
            get => _showPicker;
            set
            {
                if (_showPicker == value) return;
                _showPicker = value;
                flagChanged = true;
            }
        }
        private bool _showPicker = true;
        /// <summary>
        /// 入力の右側の色のプレビューを表示するかどうかを取得または設定する
        /// </summary>
        public bool ShowSmallPreview
        {
            get => _showSmallPreview;
            set
            {
                if (_showSmallPreview == value) return;
                _showSmallPreview = value;
                flagChanged = true;
            }
        }
        private bool _showSmallPreview = true;
        /// <summary>
        /// 扱う値のタイプを取得または設定する
        /// </summary>
        public IToolColorEdit.ColorEditValueType ValueType
        {
            get => _valueType;
            set
            {
                if (_valueType == value) return;
                _valueType = value;
                flagChanged = true;
            }
        }
        private IToolColorEdit.ColorEditValueType _valueType;
        #endregion
    }
}

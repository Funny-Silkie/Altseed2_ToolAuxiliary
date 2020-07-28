using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 色ボタンのクラス
    /// </summary>
    [Serializable]
    public class ColorButton : ButtonBase, IToolColorEdit
    {
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// 表示する説明を取得または設定する
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// サイズを取得または設定する
        /// </summary>
        public Vector2F Size { get; set; }
        /// <summary>
        /// 既定の説明，色，大きさを持つ<see cref="ColorButton"/>の新しいインスタンスを生成する
        /// </summary>
        public ColorButton() : this(string.Empty, new Color(255, 255, 255), new Vector2F(30f, 30f)) { }
        /// <summary>
        /// 指定した説明，色，大きさを持つ<see cref="ColorButton"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="description">説明</param>
        /// <param name="color">色</param>
        /// <param name="size">大きさ</param>
        public ColorButton(string description, Color color, Vector2F size)
        {
            Description = description;
            Color = color;
            Size = size;
        }
        internal override void Update()
        {
            var color = Color;
            if (!Engine.Tool.ColorButton(Description ?? string.Empty, ref color, Flags, Size)) return;
            OnClicked(EventArgs.Empty);
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
        internal ToolColorEditFlags Flags
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
        private ToolColorEditFlags _flag;
        ToolColorEditFlags IToolColorEdit.Flags => Flags;
        IToolColorEdit.ColorEditInputType IToolColorEdit.InputType => default;
        IToolColorEdit.ColorEditPickerType IToolColorEdit.PickerType => default;
        bool IToolColorEdit.ShowAlphaBar => false;
        bool IToolColorEdit.ShowInputOption => false;
        bool IToolColorEdit.ShowLabel => false;
        bool IToolColorEdit.ShowPicker => false;
        bool IToolColorEdit.ShowSmallPreview => false;
        IToolColorEdit.ColorEditValueType IToolColorEdit.ValueType => default;
        #endregion
    }
}

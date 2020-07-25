using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 選択可能ラベルのクラス
    /// </summary>
    [Serializable]
    public class Selectable : ToolComponent, IToolSelectable
    {
        /// <summary>
        /// チェックされているかどうかを取得または設定する
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 既定の情報を持つ<see cref="Selectable"/>の新しいインスタンスを生成する
        /// </summary>
        public Selectable() : this(string.Empty, false) { }
        /// <summary>
        /// 指定した情報を持つ<see cref="Selectable"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="check">チェックされているかどうか</param>
        public Selectable(string label, bool check)
        {
            Checked = check;
            Label = label;
        }
        /// <summary>
        /// <see cref="Checked"/>が変更された時に実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<bool>> ChangeChecked;
        /// <summary>
        /// <see cref="Checked"/>が変更された時に実行
        /// </summary>
        /// <param name="e"><see cref="Checked"/>の変更前後が与えられた<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnChangeChecked(ToolValueEventArgs<bool> e)
        {
            ChangeChecked?.Invoke(this, e);
        }
        internal override void Update()
        {
            var c = Checked;
            if (!Engine.Tool.Selectable(Label ?? string.Empty, ref c, Flags)) return;
            if (c == Checked) return;
            var old = Checked;
            Checked = c;
            OnChangeChecked(new ToolValueEventArgs<bool>(old, c));
        }
        #region IToolSelectable
        private bool flagChanged = true;
        /// <summary>
        /// ダブルクリックでもイベントが作動するかどうかを取得または設定する
        /// </summary>
        public bool AllowDoubleClick
        {
            get => _allowDoubleClick;
            set
            {
                if (_allowDoubleClick == value) return;
                _allowDoubleClick = value;
                flagChanged = true;
            }
        }
        private bool _allowDoubleClick;
        /// <summary>
        /// 選択可能かどうかを取得または設定する
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
            set
            {
                if (_enabled == value) return;
                _enabled = value;
                flagChanged = true;
            }
        }
        private bool _enabled = true;
        internal ToolSelectable Flags
        {
            get
            {
                if (flagChanged)
                {
                    _flags = FlagCalculator.CalcToolSelectable(this);
                    flagChanged = false;
                }
                return _flags;
            }
        }
        private ToolSelectable _flags;
        ToolSelectable IToolSelectable.Flags => Flags;
        /// <summary>
        /// クリック時に親ポップアップウィンドウを開いたままにするかどうかを取得または設定する
        /// </summary>
        public bool KeepOpenPopups
        {
            get => _keepOpenPopups;
            set
            {
                if (_keepOpenPopups == value) return;
                _keepOpenPopups = value;
                flagChanged = true;
            }
        }
        private bool _keepOpenPopups;
        #endregion
    }
}

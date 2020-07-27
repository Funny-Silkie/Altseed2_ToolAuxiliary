using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 木構造のノードのクラス
    /// </summary>
    [Serializable]
    public class TreeNode : ToolComponent, IComponentRegisterable<ToolComponent>, IToolTreeNode
    {
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        IEnumerable<ToolComponent> IComponentRegisterable<ToolComponent>.Components => Components;
        private readonly ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// クリックされた時に実行
        /// </summary>
        public event EventHandler Clicked;
        /// <summary>
        /// 既定の文字列を備えた<see cref="TreeNode"/>の新しいインスタンスを生成する
        /// </summary>
        public TreeNode() : this(string.Empty) { }
        /// <summary>
        /// 指定した文字列を備えた<see cref="TreeNode"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        public TreeNode(string label)
        {
            Label = label;
        }
        /// <summary>
        /// コンポーネントを追加する
        /// </summary>
        /// <param name="component">追加するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を重複なく追加出来たらtrue，それ以外でfalse</returns>
        public bool AddComponent(ToolComponent component) => container.Add(component);
        /// <summary>
        /// 登録されているコンポーネントを全て削除する
        /// </summary>
        public void ClearComponents() => container.Clear();
        /// <summary>
        /// クリックされた時に実行
        /// </summary>
        /// <param name="e">与えられる<see cref="EventArgs"/>のインスタンス</param>
        protected virtual void OnClick(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
        /// <summary>
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="component">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を削除出来たらtrue，それ以外でfalse</returns>
        public bool RemoveComponent(ToolComponent component) => container.Remove(component);
        internal override void Update()
        {
            if (!Engine.Tool.TreeNodeEx(Label ?? string.Empty, Flags)) return;
            OnClick(EventArgs.Empty);
            for (int i = 0; i < container.Count; i++) container[i].DoUpdate();
            Engine.Tool.TreePop();
        }
        #region IToolTreeNode
        private bool flagChanged = true;
        /// <summary>
        /// 常に開いているかどうかを取得または設定する
        /// </summary>
        public bool AlwaysOpen
        {
            get => _alwaysOpen;
            set
            {
                if (_alwaysOpen == value) return;
                _alwaysOpen = value;
                flagChanged = true;
            }
        }
        private bool _alwaysOpen;
        /// <summary>
        /// 矢印の代わりに丸を用いるかどうかを取得または設定する
        /// </summary>
        public bool Bullet
        {
            get => _bullet;
            set
            {
                if (_bullet == value) return;
                _bullet = value;
                flagChanged = true;
            }
        }
        private bool _bullet;
        /// <summary>
        /// 初期状態で開いているかどうかを取得または設定する
        /// </summary>
        public bool DefaultOpened
        {
            get => _defaultOpened;
            set
            {
                if (_defaultOpened == value) return;
                _defaultOpened = value;
                flagChanged = true;
            }
        }
        private bool _defaultOpened;
        internal ToolTreeNodeFlags Flags
        {
            get
            {
                if (flagChanged)
                {
                    flags = FlagCalculator.CalcToolTreeNode(this);
                    flagChanged = false;
                }
                return flags;
            }
        }
        private ToolTreeNodeFlags flags;
        ToolTreeNodeFlags IToolTreeNode.Flags => Flags;
        /// <summary>
        /// 枠の種類を取得または設定する
        /// </summary>
        public IToolTreeNode.TreeNodeFrameType FrameType
        {
            get => _frameType;
            set
            {
                if (_frameType == value) return;
                _frameType = value;
                flagChanged = true;
            }
        }
        private IToolTreeNode.TreeNodeFrameType _frameType;
        /// <summary>
        /// 矢印のクリックのみで開閉判定を行うかどうかを取得または設定する
        /// </summary>
        public bool OpenOnArrow
        {
            get => _openOnArrow;
            set
            {
                if (_openOnArrow == value) return;
                _openOnArrow = value;
                flagChanged = true;
            }
        }
        private bool _openOnArrow;
        /// <summary>
        /// ダブルクリックで開閉判定を行うかどうかを取得または設定する
        /// </summary>
        public bool OpenOnDoubleClick
        {
            get => _openOnDoubleClick;
            set
            {
                if (_openOnDoubleClick == value) return;
                _openOnDoubleClick = value;
                flagChanged = true;
            }
        }
        private bool _openOnDoubleClick;
        /// <summary>
        /// 自身が選択されているように描画するかどうかを取得または設定する
        /// </summary>
        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected == value) return;
                _selected = value;
                flagChanged = true;
            }
        }
        private bool _selected;
        /// <summary>
        /// 開閉判定が横全体で行われるかどうかを取得または設定する
        /// </summary>
        public bool SpanFullWidth
        {
            get => _spanFullWidth;
            set
            {
                if (_spanFullWidth == value) return;
                _spanFullWidth = value;
                flagChanged = true;
            }
        }
        private bool _spanFullWidth;
        #endregion
    }
}

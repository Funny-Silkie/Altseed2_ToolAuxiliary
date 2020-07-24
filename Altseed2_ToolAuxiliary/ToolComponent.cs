using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="Altseed2.Tool"/>に用いられるクラスの基本クラス
    /// </summary>
    [Serializable]
    public abstract class ToolComponent : IToolComponent
    {
        /// <summary>
        /// インデックスを取得する
        /// </summary>
        public int Index { get; internal set; } = -1;
        /// <summary>
        /// 更新されるかどうかを取得または設定する
        /// </summary>
        public bool IsUpdated { get; set; } = true;
        /// <summary>
        /// <see cref="ToolComponent"/>の新しいインスタンスを生成する
        /// </summary>
        protected ToolComponent() { }
        /// <summary>
        /// 自身が追加された時に実行
        /// </summary>
        public event EventHandler Registered;
        /// <summary>
        /// 自身が削除された時に実行
        /// </summary>
        public event EventHandler UnRegistered;
        ToolComponent IToolComponent.AsToolComponent() => this;
        internal void DoUpdate(bool force = false)
        {
            if (force || IsUpdated) Update();
        }
        internal void InvokeOnRegistered() => OnRegistered(EventArgs.Empty);
        internal void InvokeOnUnRegistered() => OnUnRegistered(EventArgs.Empty);
        /// <summary>
        /// 自身が追加された時に実行
        /// </summary>
        /// <param name="e">与えられる<see cref="EventArgs"/>のインスタンス</param>
        protected virtual void OnRegistered(EventArgs e)
        {
            Registered?.Invoke(this, e);
        }
        /// <summary>
        /// 自身が削除された時に実行
        /// </summary>
        /// <param name="e">与えられる<see cref="EventArgs"/>のインスタンス</param>
        protected virtual void OnUnRegistered(EventArgs e)
        {
            UnRegistered?.Invoke(this, e);
        }
        internal abstract void Update();
    }
    /// <summary>
    /// ツールコンポーネントの基底インターフェイス
    /// </summary>
    public interface IToolComponent
    {
        internal ToolComponent AsToolComponent();
    }
}

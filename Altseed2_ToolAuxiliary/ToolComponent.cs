using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="Altseed2.Tool"/>に用いられるクラスの基本クラス
    /// </summary>
    [Serializable]
    public abstract class ToolComponent
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
        internal void DoUpdate(bool force = false)
        {
            if (force || IsUpdated) Update();
        }
        internal void InvokeOnRegistered() => OnRegistered();
        internal void InvokeOnUnRegistered() => OnUnRegistered();
        /// <summary>
        /// 自身が追加された時に実行
        /// </summary>
        protected virtual void OnRegistered()
        {
            Registered?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// 自身が削除された時に実行
        /// </summary>
        protected virtual void OnUnRegistered()
        {
            UnRegistered?.Invoke(this, EventArgs.Empty);
        }
        internal abstract void Update();
    }
}

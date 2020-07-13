using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="MenuBar"/>に登録されるメニューのクラス
    /// </summary>
    public class Menu : ToolComponent, IComponentRegisterable<ToolComponent>
    {
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        IEnumerable<ToolComponent> IComponentRegisterable<ToolComponent>.Components => Components;
        private readonly ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// 選択可能かどうかを取得または設定する
        /// </summary>
        public bool Enabled { get; set; } = true;
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// クリックされた時に実行
        /// </summary>
        public event EventHandler Clicked;
        /// <summary>
        /// 既定の文字列を持つ<see cref="Menu"/>の新しいインスタンスを生成する
        /// </summary>
        public Menu() : this(string.Empty) { }
        /// <summary>
        /// 指定した文字列を持つ<see cref="Menu"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        public Menu(string label)
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
        protected virtual void OnClicked() { }
        /// <summary>
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="component">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を削除出来たらtrue，それ以外でfalse</returns>
        public bool RemoveComponent(ToolComponent component) => container.Remove(component);
        internal override void Update()
        {
            if (!Engine.Tool.BeginMenu(Label ?? string.Empty, Enabled)) return;
            Clicked?.Invoke(this, EventArgs.Empty);
            OnClicked();
            for (int i = 0; i < container.Count; i++) container[i].Update();
            Engine.Tool.EndMenu();
        }
    }
}

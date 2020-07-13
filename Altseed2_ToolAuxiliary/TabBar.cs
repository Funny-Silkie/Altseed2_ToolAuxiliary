using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// タブバーのツールコンポーネントのクラス
    /// </summary>
    [Serializable]
    public class TabBar : ToolComponent, IComponentRegisterable<TabItem>
    {
        /// <summary>
        /// 格納されている<see cref="TabBar"/>を取得する
        /// </summary>
        public ReadOnlyCollection<TabItem> Components => container.AsReadOnly();
        IEnumerable<TabItem> IComponentRegisterable<TabItem>.Components => Components;
        private readonly ComponentContainer<TabItem> container = new ComponentContainer<TabItem>();
        /// <summary>
        /// 諸設定を取得または設定する
        /// </summary>
        public ToolTabBar Flags { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// 既定の文字列を持つ<see cref="TabBar"/>の新しいインスタンスを生成する
        /// </summary>
        public TabBar() : this(string.Empty) { }
        /// <summary>
        /// 指定した文字列を持つ<see cref="TabBar"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        public TabBar(string label)
        {
            Label = label;
        }
        /// <summary>
        /// コンポーネントを追加する
        /// </summary>
        /// <param name="tabItem">追加するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="tabItem"/>がnull</exception>
        /// <returns><paramref name="tabItem"/>を重複なく追加出来たらtrue，それ以外でfalse</returns>
        public bool AddTabItem(TabItem tabItem) => container.Add(tabItem);
        bool IComponentRegisterable<TabItem>.AddComponent(TabItem component) => AddTabItem(component);
        /// <summary>
        /// 登録されているコンポーネントを全て削除する
        /// </summary>
        public void ClearTabItems() => container.Clear();
        /// <summary>
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="tabItem">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="tabItem"/>がnull</exception>
        /// <returns><paramref name="tabItem"/>を削除出来たらtrue，それ以外でfalse</returns>
        public bool RemoveTabItem(TabItem tabItem) => container.Remove(tabItem);
        bool IComponentRegisterable<TabItem>.RemoveComponent(TabItem component) => RemoveTabItem(component);
        internal override void Update()
        {
            if (!Engine.Tool.BeginTabBar(Label, Flags)) return;
            for (int i = 0; i < container.Count; i++) container[i].Update();
            Engine.Tool.EndTabBar();
        }
    }
}

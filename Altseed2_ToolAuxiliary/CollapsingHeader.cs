﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 折り畳み可能なヘッダのクラス
    /// </summary>
    [Serializable]
    public class CollapsingHeader : ToolComponent, IComponentRegisterable<ToolComponent>
    {
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        IEnumerable<ToolComponent> IComponentRegisterable<ToolComponent>.Components => Components;
        private readonly ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// 使用する設定を取得または設定する
        /// </summary>
        public ToolTreeNode Flags { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// クリックされた時に実行
        /// </summary>
        public event EventHandler Clicked;
        /// <summary>
        /// 既定の文字列を備えた<see cref="CollapsingHeader"/>の新しいインスタンスを生成する
        /// </summary>
        public CollapsingHeader() : this(string.Empty) { }
        /// <summary>
        /// 指定した文字列を備えた<see cref="CollapsingHeader"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        public CollapsingHeader(string label)
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
            if (!Engine.Tool.CollapsingHeader(Label ?? string.Empty, Flags)) return;
            OnClick(EventArgs.Empty);
            for (int i = 0; i < container.Count; i++) container[i].DoUpdate();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 同じ行にツールコンポーネントを表示するクラス
    /// </summary>
    [Serializable]
    public class Line : ToolComponent, IComponentRegisterable<ToolComponent>
    {
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        IEnumerable<ToolComponent> IComponentRegisterable<ToolComponent>.Components => Components;
        private readonly ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// <see cref="Line"/>の新しいインスタンスを生成する
        /// </summary>
        public Line() { }
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
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="component">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を削除出来たらtrue，それ以外でfalse</returns>
        public bool RemoveComponent(ToolComponent component) => container.Remove(component);
        internal override void Update()
        {
            for (int i = 0; i < container.Count; i++)
            {
                if (i > 0 && container[i].IsUpdated) Engine.Tool.SameLine();
                container[i].DoUpdate();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ツールの子ウィンドウのクラス
    /// </summary>
    [Serializable]
    public class ChildWindow : ToolComponent, IComponentRegisterable<ToolComponent>
    {
        /// <summary>
        /// 枠線を描画するかどうかを取得または設定する
        /// </summary>
        public bool Border { get; set; } = true;
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        IEnumerable<ToolComponent> IComponentRegisterable<ToolComponent>.Components => Components;
        private readonly ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// 適用する設定を取得または設定する
        /// </summary>
        public ToolWindowFlags Flags { get; set; }
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// ウィンドウの大きさを取得または設定する
        /// </summary>
        public Vector2I Size { get; set; }
        /// <summary>
        /// 既定の大きさと文字列を備えた<see cref="ChildWindow"/>の新しいインスタンスを生成する
        /// </summary>
        public ChildWindow() : this("Child Window", new Vector2I(100, 100)) { }
        /// <summary>
        /// 指定した大きさと文字列を備えた<see cref="ChildWindow"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="size">大きさ</param>
        public ChildWindow(string label, Vector2I size)
        {
            Label = label;
            Size = size;
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
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="component">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を削除出来たらtrue，それ以外でfalse</returns>
        public bool RemoveComponent(ToolComponent component) => container.Remove(component);
        internal override void Update()
        {
            if (!Engine.Tool.BeginChild(Label ?? string.Empty, Size, Border, Flags)) return;
            for (int i = 0; i < container.Count; i++) container[i].DoUpdate();
            Engine.Tool.EndChild();
        }
    }
    /// <summary>
    /// ツールウィンドウのクラス
    /// </summary>
    [Serializable]
    public class Window : ToolComponent, IComponentRegisterable<ToolComponent>
    {
        /// <summary>
        /// 枠線を描画するかどうかを取得または設定する
        /// </summary>
        public bool Border { get; set; } = true;
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        IEnumerable<ToolComponent> IComponentRegisterable<ToolComponent>.Components => Components;
        private readonly ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// 適用する設定を取得または設定する
        /// </summary>
        public ToolWindowFlags Flags { get; set; }
        /// <summary>
        /// ウィンドウがたたまれているかどうかを取得または設定する
        /// </summary>
        public bool IsCollapsed { get; set; }
        /// <summary>
        /// 使用するメニューバーを取得または設定する
        /// </summary>
        public MenuBar MenuBar { get; set; }
        /// <summary>
        /// ウィンドウの名前を取得または設定する
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 座標を取得または設定する
        /// </summary>
        public Vector2F Position { get; set; }
        /// <summary>
        /// ウィンドウの大きさを取得または設定する
        /// </summary>
        public Vector2F Size { get; set; }
        /// <summary>
        /// 既定の大きさと文字列を備えた<see cref="Window"/>の新しいインスタンスを生成する
        /// </summary>
        public Window() : this("Window", new Vector2F(100, 100)) { }
        /// <summary>
        /// 指定した大きさと文字列を備えた<see cref="Window"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示する文字列</param>
        /// <param name="size">大きさ</param>
        public Window(string label, Vector2F size)
        {
            Name = label;
            Size = size;
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
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="component">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を削除出来たらtrue，それ以外でfalse</returns>
        public bool RemoveComponent(ToolComponent component) => container.Remove(component);
        internal override void Update()
        {
            Engine.Tool.SetNextWindowCollapsed(IsCollapsed, ToolCond.Once);
            Engine.Tool.SetNextWindowPos(Position, ToolCond.Once);
            Engine.Tool.SetNextWindowSize(Size, ToolCond.Once);
            if (Engine.Tool.Begin(Name ?? string.Empty, Flags))
            {
                MenuBar?.Update();
                for (int i = 0; i < container.Count; i++)
                    container[i].DoUpdate();
            }
            IsCollapsed = Engine.Tool.IsWindowCollapsed();
            Position = Engine.Tool.GetWindowPos();
            Size = Engine.Tool.GetWindowSize();
            Engine.Tool.End();
        }
    }
}

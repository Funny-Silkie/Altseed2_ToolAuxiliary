﻿using System;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// <see cref="Tool"/>の補助を行うクラス
    /// </summary>
    public static class ToolHelper
    {
        /// <summary>
        /// クリップボードのテクストを取得または設定する
        /// </summary>
        public static string ClipBoardText { get => Engine.Tool.GetClipboardText(); set => Engine.Tool.SetClipboardText(value ?? string.Empty); }
        /// <summary>
        /// カーソルの座標を取得または設定する
        /// </summary>
        public static Vector2F CursorPosition { get => Engine.Tool.GetCursorPos(); set => Engine.Tool.SetCursorPos(value); }
        /// <summary>
        /// 格納されている<see cref="ToolComponent"/>を取得する
        /// </summary>
        public static ReadOnlyCollection<ToolComponent> Components => container.AsReadOnly();
        private readonly static ComponentContainer<ToolComponent> container = new ComponentContainer<ToolComponent>();
        /// <summary>
        /// ウィンドウがたたまれているかどうかを取得または設定する
        /// </summary>
        public static bool IsCollapsed { get; set; }
        /// <summary>
        /// 使用するメニューバーを取得または設定する
        /// </summary>
        public static MenuBar MenuBar { get; set; }
        /// <summary>
        /// ウィンドウにつけられる名前を取得または設定する
        /// </summary>
        public static string Name { get; set; } = "Tool Window";
        /// <summary>
        /// ウィンドウの座標を取得または設定する
        /// </summary>
        public static Vector2F Position { get; set; }
        /// <summary>
        /// スクロールの最大座標を取得する
        /// </summary>
        public static Vector2F ScrollMax => new Vector2F(Engine.Tool.GetScrollMaxX(), Engine.Tool.GetScrollMaxY());
        /// <summary>
        /// X方向のスクロール地点を取得または設定する
        /// </summary>
        public static float ScrollX { get => Engine.Tool.GetScrollX(); set => Engine.Tool.SetScrollX(value); }
        /// <summary>
        /// Y方向のスクロール地点を取得または設定する
        /// </summary>
        public static float ScrollY { get => Engine.Tool.GetScrollY(); set => Engine.Tool.SetScrollY(value); }
        /// <summary>
        /// ウィンドウの大きさを取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値の成分が0未満</exception>
        public static Vector2F Size
        {
            get => _size;
            set
            {
                if (_size == value) return;
                if (value.X <= 0 || value.Y <= 0) throw new ArgumentOutOfRangeException(nameof(value), "設定しようとした値の成分が負の値です");
                _size = value;
            }
        }
        private static Vector2F _size = new Vector2F(100, 100);
        /// <summary>
        /// ツールウィンドウにおける設定を取得または設定する
        /// </summary>
        public static ToolWindowFlags WindowFlags { get; set; }
        /// <summary>
        /// コンポーネントを追加する
        /// </summary>
        /// <param name="component">追加するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を重複なく追加出来たらtrue，それ以外でfalse</returns>
        public static bool AddComponent(ToolComponent component) => container.Add(component);
        /// <summary>
        /// 登録されているコンポーネントを全て削除する
        /// </summary>
        public static void ClearComponents() => container.Clear();
        private static ToolWindowFlags GetFlags()
        {
            var result = WindowFlags;
            if (MenuBar != null) result |= ToolWindowFlags.MenuBar;
            return result;
        }
        /// <summary>
        /// コンポーネントを削除する
        /// </summary>
        /// <param name="component">削除するコンポーネント</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/>がnull</exception>
        /// <returns><paramref name="component"/>を削除出来たらtrue，それ以外でfalse</returns>
        public static bool RemoveComponent(ToolComponent component) => container.Remove(component);
        /// <summary>
        /// ツールを全て描画する
        /// </summary>
        public static void Update()
        {
            Engine.Tool.SetNextWindowCollapsed(IsCollapsed, ToolCond.Once);
            Engine.Tool.SetNextWindowPos(Position, ToolCond.Once);
            Engine.Tool.SetNextWindowSize(_size, ToolCond.Once);
            if (Engine.Tool.Begin(Name ?? string.Empty, GetFlags()))
            {
                MenuBar?.Update();
                for (int i = 0; i < container.Count; i++) container[i].DoUpdate();
            }
            IsCollapsed = Engine.Tool.IsWindowCollapsed();
            Size = Engine.Tool.GetWindowSize();
            Position = Engine.Tool.GetWindowPos();
            Engine.Tool.End();
        }
    }
}

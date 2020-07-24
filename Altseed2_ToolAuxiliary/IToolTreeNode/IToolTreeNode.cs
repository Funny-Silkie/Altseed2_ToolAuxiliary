using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// ツリーノードのツールを表す
    /// </summary>
    public interface IToolTreeNode : IToolComponent
    {
        /// <summary>
        /// 常に開いているかどうかを取得する
        /// </summary>
        bool AlwaysOpen { get; }
        /// <summary>
        /// 矢印の代わりに丸を用いるかどうかを取得する
        /// </summary>
        bool Bullet { get; }
        /// <summary>
        /// 初期状態で開いているかどうかを取得する
        /// </summary>
        bool DefaultOpened { get; }
        /// <summary>
        /// 使用する設定を取得する
        /// </summary>
        ToolTreeNode Flags { get; }
        /// <summary>
        /// 枠の種類を取得する
        /// </summary>
        TreeNodeFrameType FrameType { get; }
        /// <summary>
        /// 矢印のクリックのみで開閉判定を行うかどうかを取得する
        /// </summary>
        bool OpenOnArrow { get; }
        /// <summary>
        /// ダブルクリックで開閉判定を行うかどうかを取得する
        /// </summary>
        bool OpenOnDoubleClick { get; }
        /// <summary>
        /// 自身が選択されているように描画するかどうかを取得する
        /// </summary>
        bool Selected { get; }
        /// <summary>
        /// 開閉判定が横全体で行われるかどうかを取得する
        /// </summary>
        bool SpanFullWidth { get; }
        /// <summary>
        /// ツリーノードの枠のタイプを表す列挙体
        /// </summary>
        [Serializable]
        public enum TreeNodeFrameType
        {
            /// <summary>
            /// 枠無し
            /// </summary>
            Default,
            /// <summary>
            /// 枠は無いが枠有りの場合と同じスペース取りをする
            /// </summary>
            FramePadding,
            /// <summary>
            /// 枠有り
            /// </summary>
            Framed
        }
    }
}

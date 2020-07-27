using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// タブバーの基底インターフェイス
    /// </summary>
    public interface IToolTabBar : IToolComponent
    {
        /// <summary>
        /// 入りきらないタブの表示方法を取得する
        /// </summary>
        TabBarFittingType FittingType { get; }
        /// <summary>
        /// 使用する設定を取得する
        /// </summary>
        ToolTabBarFlags Flags { get; }
        /// <summary>
        /// タブを並び替えられるかどうかを取得する
        /// </summary>
        bool IsReorderable { get; }
        /// <summary>
        /// 左側にタブをリストアップするボタンを表示するかどうかを取得する
        /// </summary>
        bool ShowListUpButton { get; }
        /// <summary>
        /// 入りきらないタブの表示方法を表す列挙体
        /// </summary>
        [Serializable]
        public enum TabBarFittingType
        {
            /// <summary>
            /// デフォルト値 何も特別な処理をしない
            /// </summary>
            Default,
            /// <summary>
            /// サイズを調整して入りきるようにする
            /// </summary>
            ReSize,
            /// <summary>
            /// スクロールバーを表示する
            /// </summary>
            ScrollBar
        }
    }
}

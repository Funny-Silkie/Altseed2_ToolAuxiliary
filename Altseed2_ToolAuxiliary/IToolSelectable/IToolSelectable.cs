namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 選択可能なツールコンポーネントの基底インターフェイス
    /// </summary>
    public interface IToolSelectable : IToolComponent
    {
        /// <summary>
        /// ダブルクリックでもイベントが作動するかどうかを取得する
        /// </summary>
        bool AllowDoubleClick { get; }
        /// <summary>
        /// 選択可能かどうかを取得する
        /// </summary>
        bool Enabled { get; }
        /// <summary>
        /// 使用する設定を取得する
        /// </summary>
        ToolSelectableFlags Flags { get; }
        /// <summary>
        /// クリック時に親ポップアップウィンドウを開いたままにするかどうかを取得する
        /// </summary>
        bool KeepOpenPopups { get; }
    }
}

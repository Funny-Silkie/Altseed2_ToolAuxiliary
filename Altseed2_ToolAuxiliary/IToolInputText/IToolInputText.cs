using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// テキストを入力するツールコンポーネントの基底インターフェイス
    /// </summary>
    public interface IToolInputText : IToolComponent
    {
        /// <summary>
        /// マウスでフォーカスしたときテキスト全体を選択するかどうか取得する
        /// </summary>
        bool AutoSelectAll { get; }
        /// <summary>
        /// 入力できる文字の種類を取得する
        /// </summary>
        InputTextCharType CharType { get; }
        /// <summary>
        /// 横方向のスクロールが行われるかどうかを取得する
        /// </summary>
        bool EnableHorizontalScroll { get; }
        /// <summary>
        /// 使用する設定を取得する
        /// </summary>
        ToolInputText Flags { get; }
        /// <summary>
        /// 上書きモードかどうかを取得する
        /// </summary>
        /// <remarks>falseの時は挿入モード</remarks>
        bool IsOverwriteMode { get; }
        /// <summary>
        /// 入力した文字列を*で隠すかどうかを取得する
        /// </summary>
        bool IsPasswordMode { get; }
        /// <summary>
        /// 読み取り専用かどうかを取得する
        /// </summary>
        bool IsReadOnly { get; }
        /// <summary>
        /// スペースやタブスペースを禁じるかどうかを取得する
        /// </summary>
        /// <remarks>trueの時は<see cref="TabSpace"/>よりも優先される</remarks>
        bool NoBlank { get; }
        /// <summary>
        /// Tabキーを押したときタブスペースを挿入するかどうかを取得する
        /// </summary>
        bool TabSpace { get; }
        /// <summary>
        /// 小文字を大文字に自動変換するかどうかを取得する
        /// </summary>
        bool UpperCaseFilter { get; }
        /// <summary>
        /// <see cref="IToolInputText"/>における入力できる文字の制限を表す列挙体
        /// </summary>
        [Serializable, Flags]
        public enum InputTextCharType : int
        {
            /// <summary>
            /// 全ての文字の入力を許可する
            /// </summary>
            AllowAll = 0,
            /// <summary>
            /// 0123456789.+-*/eEのみを許可
            /// </summary>
            Scientific = 1,
            /// <summary>
            /// 0123456789.+-*/のみを許可
            /// </summary>
            Decimal = 3,
            /// <summary>
            /// 0123456789abcdefのみを許可
            /// </summary>
            Hexadecimal = 4,
            /// <summary>
            /// 0123456789のみを許可
            /// </summary>
            NumberOnly = 7,
        }
    }
}

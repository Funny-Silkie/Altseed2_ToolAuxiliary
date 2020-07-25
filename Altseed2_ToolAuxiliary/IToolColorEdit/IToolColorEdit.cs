using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 色を編集できるツールコンポーネントの基底インターフェイス
    /// </summary>
    public interface IToolColorEdit : IToolComponent
    {
        /// <summary>
        /// アルファ値を編集するかどうかを取得する
        /// </summary>
        bool EditAlpha { get; }
        /// <summary>
        /// 使用する設定を取得する
        /// </summary>
        ToolColorEdit Flags { get; }
        /// <summary>
        /// ウィンドウに表示されるインプットの種類を取得する
        /// </summary>
        ColorEditInputType InputType { get; }
        /// <summary>
        /// 最初に表示されるカラーエディタの種類を取得する
        /// </summary>
        ColorEditPickerType PickerType { get; }
        /// <summary>
        /// アルファ値のバーを表示するかどうかを取得する
        /// </summary>
        bool ShowAlphaBar { get; }
        /// <summary>
        /// 入力を右クリックしたときにオプションを表示するかどうかを取得する
        /// </summary>
        bool ShowInputOption { get; }
        /// <summary>
        /// テキストラベルを表示するかどうかを取得する
        /// </summary>
        bool ShowLabel { get; }
        /// <summary>
        /// 詳細を設定するウィンドウを表示するかどうかを取得する
        /// </summary>
        bool ShowPicker { get; }
        /// <summary>
        /// 入力の右側の色のプレビューを表示するかどうかを取得する
        /// </summary>
        bool ShowSmallPreview { get; }
        /// <summary>
        /// 扱う値のタイプを取得する
        /// </summary>
        ColorEditValueType ValueType { get; }
        /// <summary>
        /// ウィンドウに表示されるインプットの種類を表す列挙体
        /// </summary>
        [Serializable]
        public enum ColorEditInputType
        {
            /// <summary>
            /// インプットを表示しない
            /// </summary>
            None,
            /// <summary>
            /// RGBのインプットを表示する
            /// </summary>
            RGB,
            /// <summary>
            /// HSVのインプットを表示する
            /// </summary>
            HSV,
            /// <summary>
            /// <c>#FFFFFFFF</c>の様な16進数表記で表示する
            /// </summary>
            ColorCode,
            /// <summary>
            /// <see cref="RGB"/>，<see cref="HSV"/>，<see cref="ColorCode"/>全てを用いる
            /// </summary>
            All
        }
        /// <summary>
        /// 最初に表示されるカラーエディタの種類を表す列挙体
        /// </summary>
        [Serializable]
        public enum ColorEditPickerType
        {
            /// <summary>
            /// 両方を用いる
            /// </summary>
            Both,
            /// <summary>
            /// 四角形のエディタ
            /// </summary>
            Bar,
            /// <summary>
            /// 円と三角形のエディタ
            /// </summary>
            Wheel
        }
        /// <summary>
        /// 扱う値のタイプを表す列挙体
        /// </summary>
        [Serializable]
        public enum ColorEditValueType
        {
            /// <summary>
            /// 両方を用いる
            /// </summary>
            Both,
            /// <summary>
            /// [0, 255]の整数
            /// </summary>
            UInt8,
            /// <summary>
            /// [0, 1]の小数
            /// </summary>
            Float
        }
    }
}

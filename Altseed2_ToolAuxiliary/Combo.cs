using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// コンボボックスのクラス
    /// </summary>
    /// <typeparam name="T">格納される要素の型</typeparam>
    [Serializable]
    public class Combo<T> : ListComponent<T>
    {
        /// <summary>
        /// 表示される文字列を取得または設定する
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// ポップアップされる最大アイテム数を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">設定しようとした値が0以下</exception>
        public int MaxPopUpItems
        {
            get => _maxPopUpItems;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), $"設定しようとした値が0以下です\n設定しようとした値：{value}");
                if (_maxPopUpItems == value) return;
                _maxPopUpItems = value;
            }
        }
        private int _maxPopUpItems = 3;
        /// <summary>
        /// 既定の文字列と要素を格納する<see cref="Combo{T}"/>の新しいインスタンスを生成する
        /// </summary>
        public Combo() : this(string.Empty, Array.Empty<T>()) { }
        /// <summary>
        /// 指定した文字列と要素を格納する<see cref="Combo{T}"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="items">格納する要素</param>
        public Combo(string label, T[] items)
        {
            Label = label;
            Items.SetFromArray(items);
        }
        internal override void Update()
        {
            var current = SelectedIndex;
            if (!Engine.Tool.Combo(Label ?? string.Empty, ref current, Items.Text, MaxPopUpItems)) return;
            if (current == SelectedIndex) return;
            SelectedIndex = current;
        }
    }
}

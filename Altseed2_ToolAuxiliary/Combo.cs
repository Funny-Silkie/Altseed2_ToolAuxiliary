using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// コンボボックスのクラス
    /// </summary>
    [Serializable]
    public sealed class Combo : ToolComponent
    {
        private readonly static object[] defaultItem = new[] { "Value" };
        private string text;
        /// <summary>
        /// 表示するアイテムを設定する
        /// </summary>
        /// <exception cref="ArgumentNullException">設定しようとした値がnull</exception>
        public object[] Items
        {
            get => _items;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value), "引数がnullです");
                if (_items == value) return;
                var array = new object[value.Length];
                text = string.Empty;
                var builder = new System.Text.StringBuilder();
                for (int i = 0; i < value.Length; i++)
                {
                    if (i > 0) builder.Append('\t');
                    builder.Append(value[i].ToString() ?? string.Empty);
                    array[i] = value[i];
                }
                _items = array;
                text = builder.ToString() ?? string.Empty;
            }
        }
        private object[] _items;
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
        /// 選択されているインデックスを取得または設定する
        /// </summary>
        public int SelectedIndex { get; set; }
        /// <summary>
        /// 選択されているアイテムを取得または設定する
        /// </summary>
        public object SelectedItem
        {
            get => SelectedIndex >= 0 && SelectedIndex < _items.Length ? _items[SelectedIndex] : null;
            set => SelectedIndex = Array.IndexOf(_items, value);
        }
        /// <summary>
        /// 選択されている要素が変化したときに実行
        /// </summary>
        public event EventHandler SelectedItemChanged;
        /// <summary>
        /// 既定の文字列と要素を格納する<see cref="Combo"/>の新しいインスタンスを生成する
        /// </summary>
        public Combo() : this(string.Empty, defaultItem) { }
        /// <summary>
        /// 指定した文字列と要素を格納する<see cref="Combo"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="label">表示される文字列</param>
        /// <param name="items">格納する要素</param>
        public Combo(string label, object[] items)
        {
            Label = label;
            Items = items;
        }
        internal override void Update()
        {
            var current = SelectedIndex;
            if (!Engine.Tool.Combo(Label ?? string.Empty, ref current, text, MaxPopUpItems)) return;
            if (current == SelectedIndex) return;
            SelectedIndex = current;
            SelectedItemChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

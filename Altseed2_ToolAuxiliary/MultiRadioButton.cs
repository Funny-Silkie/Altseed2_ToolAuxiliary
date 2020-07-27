using System;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// 複数のラジオボタンのクラス
    /// </summary>
    [Serializable]
    public class MultiRadioButton : ToolComponent
    {
        private (string label, int index)[] items;
        /// <summary>
        /// 表示する文字列を取得または設定する
        /// </summary>
        /// <exception cref="ArgumentNullException">設定しようとした値がnull</exception>
        public string[] Labels
        {
            get
            {
                var result = new string[items.Length];
                for (int i = 0; i < items.Length; i++) result[i] = items[i].label;
                return result;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value), "引数がnullです");
                items = new (string label, int index)[value.Length];
                for (int i = 0; i < value.Length; i++) items[i] = (value[i], i);
            }
        }
        /// <summary>
        /// 選択しているボタンのインデックスを取得または設定する
        /// </summary>
        public int SelectedIndex { get; set; }
        /// <summary>
        /// 指定した文字列とボタンのインデックスを持つ<see cref="MultiRadioButton"/>の新しいインスタンスを生成する
        /// </summary>
        /// <param name="items">使用するボタンの文字列とインデックス</param>
        /// <exception cref="ArgumentNullException"><paramref name="items"/>がnull</exception>
        public MultiRadioButton(params (string, int)[] items)
        {
            this.items = items ?? throw new ArgumentNullException(nameof(items), "引数がnullです");
        }
        /// <summary>
        /// <see cref="SelectedIndex"/>が変化したときに実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<int>> SelectedIndexChanged;
        /// <summary>
        /// <see cref="SelectedIndex"/>が変化したときに実行
        /// </summary>
        /// <param name="e">変更前後の<see cref="SelectedIndex"/>を示す<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnSelectedIndexChanged(ToolValueEventArgs<int> e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }
        /// <summary>
        /// ボタンの文字列をインデックスを設定する
        /// </summary>
        /// <param name="items">文字列とインデックスを持つ配列</param>
        /// <exception cref="ArgumentNullException"><paramref name="items"/>がnull</exception>
        public void SetItems(params (string label, int index)[] items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items), "引数がnullです");
            this.items = ((string, int)[])items.Clone();
        }
        internal override void Update()
        {
            var v = SelectedIndex;
            foreach (var (label, index) in items) Engine.Tool.RadioButton(label ?? string.Empty, ref v, index);
            if (v == SelectedIndex) return;
            var old = SelectedIndex;
            SelectedIndex = v;
            OnSelectedIndexChanged(new ToolValueEventArgs<int>(old, SelectedIndex));
        }
    }
}

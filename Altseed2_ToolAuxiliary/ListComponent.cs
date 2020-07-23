using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Altseed2.ToolAuxiliary
{
    /// <summary>
    /// リストとして列挙出来るリストのコンポーネントクラス
    /// </summary>
    /// <typeparam name="T">格納する要素の型</typeparam>
    [Serializable]
    public abstract class ListComponent<T> : ToolComponent
    {
        /// <summary>
        /// 表示するアイテムのコレクションを取得する
        /// </summary>
        public ListComponentItems Items { get; } = new ListComponentItems();
        /// <summary>
        /// 選択されているインデックスを取得または設定する
        /// </summary>
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (_selectedIndex == value) return;
                var arg = new ToolValueEventArgs<int>(_selectedIndex, value);
                _selectedIndex = value;
                OnSelectedIndexChanged(arg);
            }
        }
        private int _selectedIndex;
        /// <summary>
        /// 選択されているアイテムを取得または設定する
        /// </summary>
        public T SelectedItem
        {
            get => _selectedIndex >= 0 && _selectedIndex < Items.Count ? Items[_selectedIndex] : default;
            set => _selectedIndex = Items.IndexOf(value);
        }
        /// <summary>
        /// 選択されている文字列を取得または設定する
        /// </summary>
        public string SelectedText
        {
            get => _selectedIndex >= 0 && Items.Count < _selectedIndex ? Items.GetText(_selectedIndex) : null;
            set => SelectedIndex = Items.IndexOfText(value);
        }
        /// <summary>
        /// <see cref="SelectedIndex"/>が変化したときに実行
        /// </summary>
        public event EventHandler<ToolValueEventArgs<int>> SelectedIndexChanged;
        /// <summary>
        /// <see cref="ListComponent{T}"/>の新しいインスタンスを生成する
        /// </summary>
        protected ListComponent() { }
        /// <summary>
        /// 選択されている要素が変化したときに実行
        /// </summary>
        /// <param name="e">変更前後の<see cref="SelectedIndex"/>を示す<see cref="ToolValueEventArgs{T}"/>のインスタンス</param>
        protected virtual void OnSelectedIndexChanged(ToolValueEventArgs<int> e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }
        /// <summary>
        /// 格納するデータを設定する
        /// </summary>
        /// <param name="array">設定するデータ</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
        public void SetItems(params T[] array)
        {
            Items.SetFromArray(array);
            SelectedIndex = array.Length > 0 ? 0 : -1;
        }
        /// <summary>
        /// <see cref="ListComponent{T}"/>に格納される要素のコレクションのクラス
        /// </summary>
        [Serializable]
        public class ListComponentItems : IList<T>, IReadOnlyList<T>, IList
        {
            /// <summary>
            /// 要素からそれに対応する文字列を生成するのに用いる関数
            /// </summary>
            /// <param name="obj">文字列を取得する要素</param>
            /// <returns><paramref name="obj"/>を表す文字列</returns>
            public delegate string TextProvider(T obj);
            private T[] items;
            private bool textChanged;
            private int version;
            /// <summary>
            /// 格納されている要素数を取得する
            /// </summary>
            public int Count { get; private set; }
            bool IList.IsFixedSize => false;
            bool ICollection<T>.IsReadOnly => false;
            bool IList.IsReadOnly => false;
            bool ICollection.IsSynchronized => false;
            /// <summary>
            /// 要素から文字列を生成する関数を取得または設定する
            /// </summary>
            /// <remarks>nullの場合は<see cref="object.ToString"/>が使用される</remarks>
            public TextProvider Provider { get; set; }
            object ICollection.SyncRoot
            {
                get
                {
                    if (_syncRoot == null) Interlocked.CompareExchange(ref _syncRoot, new object(), null);
                    return _syncRoot;
                }
            }
            [NonSerialized]
            private object _syncRoot;
            /// <summary>
            /// 現在持っている要素を表す文字列を取得する
            /// </summary>
            public string Text
            {
                get
                {
                    if (textChanged)
                    {
                        var builder = new StringBuilder();
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (i > 0) builder.Append('\t');
                            builder.Append((Provider == null ? items[i]?.ToString() : Provider.Invoke(items[i])) ?? string.Empty);
                        }
                        _text = builder.ToString() ?? string.Empty;
                        textChanged = false;
                    }
                    return _text;
                }
            }
            private string _text;
            /// <summary>
            /// 指定したインデックスの要素を取得または設定する
            /// </summary>
            /// <param name="index">取得する要素のインデックス</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Count"/>以上</exception>
            /// <returns><paramref name="index"/>に対応する要素</returns>
            public T this[int index]
            {
                get
                {
                    if (index < 0 || Count <= index) throw new ArgumentOutOfRangeException(nameof(index), $"引数が許容範囲外です\n許容される範囲：0-{Count - 1}\n実際の値：{index}");
                    return items[index];
                }
                set
                {
                    if (index < 0 || Count <= index) throw new ArgumentOutOfRangeException(nameof(index), $"引数が許容範囲外です\n許容される範囲：0-{Count - 1}\n実際の値：{index}");
                    items[index] = value;
                    textChanged = true;
                    version++;
                }
            }
            object IList.this[int index]
            {
                get => this[index];
                set
                {
                    if (!IsCompatibleValue(value, out var item)) throw new ArgumentException($"無効な方です\n要求される型：{typeof(T)}\n実際の型：{value?.GetType().Name ?? "Null"}", nameof(value));
                    this[index] = item;
                }
            }
            private static bool IsCompatibleValue(object obj, out T value)
            {
                if (obj == null && default(T) == null)
                {
                    value = default;
                    return true;
                }
                if (obj is T t)
                {
                    value = t;
                    return true;
                }
                value = default;
                return false;
            }
            /// <summary>
            /// 要素を追加する
            /// </summary>
            /// <param name="item">追加する要素</param>
            public void Add(T item)
            {
                if (items.Length < Count + 1) ReSize(Count + 1);
                items[Count++] = item;
                version++;
                textChanged = true;
            }
            int IList.Add(object value)
            {
                if (!IsCompatibleValue(value, out var item)) return -1;
                Add(item);
                return Count - 1;
            }
            /// <summary>
            /// 要素をまとめて設定する
            /// </summary>
            /// <param name="array">設定する要素</param>
            /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
            public void AddRange(params T[] array)
            {
                if (array == null) throw new ArgumentNullException(nameof(array), "引数がnullです");
                if (items.Length < array.Length + Count) ReSize(array.Length + Count);
                for (int i = 0; i < array.Length; i++) items[Count + i] = array[i];
                Count += array.Length;
                version++;
                textChanged = true;
            }
            /// <summary>
            /// 全ての要素を削除する
            /// </summary>
            public void Clear()
            {
                if (Count == 0) return;
                for (int i = 0; i < Count; i++) items[i] = default;
                Count = 0;
                version++;
                textChanged = true;
            }
            /// <summary>
            /// 指定した要素が格納されているかどうかを検索する
            /// </summary>
            /// <param name="item">検索する要素</param>
            /// <returns><paramref name="item"/>が格納されていたらtrue，それ以外でfalse</returns>
            public bool Contains(T item) => IndexOf(item) >= 0;
            bool IList.Contains(object value) => IsCompatibleValue(value, out var item) && Contains(item);
            /// <summary>
            /// 指定した配列に要素をコピーする
            /// </summary>
            /// <param name="array">コピー先の配列</param>
            /// <param name="arrayIndex"><paramref name="array"/>におけるコピー開始地点</param>
            /// <exception cref="ArgumentException"><paramref name="array"/>のサイズ不足</exception>
            /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/>が0未満</exception>
            public void CopyTo(T[] array, int arrayIndex)
            {
                if (array == null) throw new ArgumentNullException(nameof(array), "引数がnullです");
                if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex), "引数が0未満です");
                if (array.Length < arrayIndex + Count) throw new ArgumentException("サイズ不足です", nameof(array));
                for (int i = 0; i < Count; i++) array[arrayIndex++] = items[i];
            }
            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null) throw new ArgumentNullException(nameof(array), "引数がnullです");
                if (array.Rank != 1) throw new ArgumentException("次元が1次元ではありません", nameof(array));
                if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "引数が0未満です");
                if (array.Length < index + Count) throw new ArgumentException("サイズ不足です", nameof(array));
                if (array.GetLowerBound(0) != 0) throw new ArgumentException("開始インデックスが0ではありません", nameof(array));
                switch (array)
                {
                    case T[] t: CopyTo(t, index); return;
                    case object[] o:
                        try
                        {
                            for (int i = 0; i < Count; i++) o[index++] = items[i];
                        }
                        catch (ArrayTypeMismatchException)
                        {
                            throw new ArgumentException("無効な型が渡されました", nameof(array));
                        }
                        return;
                    default: throw new ArgumentException($"無効な配列の型です\n型：{array.GetType()}", nameof(array));
                }
            }
            /// <summary>
            /// 列挙を行う構造体を取得する
            /// </summary>
            /// <returns><see cref="Enumerator"/>の新しいインスタンス</returns>
            public Enumerator GetEnumerator() => new Enumerator(this);
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
            /// <summary>
            /// 指定したインデックスの文字列を取得する
            /// </summary>
            /// <param name="index">取得する文字列のインデックス</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Count"/>以上</exception>
            /// <returns><paramref name="index"/>に対応する文字列のインデックス</returns>
            public string GetText(int index)
            {
                if (index < 0 || Count <= index) throw new ArgumentOutOfRangeException(nameof(index), $"引数が許容範囲外です\n許容される範囲：0-{Count - 1}\n実際の値：{index}");
                var array = Text.Split('\t');
                return array[index];
            }
            /// <summary>
            /// 指定した要素のうち先頭の物のインデックスを取得する
            /// </summary>
            /// <param name="item">検索する要素</param>
            /// <returns><paramref name="item"/>のうち先頭の物のインデックス 無かったら-1</returns>
            public int IndexOf(T item)
            {
                if (item == null)
                {
                    for (int i = 0; i < Count; i++)
                        if (items[i] == null)
                            return i;
                }
                else
                {
                    var comparer = EqualityComparer<T>.Default;
                    for (int i = 0; i < Count; i++)
                        if (comparer.Equals(items[i], item))
                            return i;
                }
                return -1;
            }
            int IList.IndexOf(object value) => IsCompatibleValue(value, out var item) ? IndexOf(item) : -1;
            /// <summary>
            /// 指定したい文字列のうち先頭の物のインデックスを取得する
            /// </summary>
            /// <param name="text">検索する文字列</param>
            /// <returns><paramref name="text"/>のうち先頭の物のインデックス</returns>
            public int IndexOfText(string text)
            {
                if (text == null) return -1;
                var sp = text.Split('\t', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sp.Length; i++)
                    if (sp[i] == text)
                        return i;
                return -1;
            }
            /// <summary>
            /// 指定したインデックスに要素を挿入する
            /// </summary>
            /// <param name="index"><paramref name="item"/>を挿入するインデックス</param>
            /// <param name="item">挿入する要素</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Count"/>より大きい</exception>
            public void Insert(int index, T item)
            {
                if (index < 0 || Count < index) throw new ArgumentOutOfRangeException(nameof(index), $"引数が許容範囲外です\n許容される範囲：0-{Count}\n実際の値：{index}");
                if (index < Count) Array.Copy(items, index, items, index + 1, Count - index);
                items[index] = item;
                Count++;
                version++;
                textChanged = true;
            }
            void IList.Insert(int index, object value)
            {
                if (!IsCompatibleValue(value, out var item)) throw new ArgumentException($"無効な方です\n要求される型：{typeof(T)}\n実際の型：{value?.GetType().Name ?? "Null"}", nameof(value));
                Insert(index, item);
            }
            /// <summary>
            /// 指定した要素のうち先頭の物を削除する
            /// </summary>
            /// <param name="item">削除するアイテム</param>
            /// <returns><paramref name="item"/>を削除出来たらtrue，それ以外でfalse</returns>
            public bool Remove(T item)
            {
                var index = IndexOf(item);
                if (index < 0) return false;
                RemoveAt(index);
                return true;
            }
            void IList.Remove(object value) => _ = IsCompatibleValue(value, out var item) && Remove(item);
            /// <summary>
            /// 指定したインデックスの要素を削除する
            /// </summary>
            /// <param name="index">削除する要素のインデックス</param>
            /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>が0未満または<see cref="Count"/>以上</exception>
            public void RemoveAt(int index)
            {
                if (index < 0 || Count <= index) throw new ArgumentOutOfRangeException(nameof(index), $"引数が許容範囲外です\n許容される範囲：0-{Count - 1}\n実際の値：{index}");
                if (index < --Count) Array.Copy(items, index + 1, items, index, Count - index);
                items[Count] = default;
                version++;
                textChanged = true;
            }
            private void ReSize(int min)
            {
                if (min < Count) return;
                var length = items.Length;
                var size = length == 0 ? 4 : length * 2;
                if ((uint)size > int.MaxValue) size = int.MaxValue;
                if (size < min) size = min;
                if (size == length) return;
                if (size == 0) items = Array.Empty<T>();
                else
                {
                    var array = new T[size];
                    for (int i = 0; i < Count; i++) array[i] = items[i];
                    items = array;
                }
                version++;
            }
            /// <summary>
            /// このインスタンスの持つ要素を配列と同じものにする
            /// </summary>
            /// <param name="array">データを格納する配列</param>
            /// <exception cref="ArgumentNullException"><paramref name="array"/>がnull</exception>
            public void SetFromArray(params T[] array)
            {
                if (array == null) throw new ArgumentNullException(nameof(array), "引数がnullです");
                items = (T[])array.Clone();
                Count = array.Length;
                version++;
                textChanged = true;
            }
            /// <summary>
            /// 指定した比較子を用いて並べ替える
            /// </summary>
            /// <param name="comparer">並び替えに用いる比較子</param>
            /// <exception cref="ArgumentNullException"><paramref name="comparer"/>がnull</exception>
            public void Sort(IComparer<T> comparer)
            {
                if (comparer == null) throw new ArgumentNullException(nameof(comparer), "引数がnullです");
                Array.Sort(items, 0, Count, comparer);
                version++;
                textChanged = true;
            }
            /// <summary>
            /// <see cref="ListComponentItems"/>の列挙を行う構造体
            /// </summary>
            [Serializable]
            public struct Enumerator : IEnumerator<T>
            {
                private int index;
                private readonly ListComponentItems items;
                private readonly int version;
                /// <summary>
                /// 現在列挙されている要素を取得する
                /// </summary>
                public T Current { get; private set; }
                readonly object IEnumerator.Current
                {
                    get
                    {
                        if (index <= 0 || items.Count < index) throw new InvalidOperationException("現在列挙されている要素を取得できませんでした");
                        return Current;
                    }
                }
                internal Enumerator(ListComponentItems items)
                {
                    this.items = items;
                    Current = default;
                    index = 0;
                    version = items.version;
                }
                /// <summary>
                /// このインスタンスを破棄する
                /// </summary>
                public void Dispose() { }
                /// <summary>
                /// 列挙を次に進める
                /// </summary>
                /// <exception cref="InvalidOperationException">列挙中にコレクションが変更された</exception>
                /// <returns>列挙を次に進められたらtrue，それ以外でfalse</returns>
                public bool MoveNext()
                {
                    if (version != items.version) throw new InvalidOperationException("列挙中にコレクションが変更されました");
                    if (index < items.Count)
                    {
                        Current = items[index++];
                        return true;
                    }
                    Current = default;
                    index = items.Count + 1;
                    return false;
                }
                void IEnumerator.Reset()
                {
                    if (version != items.version) throw new InvalidOperationException("列挙中にコレクションが変更されました");
                    Current = default;
                    index = 0;
                }
            }
        }
    }
}

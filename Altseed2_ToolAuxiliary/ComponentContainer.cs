using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Altseed2.ToolAuxiliary
{
    [Serializable]
    internal sealed class ComponentContainer<TComponent> : IList<TComponent>, IReadOnlyList<TComponent>, IList where TComponent : ToolComponent
    {
        private readonly List<TComponent> components = new List<TComponent>();
        public int Count => components.Count;
        bool IList.IsFixedSize => false;
        bool ICollection<TComponent>.IsReadOnly => false;
        bool IList.IsReadOnly => false;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => ((ICollection)components).SyncRoot;
        public TComponent this[int index]
        {
            get => components[index];
            set => components[index] = value;
        }
        object IList.this[int index]
        {
            get => this[index];
            set
            {
                if (value is TComponent c) this[index] = c;
                else throw new ArgumentException();
            }
        }
        public ReadOnlyCollection<TComponent> AsReadOnly() => new ReadOnlyCollection<TComponent>(this);
        public bool Add(TComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component), "引数がnullです");
            if (component.Index >= 0) return false;
            component.Index = components.Count;
            components.Add(component);
            component.InvokeOnRegistered();
            return true;
        }
        void ICollection<TComponent>.Add(TComponent item) => Add(item);
        int IList.Add(object value) => value is TComponent c && Add(c) ? Count - 1 : -1;
        public void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                components[i].Index = -1;
                components[i].InvokeOnUnRegistered();
            }
            components.Clear();
        }
        public bool Contains(TComponent component)
        {
            if (component == null) return false;
            if (component.Index < 0 || Count <= component.Index) return false;
            return components[component.Index] == component;
        }
        bool IList.Contains(object value) => value is TComponent c && Contains(c);
        public void CopyTo(TComponent[] array, int arrayIndex) => components.CopyTo(array, arrayIndex);
        void ICollection.CopyTo(Array array, int index) => ((ICollection)components).CopyTo(array, index);
        public IEnumerator<TComponent> GetEnumerator() => components.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int IndexOf(TComponent component) => Contains(component) ? component.Index : -1;
        int IList.IndexOf(object value) => value is TComponent c ? IndexOf(c) : -1;
        public bool Insert(int index, TComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component), "引数がnullです");
            if (component.Index >= 0) return false;
            if (index < 0 || Count < index) throw new ArgumentOutOfRangeException(nameof(index), $"引数が範囲外です\n許容される範囲：0～{Count}\n実際の値：{index}");
            components.Insert(index, component);
            component.Index = index;
            component.InvokeOnRegistered();
            for (int i = index + 1; i < Count; i++) components[i].Index++;
            return true;
        }
        void IList.Insert(int index, object value) => _ = value is TComponent c && Insert(index, c);
        void IList<TComponent>.Insert(int index, TComponent item) => Insert(index, item);
        public bool Remove(TComponent component)
        {
            if (component == null) throw new ArgumentNullException(nameof(component), "引数がnullです");
            if (!Contains(component)) return false;
            RemoveAt(component.Index);
            return true;
        }
        void IList.Remove(object value) => _ = value is TComponent c && Remove(c);
        public void RemoveAt(int index)
        {
            if (index < 0 || Count <= index) throw new ArgumentOutOfRangeException(nameof(index));
            var component = components[index];
            components.RemoveAt(index);
            for (int i = index; i < components.Count; i++) components[i].Index--;
            component.Index = -1;
            component.InvokeOnUnRegistered();
        }
    }
}

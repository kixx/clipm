using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ClipM
{
    public class BindingOrderedSet<T> : ICollection<T>, IBindingList
    {
        private EqualityComparer<T> @default;
        private readonly IDictionary<T, T> m_Dictionary;
        private readonly BindingList<T> m_BindingList;

        public event ListChangedEventHandler ListChanged;

        public BindingOrderedSet() 
            : this(EqualityComparer<T>.Default)
        {
        }

        public BindingOrderedSet(IEqualityComparer<T> comparer)
        {
            m_Dictionary = new Dictionary<T, T>(comparer);
            m_BindingList = new BindingList<T>();
        }

        public BindingList<T> getBindingList()
        {
            return m_BindingList;
        }

        public int Count
        {
            get
            { return m_Dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get
            { return m_Dictionary.IsReadOnly; }
        }

        public bool AllowNew
        {
            get
            {
                return ((IBindingList)m_BindingList).AllowNew;
            }
        }

        public bool AllowEdit
        {
            get
            {
                return ((IBindingList)m_BindingList).AllowEdit;
            }
        }

        public bool AllowRemove
        {
            get
            {
                return ((IBindingList)m_BindingList).AllowRemove;
            }
        }

        public bool SupportsChangeNotification
        {
            get
            {
                return ((IBindingList)m_BindingList).SupportsChangeNotification;
            }
        }

        public bool SupportsSearching
        {
            get
            {
                return ((IBindingList)m_BindingList).SupportsSearching;
            }
        }

        public bool SupportsSorting
        {
            get
            {
                return ((IBindingList)m_BindingList).SupportsSorting;
            }
        }

        public bool IsSorted
        {
            get
            {
                return ((IBindingList)m_BindingList).IsSorted;
            }
        }

        public PropertyDescriptor SortProperty
        {
            get
            {
                return ((IBindingList)m_BindingList).SortProperty;
            }
        }

        public ListSortDirection SortDirection
        {
            get
            {
                return ((IBindingList)m_BindingList).SortDirection;
            }
        }

        public bool IsFixedSize
        {
            get
            {
                return ((IBindingList)m_BindingList).IsFixedSize;
            }
        }

        public object SyncRoot
        {
            get
            {
                return ((IBindingList)m_BindingList).SyncRoot;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return ((IBindingList)m_BindingList).IsSynchronized;
            }
        }

        public object this[int index]
        {
            get
            {
                return ((IBindingList)m_BindingList)[index];
            }

            set
            {
                ((IBindingList)m_BindingList)[index] = value;
            }
        }


        public void Clear()
        {
            m_BindingList.Clear();
            m_Dictionary.Clear();
        }

        public bool Contains(T item)
        {
            return m_Dictionary.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            m_BindingList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_BindingList.GetEnumerator();
        }

        public bool Remove(T item)
        {
            bool found = m_Dictionary.ContainsKey(item);
            if (!found) return false;
            m_Dictionary.Remove(item);
            m_BindingList.Remove(item);
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Add(T item)
        {
            if (m_Dictionary.ContainsKey(item)) return false;
            m_BindingList.Add(item);
            m_Dictionary.Add(item, item);
            return true;
        }

        public bool Insert(T item)
        {
            if (m_Dictionary.ContainsKey(item)) return false;
            m_BindingList.Insert(0, item);
            m_Dictionary.Add(item, item);
            return true;
        }

        public object AddNew()
        {
            return ((IBindingList)m_BindingList).AddNew();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            ((IBindingList)m_BindingList).AddIndex(property);
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            ((IBindingList)m_BindingList).ApplySort(property, direction);
        }

        public int Find(PropertyDescriptor property, object key)
        {
            return ((IBindingList)m_BindingList).Find(property, key);
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            ((IBindingList)m_BindingList).RemoveIndex(property);
        }

        public void RemoveSort()
        {
            ((IBindingList)m_BindingList).RemoveSort();
        }

        public int Add(object value)
        {
            if (value is T)
            {
                m_Dictionary.Add((T)value, (T)value);
            }
            return ((IBindingList)m_BindingList).Add(value);
        }

        public bool Contains(object value)
        {
            if (value is T)
            {
                return m_Dictionary.ContainsKey((T)value);
            }
            else
            {
                return ((IBindingList)m_BindingList).Contains(value);
            }
        }

        public int IndexOf(object value)
        {
            return ((IBindingList)m_BindingList).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            if (value is T)
            {
                m_Dictionary.Add((T)value, (T)value);
            }
            ((IBindingList)m_BindingList).Insert(index, value);
        }

        public void Remove(object value)
        {
            if(value is T)
            {
                m_Dictionary.Remove((T)value);
            }
            ((IBindingList)m_BindingList).Remove(value);
        }

        public void RemoveAt(int index)
        {
            T value = m_BindingList[index];
            m_Dictionary.Remove(value);

            m_BindingList.RemoveAt(index);
        }

        public void CopyTo(Array array, int index)
        {
            ((IBindingList)m_BindingList).CopyTo(array, index);
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }
    }
}

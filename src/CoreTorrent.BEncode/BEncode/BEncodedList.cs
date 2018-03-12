using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreTorrent.BEncode
{
	public class BEncodedList : BEncodedValue, IList<BEncodedValue>
	{
		#region Fields and properties

		private List<BEncodedValue> _items;

		public int Count => _items.Count;

		public BEncodedValue this[int index]
		{
			get => _items[index];
			set => _items[index] = value;
		}

		public bool IsReadOnly => false;

		public override int LengthInBytes
		{
			get
			{
				int length = 1;
				foreach (var item in _items) length += item.LengthInBytes;
				return length;
			}
		}
		#endregion

		#region Constructors

		public BEncodedList() : this(4) { }

		public BEncodedList(int capacity)
		{
			_items = new List<BEncodedValue>(capacity);
		}

		public BEncodedList(IEnumerable<BEncodedValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException(nameof(collection));
			}

			_items = new List<BEncodedValue>(collection);
		}
		#endregion

		#region Class methods

		public void Add(BEncodedValue item) => _items.Add(item);

		public void AddRange(IEnumerable<BEncodedValue> collection) => _items.AddRange(collection);

		public void Clear() => _items.Clear();

		public bool Contains(BEncodedValue item) => _items.Contains(item);

		public void CopyTo(BEncodedValue[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);

		public IEnumerator<BEncodedValue> GetEnumerator() => _items.GetEnumerator();

		public int IndexOf(BEncodedValue item) => _items.IndexOf(item);

		public void Insert(int index, BEncodedValue item) => _items.Insert(index, item);

		public bool Remove(BEncodedValue item) => _items.Remove(item);

		public void RemoveAt(int index) => _items.RemoveAt(index);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override bool Equals(object obj)
		{
			BEncodedList other = obj as BEncodedList;

			return (other != null) && (Count == other.Count) && Equals(other);
		}

		public bool Equals(BEncodedList other)
		{
			for (int index = 0; index < _items.Count; ++index)
			{
				if (!_items[index].Equals(other._items[index]))
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hash = 0;

			foreach (var item in _items) hash ^= item.GetHashCode();

			return hash;
		}

		public override string ToString()
		{
			return string.Join(",", _items);
		}
		#endregion
	}
}
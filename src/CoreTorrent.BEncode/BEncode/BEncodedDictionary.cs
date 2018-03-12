using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CoreTorrent.BEncode
{
	public class BEncodedDictionary : BEncodedValue, IDictionary<BEncodedString, BEncodedValue>
	{
		#region Fields and properties

		private SortedDictionary<BEncodedString, BEncodedValue> _dictionary;

		public int Count => _dictionary.Count;

		public BEncodedValue this[BEncodedString key]
		{
			get => _dictionary[key];
			set => _dictionary[key] = value;
		}

		public bool IsReadOnly => false;

		public ICollection<BEncodedString> Keys => _dictionary.Keys;

		public ICollection<BEncodedValue> Values => _dictionary.Values;

		public override int LengthInBytes => throw new NotImplementedException();
		#endregion

		#region Constructors

		public BEncodedDictionary()
		{
			_dictionary = new SortedDictionary<BEncodedString, BEncodedValue>();
		}
		#endregion

		#region Class methods

		public void Add(BEncodedString key, BEncodedValue value) => _dictionary.Add(key, value);

		public void Add(KeyValuePair<BEncodedString, BEncodedValue> item) => _dictionary.Add(item.Key, item.Value);

		public void Clear() => _dictionary.Clear();

		public bool Contains(KeyValuePair<BEncodedString, BEncodedValue> item)
		{
			return _dictionary.TryGetValue(item.Key, out var value) && value.Equals(item.Value);
		}

		public bool ContainsKey(BEncodedString key) => _dictionary.ContainsKey(key);

		public void CopyTo(KeyValuePair<BEncodedString, BEncodedValue>[] array, int arrayIndex) => _dictionary.CopyTo(array, arrayIndex);

		public bool Remove(BEncodedString key) => _dictionary.Remove(key);

		public bool Remove(KeyValuePair<BEncodedString, BEncodedValue> item) => _dictionary.Remove(item.Key);

		public bool TryGetValue(BEncodedString key, out BEncodedValue value) => _dictionary.TryGetValue(key, out value);

		public IEnumerator<KeyValuePair<BEncodedString, BEncodedValue>> GetEnumerator() => _dictionary.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public override bool Equals(object obj)
		{
			var other = obj as BEncodedDictionary;

			return (other != null) && (Count == other.Count) && Equals(other);
		}

		public bool Equals(BEncodedDictionary other)
		{
			foreach (var item in _dictionary)
			{
				if (other.TryGetValue(item.Key, out var value) && item.Value.Equals(value))
					continue;
				else return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hash = 0;

			foreach (var item in _dictionary)
			{
				hash ^= item.Key.GetHashCode();
				hash ^= item.Value.GetHashCode();
			}

			return hash;
		}
		#endregion
	}
}
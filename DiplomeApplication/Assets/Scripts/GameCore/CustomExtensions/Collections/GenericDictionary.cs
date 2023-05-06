using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable Unity.RedundantHideInInspectorAttribute
// ReSharper disable Unity.RedundantSerializeFieldAttribute

namespace GameCore.CustomExtensions.Collections
{
	[Serializable]
	public class GenericDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField] private List<KeyValuePair> list = new List<KeyValuePair>();
		[SerializeField] private Dictionary<TKey, int> indexByKey = new Dictionary<TKey, int>();

		[SerializeField, HideInInspector] private Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

		[Serializable]
		private struct KeyValuePair
		{
			public TKey key;
			public TValue value;

			public KeyValuePair(TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
			}
		}

		// Since lists can be serialized natively by Unity no custom implementation is needed
		public void OnBeforeSerialize()
		{
		}

		// Fill dictionary with list pairs and flag key-collisions.
		public void OnAfterDeserialize()
		{
			dict.Clear();
			indexByKey.Clear();

			for (int i = 0; i < list.Count; i++)
			{
				TKey key = list[i].key;
				if (key != null && !ContainsKey(key))
				{
					dict.Add(key, list[i].value);
					indexByKey.Add(key, i);
				}
			}
		}

		// IDictionary
		public TValue this[TKey key]
		{
			get => dict[key];
			set
			{
				dict[key] = value;
				if (indexByKey.ContainsKey(key))
				{
					int index = indexByKey[key];
					list[index] = new KeyValuePair(key, value);
				}
				else
				{
					list.Add(new KeyValuePair(key, value));
					indexByKey.Add(key, list.Count - 1);
				}
			}
		}

		public ICollection<TKey> Keys => dict.Keys;
		public ICollection<TValue> Values => dict.Values;

		public void Add(TKey key, TValue value)
		{
			dict.Add(key, value);
			list.Add(new KeyValuePair(key, value));
			indexByKey.Add(key, list.Count - 1);
		}

		public bool ContainsKey(TKey key) => dict.ContainsKey(key);

		public bool Remove(TKey key)
		{
			if (dict.Remove(key))
			{
				int index = indexByKey[key];
				list.RemoveAt(index);
				indexByKey.Remove(key);
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool TryGetValue(TKey key, out TValue value) => dict.TryGetValue(key, out value);

		// ICollection
		public int Count => dict.Count;
		public bool IsReadOnly { get; set; }

		public void Add(KeyValuePair<TKey, TValue> pair)
		{
			Add(pair.Key, pair.Value);
		}

		public void Clear()
		{
			dict.Clear();
			list.Clear();
			indexByKey.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> pair)
		{
			return dict.TryGetValue(pair.Key, out var value) && EqualityComparer<TValue>.Default.Equals(value, pair.Value);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentException("The array cannot be null.");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException();
			if (array.Length - arrayIndex < dict.Count)
				throw new ArgumentException("The destination array has fewer elements than the collection.");
			foreach (KeyValuePair<TKey, TValue> pair in dict)
			{
				array[arrayIndex] = pair;
				arrayIndex++;
			}
		}

		public bool Remove(KeyValuePair<TKey, TValue> pair)
		{
			if (dict.TryGetValue(pair.Key, out var value))
			{
				bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
				if (valueMatch)
				{
					return Remove(pair.Key);
				}
			}

			return false;
		}

		// IEnumerable
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dict.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => dict.GetEnumerator();
	}
}
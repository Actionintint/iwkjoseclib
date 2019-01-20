using System.Collections.Generic;

public class Map<TKey, TValue> : Dictionary<TKey, TValue>
{
    public Map() { }
    public Map(int capacity) : base(capacity) { }
    public Map(IEqualityComparer<TKey> comparer) : base(comparer) { }
    public Map(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
    public Map(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
    public Map(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
    public Map(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection) { }
    public Map(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) : base(collection, comparer) { }

    public new TValue this[TKey key]
    {
        get => TryGetValue(key, out var value) ? value : default;
        set => base[key] = value;
    }
}

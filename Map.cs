class Map<TKey, TValue> : Dictionary<TKey, TValue>
{
    public Map() { }
    public Map(int capacity) : base(capacity) { }
    public Map(IEqualityComparer<TKey> comparer) : base(comparer) { }
    public Map(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
    public Map(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }
    public Map(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }
    public Map(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    public new TValue this[TKey key]
    {
        get
        {
            TValue value;
            return TryGetValue(key, out value) ? value : base[key] = default(TValue);
        }
        set
        {
            base[key] = value;
        }
    }
}

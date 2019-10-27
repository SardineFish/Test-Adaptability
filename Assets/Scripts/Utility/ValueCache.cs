using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct BooleanCache
{
    public float CacheTime { get; set; }

    public bool Value => cachedValues is null ? false : cachedValues.Count > 0;

    Queue<float> cachedValues;

    public BooleanCache(float cacheTime)
    {
        cachedValues = new Queue<float>();
        CacheTime = cacheTime;
    }

    public void Record(float time)
    {
        cachedValues.Enqueue(time);
    }
    public void Update(float time)
    {
        while (cachedValues.Count > 0)
        {
            if ((time - cachedValues.Peek()) > CacheTime)
                cachedValues.Dequeue();
            else
                return;
        }
    }
    public void Clear()
        => cachedValues.Clear();

    public static implicit operator bool(BooleanCache value)
        => value.Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}

public class ValueCache<T> : IEnumerable<T>
{
    struct CachedValue<T>
    {
        public float Time;
        public T Value;
    }

    public float CacheTime { get; set; }
    Queue<CachedValue<T>> cachedValues;

    public ValueCache(float cacheTime)
    {
        CacheTime = cacheTime;
    }
    public void Record(T value, float time)
    {
        cachedValues.Enqueue(new CachedValue<T>() { Time = time, Value = value });
    }
    public void Update(float time)
    {
        while (cachedValues.Count > 0)
        {
            if ((time - cachedValues.Peek().Time) > CacheTime)
                cachedValues.Dequeue();
            else
                return;
        }
    }
    public void Clear()
        => cachedValues.Clear();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var value in cachedValues)
            yield return value.Value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
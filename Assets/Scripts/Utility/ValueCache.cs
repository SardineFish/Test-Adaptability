using System;
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

    public static implicit operator bool(BooleanCache value)
        => value.Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}
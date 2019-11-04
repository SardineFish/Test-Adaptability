using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    public class TiledDataCollection<T> : Project.GameMap.IDataMap<T> where T: class
    {
        Dictionary<Vector2Int, T> blockData = new Dictionary<Vector2Int, T>();
        public T this[Vector2Int pos]
            => Get(pos);

        public TiledDataCollection() : this(16) { }
        public TiledDataCollection(int capacity)
        {
            blockData = new Dictionary<Vector2Int, T>(capacity);
        }

        public T Get(Vector2Int pos)
            => blockData.ContainsKey(pos) ? blockData[pos] : null;
        public void Set(Vector2Int pos, T data)
            => blockData[pos] = data;
        public bool Has(Vector2Int pos)
            => blockData.ContainsKey(pos);

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var pair in blockData)
                yield return pair.Value;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

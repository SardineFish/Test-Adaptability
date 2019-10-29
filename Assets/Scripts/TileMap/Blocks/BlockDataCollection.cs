using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;

namespace Project.Blocks
{
    public class BlockDataCollection<T> : IEnumerable<T> where T: class
    {
        Dictionary<Vector2Int, T> blockData = new Dictionary<Vector2Int, T>();
        public T this[Vector2Int pos]
            => GetBlockAt(pos);

        public BlockDataCollection() : this(16) { }
        public BlockDataCollection(int capacity)
        {
            blockData = new Dictionary<Vector2Int, T>(capacity);
        }

        public T GetBlockAt(Vector2Int pos)
            => blockData.ContainsKey(pos) ? blockData[pos] : null;
        public T SetDataAt(Vector2Int pos, T data)
            => blockData[pos] = data;

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

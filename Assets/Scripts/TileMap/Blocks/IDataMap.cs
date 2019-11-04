using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.GameMap
{
    public interface IDataMap<T> : IEnumerable<T>
    {
        T Get(Vector2Int position);
        void Set(Vector2Int position, T data);
        bool Has(Vector2Int position);
    }
}

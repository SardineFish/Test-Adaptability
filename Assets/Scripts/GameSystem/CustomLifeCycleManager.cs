using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CustomLifeCycleManager<ComponentT> : Singleton<CustomLifeCycleManager<ComponentT>> where ComponentT : MonoBehaviour
{
    public static List<ComponentT> Entities;
    //public List<GameEntity> Entities = new List<GameEntity>();
    // Use this for initialization
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Entities = Entities.Where(entity => entity).ToList();
    }

    public static void RegisterEntity(ComponentT entity)
    {
        if (Entities == null)
            Entities = new List<ComponentT>();
        Entities.Add(entity);
    }

    public static void RemoveEntity(ComponentT entity)
    {
        Entities.Remove(entity);
    }

    public static ComponentT FindEntity(string name)
    {
        return Entities
            .Where(entity => IsInHierarchy(entity) && entity.gameObject.name == name)
            .FirstOrDefault();
    }

    public static ComponentT Find(string name)
        => FindEntity(name);

    public static EntityT FindEntity<EntityT>(string name) where EntityT : ComponentT
    {
        return Entities
            .Where(entity => IsInHierarchy(entity) && entity is EntityT && entity.name == name)
            .FirstOrDefault() as EntityT;
    }

    public static EntityT[] FindEntities<EntityT>() where EntityT : ComponentT
    {
        return Entities
            .Where(entity => IsInHierarchy(entity) && entity is EntityT)
            .Select(entity => entity as EntityT)
            .ToArray();
    }

    public static bool IsInHierarchy(ComponentT entity)
    {
        return entity
            && entity.gameObject
            && entity.gameObject.scene != null
            && entity.gameObject.scene.name != null;
    }

    public static bool IsPrefab(ComponentT entity)
    {
        return entity
            && entity.gameObject
            && (entity.gameObject.scene == null || entity.gameObject.scene.name == null);
    }
}

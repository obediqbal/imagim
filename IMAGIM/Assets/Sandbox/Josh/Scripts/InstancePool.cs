using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Sandbox.Josh.Scripts
{
    public class InstancePool : MonoBehaviour
    {
        public static InstancePool inst;
        [SerializeField] Transform[] poolables;
        Dictionary<Type, Transform> transforms = new Dictionary<Type, Transform>();
        Dictionary<Type, Queue<Poolable>> inactivePool = new Dictionary<Type, Queue<Poolable>>();
        Dictionary<Type, List<Poolable>> activePool = new Dictionary<Type, List<Poolable>>();
        [SerializeField] bool showDebug;
        [ReadOnly]
        [SerializeField] List<DebugInstance> debugInstances = new List<DebugInstance>();
        public enum Type {
            DeployableUnit,
            Tower,
            Player,
        }
        private void Awake()
        {
            if (inst == null)
            {
                inst = this;
                for (int i = 0; i < poolables.Length; i++)
                {
                    Poolable poolable = poolables[i].GetComponent<Poolable>();
                    inactivePool.Add(poolable.PoolType, new Queue<Poolable>());
                    activePool.Add(poolable.PoolType, new List<Poolable>());
                    transforms.Add(poolable.PoolType, poolables[i]);
                }
            } else
            {
                Destroy(this);
            }
        }
        public static Poolable Spawn(Type type)
        {
            try
            {
                Poolable poolable;
                if (inst.inactivePool[type].Count > 0)
                {
                    poolable = inst.inactivePool[type].Dequeue();
                } else
                {
                    Transform poolableTransform = Instantiate(inst.transforms[type]);
                    poolable = poolableTransform.GetComponent<Poolable>();
                }
                AddToPool(poolable);
                return poolable;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("InstancePool missing reference for type " + type);
                return null;
            }
        }
        public static bool Contains(Poolable poolable)
        {
            return inst.activePool[poolable.PoolType].Contains(poolable);
        }
        public static void AddToPool(Poolable poolable)
        {
            inst.activePool[poolable.PoolType].Add(poolable);
            poolable.Spawn();
        }
        public static void Despawn(Poolable poolable)
        {
            try
            {
                poolable.Despawn();
                Type type = poolable.PoolType;
                inst.activePool[type].Remove(poolable);
                inst.inactivePool[type].Enqueue(poolable);
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError("InstancePool missing reference for type " + poolable.PoolType);
            }
        }
        /// <summary>
        /// Returns the list of active instances
        /// </summary>
        /// <param name="type">Type of instance</param>
        /// <returns>Instance list</returns>
        public static List<Poolable> FindInstances(Type type)
        {
            return inst.activePool[type];
        }
        private void Update()
        {
            if (showDebug)
            {
                debugInstances.Clear();
                foreach (KeyValuePair<Type, List<Poolable>> item in activePool)
                {
                    debugInstances.Add(new DebugInstance(item.Key.ToString(), item.Value.Count));
                }
            }
        }
        [System.Serializable]
        public class DebugInstance
        {
            public string name;
            public int count;
            public DebugInstance(string name, int count)
            {
                this.name = name;
                this.count = count;
            }
        }
    }
}
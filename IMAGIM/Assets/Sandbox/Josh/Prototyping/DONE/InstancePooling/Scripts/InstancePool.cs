using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.InstancePooling
{
    [System.Serializable]
    public class InstancePool
    {
        Transform _transform;
        Transform _parent;
        Queue<Transform> _inactivePool = new Queue<Transform>();
        List<Transform> _activePool = new List<Transform>();
        public List<Transform> ActivePool { get { return _activePool; } }
#if UNITY_EDITOR
        [ReadOnly]
        [SerializeField] int _activeInstanceCount;
#endif
        /// <summary>
        /// Creates a new InstancePool that instantiates automatically from pool
        /// </summary>
        /// <param name="prefab">Prefab to create if no transforms are already made</param>
        /// <param name="parent">Created prefab parent</param>
        public InstancePool(Transform prefab, Transform parent)
        {
            _transform = prefab;
            _parent = parent;
        }
        /// <summary>
        /// Creates a new Transform, used as replacement for Instantiate. Activates the GameObject
        /// </summary>
        /// <returns>Transform of the object</returns>
        public Transform InstantiateFromPool()
        {
            Transform newTransform;
            if (_inactivePool.Count > 0)
            {
                newTransform = _inactivePool.Dequeue();
            }
            else
            {
                newTransform = Object.Instantiate(_transform, _parent);
            }
            newTransform.gameObject.SetActive(true);
            _activePool.Add(newTransform);
#if UNITY_EDITOR
            _activeInstanceCount = _activePool.Count;
#endif
            return newTransform;
        }
        /// <summary>
        /// Creates a new Transform, used as replacement for Instantiate(). Activates the GameObject
        /// </summary>
        /// <param name="position">Position of created object</param>
        /// <param name="rotation">Rotation of created object</param>
        /// <returns>Transform of the object</returns>
        public Transform InstantiateFromPool(Vector3 position, Quaternion rotation)
        {
            Transform newTransform;
            if (_inactivePool.Count > 0)
            {
                newTransform = _inactivePool.Dequeue();
                newTransform.position = position;
                newTransform.rotation = rotation;
            }
            else
            {
                newTransform = Object.Instantiate(_transform, position, rotation, _parent);
            }
            newTransform.gameObject.SetActive(true);
            _activePool.Add(newTransform);
#if UNITY_EDITOR
            _activeInstanceCount = _activePool.Count;
#endif
            return newTransform;
        }
        /// <summary>
        /// Removes a transform and adds into pool, replacement for Destroy(). Deactivates the GameObject
        /// </summary>
        /// <param name="transform">Transform to remove</param>
        public void DestroyToPool(Transform transform)
        {
            _activePool.Remove(transform);
            _inactivePool.Enqueue(transform);
            transform.gameObject.SetActive(false);
#if UNITY_EDITOR
            _activeInstanceCount = _activePool.Count;
#endif
        }
    }
}
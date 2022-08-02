using DKH.InstancePooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DKH.Debugging
{
    public class DemoInstancePooling : MonoBehaviour
    {
        public static DemoInstancePooling Instance;
        [SerializeField]
        Transform _transform;
        [SerializeField]
        InstancePool _circles;
        public InstancePool Circles { get { return _circles; } }
        private void Awake()
        {
            Instance = this;
            _circles = new InstancePool(_transform, transform);
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _circles.InstantiateFromPool(pos, Quaternion.identity);
            }
        }
    }
}
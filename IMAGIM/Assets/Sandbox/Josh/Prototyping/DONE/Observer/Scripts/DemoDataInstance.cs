using DKH.ObserverSystem;
using DKH.ResourceSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.Debugging
{
    public class DemoDataInstance : MonoBehaviour
    {
        [Range(0, 100)]
        public float TestValue;
        Observable<float> _testValue;
        [Header("References")]
        [SerializeField]
        DemoNumberDisplay demoNumberDisplay;
        [SerializeField]
        Resource coin;

        private void Awake()
        {
            _testValue = new Observable<float>(TestValue);
        }
        private void OnEnable()
        {
            _testValue.AddObserver(onTestValueChanged);
        }
        private void OnDisable()
        {
            _testValue.RemoveObserver(onTestValueChanged);
        }
        private void onTestValueChanged(float val)
        {
            demoNumberDisplay.SetText(val.ToString());
            TestValue = val;
            if (coin != null)
            {
                coin.SetValue(val);
            }
        }

        public void AddValue()
        {
            _testValue.Value += 1;
        }

        public void RemoveValue()
        {
            _testValue.Value -= 1;
        }

        private void OnValidate()
        {
            if (!Application.isPlaying)
                return;
            if (_testValue == null)
                return;
            _testValue.Value = TestValue;
        }
    }
}
using DKH.ObserverSystem;
using DKH.ResourceSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fota : MonoBehaviour
{
    Observable<float> _testValue;
    [Header("References")]
    [SerializeField]
    Slider slider;
    [SerializeField]
    Resource fota;

    private void Update()
    {
        slider.value = fota.GetValue();
    }
}

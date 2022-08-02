using DKH.ObserverSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DKH.Debugging
{
    public class DemoNumberDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text tmpText;
        public void SetText(string text)
        {
            tmpText.text = text;
        }
    }
}
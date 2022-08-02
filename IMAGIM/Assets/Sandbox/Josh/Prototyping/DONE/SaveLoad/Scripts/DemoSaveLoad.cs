using DKH.ResourceSystem;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace DKH.Debugging
{
    public class DemoSaveLoad : MonoBehaviour
    {
        public Text debugText;
        public Resource coin;
        private void Update()
        {
            debugText.text = coin.GetValue().ToString();
        }
        public void debugAdd()
        {
            coin.AddValue(1);
        }
        public void debugRemove()
        {
            coin.AddValue(-1);
        }
    }
}
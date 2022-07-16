using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sandbox.Josh.Scripts
{
    public class DebugSaveLoad : MonoBehaviour
    {
        public Text debugText;
        public ResourceSO coin;
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
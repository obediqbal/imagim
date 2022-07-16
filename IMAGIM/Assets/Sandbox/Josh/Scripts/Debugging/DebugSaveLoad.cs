using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sandbox.Josh.Scripts
{
    public class DebugSaveLoad : MonoBehaviour
    {
        public Text debugText;
        public GameDataHolder dataHolder;
        private void Update()
        {
            debugText.text = dataHolder.Resources[0].GetValue().ToString();
        }
        public void debugAdd()
        {
            dataHolder.Resources[0].AddValue(1);
        }
        public void debugRemove()
        {
            dataHolder.Resources[0].AddValue(-1);
        }
    }
}
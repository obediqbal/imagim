using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Sandbox.Josh.Scripts
{
    public class GameDataHolder : MonoBehaviour
    {
        public ResourceSO[] Resources;
        public GameData GameData;
        private void Awake()
        {
            for (int i = 0; i < Resources.Length; i++)
            {
                Resources[i].SetDefaultValue();
            }
        }
        public void Load()
        {
            GameData.ResourceValues = SaveSystem.LoadGameData<GameData>(GameData.s_DataFileName).ResourceValues;
            for (int i = 0; i < Resources.Length; i++)
            {
                Resources[i].SetValue(GameData.ResourceValues[i]);
            }
        }

        public void Save()
        {
            GameData.ResourceValues = new float[Resources.Length];
            for (int i = 0; i < Resources.Length; i++)
            {
                GameData.ResourceValues[i] = Resources[i].GetValue();
            }
            SaveSystem.SaveGameData(GameData);
        }
    }
}
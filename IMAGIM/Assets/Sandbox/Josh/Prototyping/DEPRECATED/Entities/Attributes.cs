using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DKH.DeprecatedEntitySystem
{
    /// <summary>
    /// Represents attribute values that can be modified for an entity.
    /// </summary>
    [Obsolete("The class is deprecated.", true)]
    public class Attributes
    {
        public enum Type
        {
            GenericMaxHealth,
            PlayerMaxMana,
            IgnoreThis,
        }
        public float[] values;
        public Attributes()
        {
            values = new float[(int)Type.IgnoreThis];
        }
        public void SetAttributeValue(Type type, float value)
        {
            values[(int)type] = value;
        }
        public void AddAttributeValue(Type type, float value)
        {
            values[(int)type] += value;
        }
        public float GetAttributeValue(Type type)
        {
            return values[(int)type];
        }
    }
}
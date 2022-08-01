using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bar : MonoBehaviour
{
    public Slider Slider;
    public Gradient Gradient;
    public Image fill;

    public void SetMax(int value)
    {
        Slider.maxValue = value;
        Slider.value = value;
        fill.color = Gradient.Evaluate(1f);
    }
    
    public void SetValue(int value)
    {
        Slider.value = value;
        fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu(menuName: "Game Units/Health Bar")]
public class Health : MonoBehaviour
{
    public Slider slider;

    public void DisplayMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void DisplayHealth(float health)
    {
        slider.value = health;
    }
}

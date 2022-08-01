using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coin : MonoBehaviour
{
    public Text coincount;
    public health health;
    void Update()
    {
        coincount.text = health.coins.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUFF : MonoBehaviour
{
    bool buffActive;
    public GameObject buff;
    private void Start()
    {
        GetComponent<GameObject>();
        buffActive = false;
    }
    private void Update()
    {
        if (buffActive)
        {
            buff.SetActive(true);
        }
        if (!buffActive)
        {
            buff.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ATKBUFF();
        }
    }
    public void ATKBUFF()
    {
        buffActive = true;
        Debug.Log("Skill Activated");
        StartCoroutine(ATKUP());
    }

    IEnumerator ATKUP()
    {
        yield return new WaitForSeconds(5f);
        buffActive = false;
    }
}

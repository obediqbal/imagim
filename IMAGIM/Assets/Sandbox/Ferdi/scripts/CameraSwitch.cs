using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] GameObject CameraMove;
    [SerializeField] GameObject CameraPlayer;
    float movementSpeed;
    float horizontalInput;
    bool followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 50f;
        CameraMove.SetActive(false);
        followPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if(followPlayer == true)
            {
                CameraMove.transform.position = CameraPlayer.transform.position;
                followPlayer = false;
            }
            horizontalInput = 1;
            CameraPlayer.SetActive(false);
            CameraMove.SetActive(true);
        } 
        else if (Input.GetKey(KeyCode.J))
        {
            if (followPlayer == true)
            {
                CameraMove.transform.position = CameraPlayer.transform.position;
                followPlayer = false;
            }
            horizontalInput = -1;
            CameraPlayer.SetActive(false);
            CameraMove.SetActive(true);
        } 
        else
        {
            horizontalInput = 0;
        }
        CameraMove.transform.position = CameraMove.transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.K))
        {
            followPlayer = true;
            CameraMove.transform.position = CameraPlayer.transform.position;
            CameraMove.SetActive(false);
            CameraPlayer.SetActive(true);
        }
    }
}

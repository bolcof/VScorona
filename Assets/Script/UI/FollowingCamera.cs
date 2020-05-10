using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject PlayerObj;
    private bool iPhoneX;

    private void Start()
    {
        iPhoneX = Screen.width * 2 <= Screen.height ? true : false;
    }

    private void Update()
    {
        if (!iPhoneX)
        {
            this.gameObject.transform.position = new Vector3(0, 8, PlayerObj.transform.position.z - 17);
        }
        else
        {
            this.gameObject.transform.position = new Vector3(0, 9.6f, PlayerObj.transform.position.z - 20.4f);
        }
    }
}

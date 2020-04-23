using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public GameObject PlayerObj;

    private void Update()
    {
        this.gameObject.transform.position = new Vector3(0, 5, PlayerObj.transform.position.z - 10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMaker : MonoBehaviour
{
    public GameObject GroundObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(GroundObj, new Vector3(5.0f, 0.0f, this.gameObject.transform.position.z + 300.0f), Quaternion.Euler(0.0f, 90.0f, 0.0f), GameObject.Find("GroundRoot").transform);
        }
    }
}

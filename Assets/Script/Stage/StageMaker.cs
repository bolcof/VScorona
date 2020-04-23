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
            Instantiate(GroundObj, new Vector3(0.0f, 0.0f, this.gameObject.transform.position.z + 150.0f), Quaternion.identity, GameObject.Find("GroundRoot").transform.parent);
        }
    }
}

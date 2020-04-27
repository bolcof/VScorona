using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject PlayerObj;
    // Start is called before the first frame update
    void Start()
    {
        PlayerObj = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(0, 5, PlayerObj.transform.position.z - 60);
    }

    private void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }
}

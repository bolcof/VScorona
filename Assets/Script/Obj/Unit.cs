using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Slot;
    public GameObject[] ObjRoots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn(GameObject Obj, int posID)
    {
        Instantiate(Obj, ObjRoots[posID].transform.position, Quaternion.identity, ObjRoots[posID].transform);
    }
}

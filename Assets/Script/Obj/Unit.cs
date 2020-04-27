using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int slot;
    public GameObject[] ObjRoots;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Spawn(GameObject[] Objs)
    {
        for(int i = 0; i < slot; i++)
        {
            Instantiate(Objs[i], ObjRoots[i].transform.position, Quaternion.identity, ObjRoots[i].transform);
        }
    }
}

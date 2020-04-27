using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaker : MonoBehaviour
{
    public GameObject ObjRoot;

    public GameObject Man_standing, Man_walking;
    public GameObject[] Shop, Customer;
    public GameObject Amabie;

    public GameObject[] UnitFrame = new GameObject[2];

    private GameObject[][] ObjLog = new GameObject[10][];
    private GameObject MustObj;

    public GameState Gstate;

    public void Start()
    {
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
        ObjRoot = GameObject.Find("ObjectRoot");
    }

    public void Spawn(float posZ)
    {
        GameObject Unit = Instantiate(UnitFrame[Random.Range(0, 2)], new Vector3(0.0f, 0.0f, posZ), Quaternion.identity, ObjRoot.transform);

        if (!didNeedMust())
        {
            Unit.GetComponent<Unit>().Spawn(RandomizeEnemy(Unit.GetComponent<Unit>().slot));
        }
        else
        {

        }
    }

    public GameObject[] RandomizeEnemy(int slot)
    {
        GameObject[] Objs = new GameObject[slot];
        /* 0,0,1,1,1,2  Lv.1 : 1,1,1,2,2,2  Lv.2 : 1,1,2,2,2,3  Lv.3 : 1,2,2,3,3,3  Lv.4 : 2,2,3,3,3,4 */
        int[,] EnemyNumLib = { { 0, 0, 1, 1, 1, 2 }, { 1, 1, 1, 2, 2, 2 }, { 1, 1, 2, 2, 2, 3 }, { 1, 2, 2, 3, 3, 3 }, { 2, 2, 3, 3, 3, 4 } };

        int EnemyNum = EnemyNumLib[Gstate.Level, Random.Range(0, 6)];
        

        return Objs;
    }

    public GameObject[] RandomizeEnemy(int slot, GameObject must)
    {
        GameObject[] Objs = new GameObject[slot];

        return Objs;
    } 

    private bool didNeedMust()
    {
        return false;
    }

    private bool[] Combination (int n, int r)
    {
        bool[] tmp = new bool[n];
        for(int i = 0; i < n; i++)
        {
            if (i < r)
            {
                tmp[i] = true;
            }
            else
            {
                tmp[i] = false;
            }
        }

        for(int i = 0; i < n; i++)
        {
            int j = Random.Range(0, 100000) % n;
            bool t = tmp[i];
            tmp[i] = tmp[j];
            tmp[j] = t;
        }

        return tmp;
    }
}

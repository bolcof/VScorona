using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaker : MonoBehaviour
{
    private GameObject ObjRoot, PlayerObj;
    private int exPlayerObjPos = 0;
    private int exSpawnPos = 100;
    private int distance = 10;

    public GameObject Man_standing, Man_walking;
    public GameObject[] Shop, Customer;
    public GameObject Amabie;

    public GameObject[] UnitFrame = new GameObject[2];
    int[,] EnemyNumLib = { { 0, 0, 1, 1, 1, 2 }, { 1, 1, 1, 2, 2, 2 }, { 1, 1, 2, 2, 2, 3 }, { 1, 2, 2, 3, 3, 3 }, { 2, 2, 3, 3, 3, 4 } };

    private GameObject[][] ObjLog = new GameObject[10][];
    private GameObject MustObj;

    public GameState Gstate;

    public void Start()
    {
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
        ObjRoot = GameObject.Find("ObjectRoot");
        PlayerObj = GameObject.FindWithTag("Player");

        for(int i = 30; i < 100; i += 10)
        {
            Spawn(i, 0);
        }
    }

    public void Update()
    {
        if (PlayerObj.transform.position.z >= exPlayerObjPos + distance)
        {
            Spawn(exSpawnPos + distance, Gstate.Level);
            exPlayerObjPos = Mathf.FloorToInt(PlayerObj.transform.position.z) + distance;
        }
    }

    public void Spawn(float posZ, int Level)
    {
        GameObject Unit = Instantiate(UnitFrame[1], new Vector3(0.0f, 0.0f, posZ), Quaternion.identity, ObjRoot.transform);

        int EnemyNum = EnemyNumLib[Level, Random.Range(0, 6)];

        Debug.Log(Unit.GetComponent<Unit>().Slot);

        bool[] tmp = Combination(Unit.GetComponent<Unit>().Slot, EnemyNum);
        for(int i = 0; i < Unit.GetComponent<Unit>().Slot; i++)
        {
            if (tmp[i])
            {
                Unit.GetComponent<Unit>().Spawn(Man_standing, i);
            }

        }
    }

    private bool[] Combination (int size, int trueNum)
    {
        bool[] tmp = new bool[size];
        for(int i = 0; i < size; i++)
        {
            if (i < trueNum)
            {
                tmp[i] = true;
            }
            else
            {
                tmp[i] = false;
            }
        }

        for(int i = 0; i < size; i++)
        {
            int j = Random.Range(0, 100000) % size;
            bool t = tmp[i];
            tmp[i] = tmp[j];
            tmp[j] = t;
        }

        return tmp;
    }

    private void ChangeObj(GameObject target)
    {

    }
}

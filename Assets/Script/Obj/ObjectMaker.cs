using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaker : MonoBehaviour
{
    private GameObject ObjRoot, PlayerObj;
    private int exPlayerObjPos = 0;
    private int exSpawnPos = 232;
    private int distance = 16;

    public GameObject Virus;
    public GameObject[] Shop, Customer;
    public GameObject Amabie;

    public GameObject[] UnitFrame = new GameObject[2];
    int[,] EnemyNumLib = { { 0, 0, 1, 1, 1, 2 }, { 1, 1, 1, 2, 2, 2 }, { 1, 1, 2, 2, 2, 3 }, { 1, 2, 2, 3, 3, 3 }, { 2, 2, 3, 3, 3, 4 } };

    private GameObject[][] ObjLog = new GameObject[10][];
    private GameObject MustObj;
    private int NoShopLog = 0;

    public GameState Gstate;

    public void Start()
    {
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
        ObjRoot = GameObject.Find("ObjectRoot");
        PlayerObj = GameObject.FindWithTag("Player");

        for(int i = 24; i <= 232; i += 16)
        {
            Spawn(i, 0);
        }
    }

    public void Update()
    {
        if (PlayerObj.transform.position.z >= exPlayerObjPos + distance)
        {
            Spawn(exSpawnPos + distance, Gstate.Level);
            exSpawnPos += distance;
            exPlayerObjPos += distance;
        }
    }

    public void Spawn(float posZ, int Level)
    {
        GameObject Unit = Instantiate(UnitFrame[1], new Vector3(0.0f, 0.0f, posZ), Quaternion.identity, ObjRoot.transform);

        int EnemyNum = EnemyNumLib[Level, Random.Range(0, 6)];

        bool[] tmp = Combination(Unit.GetComponent<ObjUnit>().Slot, EnemyNum);
        for(int i = 0; i < Unit.GetComponent<ObjUnit>().Slot; i++)
        {
            if (tmp[i])
            {
                Unit.GetComponent<ObjUnit>().Spawn(Virus, i);
            }

        }

        float seed = Random.Range(0, 100.0f);
        if (seed <= NoShopLog * 2.5f + 3.0f)
        {
           // Debug.Log(NoShopLog * 2.5f + 3.0f);
            ChangeObj(Shop[Random.Range(0, Shop.Length)], posZ);
            NoShopLog = 0;
        }
        else
        {
            NoShopLog++;
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

    private void ChangeObj(GameObject target, float posZ)
    {
        float seed = Random.Range(0.0f, 1.0f);
        if(seed > 0.5f) {
            Instantiate(target, new Vector3(4.25f, 0.0f, posZ + 8.0f), Quaternion.identity, ObjRoot.transform);
        }
        else
        {
            Instantiate(target, new Vector3(-4.25f, 0.0f, posZ + 8.0f), Quaternion.identity, ObjRoot.transform);
        }
    }

    public void GetDish(int id)
    {

    }
}

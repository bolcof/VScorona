using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMaker : MonoBehaviour
{
    private GameObject ObjRoot, PlayerObj;
    private int exPlayerObjPos = 0;
    private int exSpawnPos = 216;
    private int distance = 18;

    public GameObject Virus;
    public GameObject[] Shop, Customer;
    public GameObject Amabie;

    public GameObject[] UnitFrame = new GameObject[2];
    int[,] EnemyNumLib = { { 0, 0, 1, 1, 1, 2 }, { 1, 1, 1, 2, 2, 2 }, { 1, 1, 2, 2, 2, 3 }, { 1, 2, 2, 3, 3, 3 }, { 2, 2, 3, 3, 3, 4 } };

    public Shop.DISHTYPE nowDishType  = global::Shop.DISHTYPE.NONE;

    private int NoShopLog = 0;
    private int NoMuchCustomerLog = 0;

    public GameState Gstate;

    public void Start()
    {
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
        ObjRoot = GameObject.Find("ObjectRoot");
        PlayerObj = GameObject.FindWithTag("Player");

        for(int i = 36; i <= exSpawnPos; i += distance)
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
            SpawnShop(Shop[Random.Range(0, Shop.Length)], posZ);
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

    private void SpawnShop(GameObject target, float posZ)
    {
        float seed = Random.Range(0.0f, 1.0f) > 0.5f ? 4.25f : -4.25f;
        float diff = Random.Range(0.0f, 1.0f) > 0.5f ? 11.0f : 4.0f;

        Instantiate(target, new Vector3(seed, 0.0f, posZ + diff), Quaternion.identity, ObjRoot.transform);
    }

    private void SpawnCustomer(GameObject target, float posZ)
    {
        float seed = Random.Range(0.0f, 1.0f) > 0.5f ? 4.25f : -4.25f;
        float diff = Random.Range(0.0f, 1.0f) > 0.5f ? 7.0f : 14.0f;

        Instantiate(target, new Vector3(seed, 0.0f, posZ + diff), Quaternion.identity, ObjRoot.transform);
    }

    private void SpawnSpecial(GameObject target, float posZ)
    {
        float seed = Random.Range(-2.0f, 2.0f);
        Instantiate(target, new Vector3(seed, 0.0f, posZ + 9.0f), Quaternion.identity, ObjRoot.transform);
    }

    public void GetDish(int id)
    {

    }
}

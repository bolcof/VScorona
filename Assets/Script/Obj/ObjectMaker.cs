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
    public GameObject Mask, Amabie;

    public GameObject[] UnitFrame = new GameObject[2];
    int[,] EnemyNumLib = { { 0, 0, 0, 1, 1, 1, 1}, { 0, 1, 1, 1, 1, 1, 2 }, { 1, 1, 1, 1, 2, 2, 2 }, { 1, 1, 2, 2, 2, 2, 3 }, { 1, 2, 2, 3, 3, 3, 3 } };

    public Shop.DISHTYPE nowDishType  = global::Shop.DISHTYPE.NONE;

    private int NoShopLog = 0;
    private int NoMuchCustomerLog = 0;
    private int NoSpecialLog = -20;

    public GameState Gstate;

    public void Start()
    {
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
        ObjRoot = GameObject.Find("ObjectRoot");
        PlayerObj = GameObject.FindWithTag("Player");

        for(int i = 36; i <= exSpawnPos; i += distance)
        {
            Spawn(i, 0);
            NoShopLog++;
            NoMuchCustomerLog++;
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
        //Virus
        GameObject Unit = Instantiate(UnitFrame[1], new Vector3(0.0f, 0.0f, posZ), Quaternion.identity, ObjRoot.transform);

        int EnemyNum = EnemyNumLib[Level, Random.Range(0, 7)];

        bool[] tmp = Combination(Unit.GetComponent<ObjUnit>().Slot, EnemyNum);
        for(int i = 0; i < Unit.GetComponent<ObjUnit>().Slot; i++)
        {
            if (tmp[i])
            {
                Unit.GetComponent<ObjUnit>().Spawn(Virus, i);
            }

        }

        //Shop
        float seed = Random.Range(0, 100.0f);
        float shopPosX = Random.Range(0.0f, 1.0f) > 0.5f ? 1.0f : -1.0f;
        if (seed <= NoShopLog * 2.5f + 3.0f)
        {
            shopPosX = SpawnShop(Shop[Random.Range(0, Shop.Length)], posZ);
            NoShopLog = 0;
        }
        else
        {
            NoShopLog++;
        }

        //Customer
        if (PlayerObj.GetComponent<PlayerBehaviour>().DishType != global::Shop.DISHTYPE.NONE)
        {
            if (NoMuchCustomerLog >= 6)
            {
                SpawnCustomer(Customer[PlayerObj.GetComponent<PlayerBehaviour>().DishType.GetHashCode() - 1], posZ, shopPosX);
                NoMuchCustomerLog = 0;
            }
            else
            {
                int seeds = Random.Range(0, 7);
                if (seeds <= 3)
                {
                    GameObject choose = Customer[seeds];
                    SpawnCustomer(choose, posZ, shopPosX);

                    if (choose.GetComponent<Customer>().DishType == PlayerObj.GetComponent<PlayerBehaviour>().DishType)
                    {
                        NoMuchCustomerLog = 0;
                    }
                    else
                    {
                        NoMuchCustomerLog++;
                    }
                }
                else
                {
                    NoMuchCustomerLog++;
                }
            }
        }
        else
        {
            int seeds = Random.Range(0, 30);
            if (seeds <= 3)
            {
                GameObject choose = Customer[seeds];
                SpawnCustomer(choose, posZ, shopPosX);
            }
        }

        //Special
        float SpecialSeed = Random.Range(0, 100.0f);
        //Debug.Log((NoSpecialLog * 0.5f - 13.5f).ToString());
        if (SpecialSeed <= NoSpecialLog * 0.45f - 13.5f)
        {
            if(PlayerObj.GetComponent<PlayerBehaviour>().Mask < 2)
            {
                GameObject Spawned = Random.Range(0.0f, 1.2f) < 0.35 * (2 - PlayerObj.GetComponent<PlayerBehaviour>().Mask) ? Mask : Amabie;
                SpawnSpecial(Spawned, posZ + (distance / 2));
            }
            else
            {
                SpawnSpecial(Amabie, posZ + (distance / 2));
            }
            NoSpecialLog = -10;
        }
        else
        {
            NoSpecialLog += (3 - PlayerObj.GetComponent<PlayerBehaviour>().Mask);
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

    private float SpawnShop(GameObject target, float posZ)
    {
        float seed = Random.Range(0.0f, 1.0f) > 0.5f ? 1.0f : -1.0f;
        float diff = Random.Range(0.0f, 1.0f) > 0.5f ? 11.0f : 4.4f;

        GameObject tmp = Instantiate(target, new Vector3(seed * 3.9f, 0.0f, posZ + diff), Quaternion.identity, ObjRoot.transform);
        tmp.transform.localScale = new Vector3(seed, 1.0f, 1.0f);

        return seed;
    }

    private void SpawnCustomer(GameObject target, float posZ, float ShopSeed)
    {
        float seed = ShopSeed * -1;
        float diff = Random.Range(0.0f, 1.0f) > 0.5f ? 7.0f : 13.6f;

        GameObject tmp = Instantiate(target, new Vector3(seed * 4.1f, 0.0f, posZ + diff), Quaternion.identity, ObjRoot.transform);
        tmp.transform.localScale = new Vector3(seed, 1.0f, 1.0f);
    }

    private void SpawnSpecial(GameObject target, float posZ)
    {
        float seed = Random.Range(-1.5f, 1.5f);
        Instantiate(target, new Vector3(seed, 1.8f, posZ), Quaternion.identity, ObjRoot.transform);
    }

    public void GetDish(int id)
    {

    }
}

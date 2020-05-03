using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public enum DISHTYPE
    {
        NONE = 0,
        Sushi = 1,
        Pizza = 2,
        Humberger = 3,
        Ramen = 4
    }

    public DISHTYPE DishType;

    private void Start()
    {

    }
}

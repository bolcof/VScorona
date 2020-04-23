using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public enum DISHTYPE
    {
        NONE = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Yellow = 4
    }

    public DISHTYPE DishType;

    private void Start()
    {

    }
}

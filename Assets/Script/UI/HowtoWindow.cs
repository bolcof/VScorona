using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoWindow : MonoBehaviour
{
    public GameObject StartWindow;

    public void Opened()
    {
    }

    public void pushCancel()
    {
        this.GetComponent<Animator>().SetBool("Open", false);
        StartWindow.GetComponent<Animator>().SetBool("Open", true);
    }

    public void pushNext()
    {

    }
}

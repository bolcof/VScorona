using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public void FadeEnd(){
        this.GetComponent<Animator>().SetBool("Speedy", false);
    }
}

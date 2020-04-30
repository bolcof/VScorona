using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartWindow : MonoBehaviour
{
    private PlayerBehaviour Player;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        this.gameObject.GetComponent<Animator>().SetBool("Open", true);
    }

    public void pushStart()
    {
        Player.isPlaying = true;
        Player.speed = 14.0f;

        Player.PlayTime = 0.0f;
        
        this.gameObject.GetComponent<Animator>().SetBool("Open", false);
    }
}

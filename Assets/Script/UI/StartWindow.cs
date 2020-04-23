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
    }

    public void pushStart()
    {
        Player.isPlaying = true;
        Player.speed = 8.0f;

        //Animationに書き換える
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
        this.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        this.gameObject.GetComponent<CanvasGroup>().interactable = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}

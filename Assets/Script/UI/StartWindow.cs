using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartWindow : MonoBehaviour
{
    private PlayerBehaviour Player;
    public GameObject mainUIwindow;
    public GameObject OptionPanel, HowtoPanel;

    private string appID;

    public AudioSource AS;
    public AudioClip dicide, cancel;
    public AudioClip Mitsu, crap;

    public bool isDebug;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();

        if(PlayerPrefs.GetInt("FirstBoot", 0) == 0)
        {
            this.gameObject.GetComponent<Animator>().SetBool("Open", false);
            HowtoPanel.GetComponent<Animator>().SetBool("Open", true);
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetBool("Open", true);
            HowtoPanel.GetComponent<Animator>().SetBool("Open", false);
        }

        OptionPanel.GetComponent<Animator>().SetBool("Open", false);
        mainUIwindow.GetComponent<Animator>().SetBool("Open", false);

        AS = this.GetComponent<AudioSource>();
    }

    public void pushStart()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            AS.PlayOneShot(dicide);
        }

        Player.isPlaying = true;
        Player.speed = 22.43f;

        Player.PlayTime = 0.0f;
        Player.gameObject.GetComponent<Animator>().SetBool("Run", true);
        Player.gameObject.GetComponent<Animator>().SetFloat("Speed", 1.0f);

        this.gameObject.GetComponent<Animator>().SetBool("Open", false);
        mainUIwindow.GetComponent<Animator>().SetBool("Open", true);

        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            Player.BGM.Play();
        }
    }

    public void pushOption()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            AS.PlayOneShot(dicide);
        }

        this.gameObject.GetComponent<Animator>().SetBool("Open", false);

        OptionPanel.GetComponent<OptionWindow>().Opened();
        OptionPanel.GetComponent<Animator>().SetBool("Open", true);

    }

    public void pushHowto()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            AS.PlayOneShot(dicide);
        }

        this.gameObject.GetComponent<Animator>().SetBool("Open", false);
        
        HowtoPanel.GetComponent<Animator>().SetBool("Open", true);
        HowtoPanel.GetComponent<how2PlayScript>().returnZeroPage();
    }

    public void goResult()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            AS.PlayOneShot(Mitsu);
            AS.clip = crap;
            AS.PlayDelayed(1.6f);
        }
    }

    public void HideAll()
    {
    }
}

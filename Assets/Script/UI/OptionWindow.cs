using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;
using UnityEngine.UI;

public class OptionWindow : MonoBehaviour
{
    public Image MuteButton;
    public Sprite[] MuteImages = new Sprite[2];

    public GameObject StartWindow;

    public void Opened()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            MuteButton.sprite = MuteImages[0];
        }
        else
        {
            MuteButton.sprite = MuteImages[1];
        }
    }

    public void pushCancel()
    {
        this.GetComponent<Animator>().SetBool("Open", false);
        StartWindow.GetComponent<Animator>().SetBool("Open", true);
    }

    public void pushMute()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
            MuteButton.sprite = MuteImages[1];
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
            MuteButton.sprite = MuteImages[0];
            StartWindow.GetComponent<StartWindow>().AS.PlayOneShot(StartWindow.GetComponent<StartWindow>().dicide);
        }
    }

    public void pushNoAds()
    {
    }
}

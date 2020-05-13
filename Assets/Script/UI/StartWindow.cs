using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class StartWindow : MonoBehaviour
{
    private PlayerBehaviour Player;
    public BannerView bannerViewBottom, bannerViewTop;
    public GameObject mainUIwindow;
    public GameObject OptionPanel, HowtoPanel;

    private string appID;

    public AudioSource AS;
    public AudioClip dicide, cancel;
    public AudioClip Mitsu, crap;

    public bool isDebug;

    private void Start()
    {
        if (PlayerPrefs.GetInt("noAds", 0) == 0)
        {
            appID = "ca-app-pub-3940256099942544~3347511713";
#if UNITY_IOS && !UNITY_EDITOR
        appID = "ca-app-pub-7199806318674055~8077951056";
        MobileAds.Initialize(appID);
        RequestBanner();
#elif UNITY_ANDROID && !UNITY_EDITOR
        appID = "ca-app-pub-7199806318674055~8883766267";
        MobileAds.Initialize(appID);
        RequestBanner();
#endif
        }
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

    private void RequestBanner()
    {
        string adUnitId;
        // 広告ユニットID これはテスト用
        adUnitId = "ca-app-pub-3940256099942544/6300978111";
#if UNITY_IOS && !UNITY_EDITOR
        adUnitId = "ca-app-pub-7199806318674055/1512542708";
#elif UNITY_ANDROID && !UNITY_EDITOR
        adUnitId = "ca-app-pub-7199806318674055/7120378010";
#endif
        if (isDebug) { adUnitId = "ca-app-pub-3940256099942544/6300978111"; }
        // Create a 320x50 banner at the top of the screen.
        bannerViewBottom = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerViewBottom.LoadAd(request);
    }

    private void RequestBanner_top()
    {

        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#if UNITY_IOS && !UNITY_EDITOR
        adUnitId = "ca-app-pub-7199806318674055/1512542708";
#elif UNITY_ANDROID && !UNITY_EDITOR
        adUnitId = "ca-app-pub-7199806318674055/7120378010";
#endif
        if (isDebug) { adUnitId = "ca-app-pub-3940256099942544/6300978111"; }
        // Create a 320x50 banner at the top of the screen.
        bannerViewTop = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerViewTop.LoadAd(request);
    }

    public void pushStart()
    {
        if (PlayerPrefs.GetInt("noAds", 0) == 0)
        {
#if !UNITY_EDITOR
        bannerViewBottom.Hide();
        RequestBanner_top();
#endif
        }

        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            AS.PlayOneShot(dicide);
        }

        Player.isPlaying = true;
        Player.speed = 20.03f;

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


        if (PlayerPrefs.GetInt("noAds", 0) == 0)
        {
#if !UNITY_EDITOR
        bannerViewTop.Hide();
        RequestBanner();
#endif
        }
    }

    public void HideAll()
    {
#if !UNITY_EDITOR
        bannerViewTop.Hide();
        bannerViewBottom.Hide();
#endif
    }
}

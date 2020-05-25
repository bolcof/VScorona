using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using EasyMobile;

public class ResultWindow : MonoBehaviour
{
    private GameState Gstate;
    // Start is called before the first frame update
    void Start()
    {
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Restart()
    {
        GameObject.Find("StartPanel").GetComponent<StartWindow>().HideAll();
        SceneManager.LoadScene("Main");
    }


    // Coroutine that captures a screenshot and generates a Texture2D object of it  
    IEnumerator CaptureScreenshot()
    {
        // Wait until the end of frame
        yield return new WaitForEndOfFrame();

        // The SaveScreenshot() method returns the path of the saved image
        // The provided file name will be added a ".png" extension automatically
        // string path = Sharing.SaveScreenshot("screenshot");

        // Share a saved image
        // Suppose we have a "screenshot.png" image stored in the persistentDataPath,
        // we'll construct its path first
        //string path = System.IO.Path.Combine(Application.persistentDataPath, "screenshot.png");

        // Share the image with the path, a sample message and an empty subject
        string tweetText;
        if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            tweetText = ((int)Gstate.EarnedMoney).ToString() + "円獲得! #AmabieEats #StayHome";
        }
        else
        {
            tweetText = "You got $" + (Gstate.EarnedMoney/100).ToString("F2") + "! #AmabieEats #StayHome";
        }
        // Sharing.ShareImage(path, tweetText);

        Debug.Log("Finish EM share");
    }

    public void pushShare()
    {
        StartCoroutine("CaptureScreenshot");
    }
}

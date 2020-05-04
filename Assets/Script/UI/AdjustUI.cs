using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustUI : MonoBehaviour
{
    public RectTransform UIroot;

    // Start is called before the first frame update
    void Start()
    {
        if (true/*iPhoneX*/)
        {
        }
        else
        {

        }

        if(PlayerPrefs.GetInt("noAds", 0) == 0)
        {
            UIroot.offsetMax = new Vector2(0.0f, Screen.height * 235.0f / 1080.0f);
        }
        else
        {
            UIroot.offsetMax = new Vector2(0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

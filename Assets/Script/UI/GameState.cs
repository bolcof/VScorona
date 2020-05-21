using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public int Level = 1;
    public int MissionCleared = 0;
    public float EarnedMoney;

    private int[] LevelUpLib = {4, 7, 10, 15, 9999};
    private int nextLevel = 4;

    private PlayerBehaviour Player;

    public Text ResultMonay, ResultCleared, ResultRank;

    private int scshCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        ResultMonay.text = "収入：0円";
        ResultCleared.text = "配達数：0件";
        ResultRank.text = "補助輪付き";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Screen Shot");
            ScreenCapture.CaptureScreenshot("image_" + scshCount.ToString() +".png");
            scshCount++;
        }
    }

    public void MissionClear()
    {
        MissionCleared++;
        Player.speed += 0.62f;
        if (MissionCleared >= nextLevel)
        {
            nextLevel += LevelUpLib[Level];
            Level++;
        }

        ResultMonay.text = "収入：" + EarnedMoney.ToString("F0") + "円";
        ResultCleared.text = "配達数：" + MissionCleared.ToString() + "件";

        if(EarnedMoney >= 30000)
        {
            ResultRank.text = "†配達神†";
        }
        else if(EarnedMoney >= 25000)
        {
            ResultRank.text = "配達王";
        }
        else if (EarnedMoney >= 20000)
        {
            ResultRank.text = "伝説の運び屋";
        }
        else if (EarnedMoney >= 17500)
        {
            ResultRank.text = "超光速の運び屋";
        }
        else if (EarnedMoney >= 15000)
        {
            ResultRank.text = "音速の運び屋";
        }
        else if (EarnedMoney >= 12500)
        {
            ResultRank.text = "疾風の運び屋";
        }
        else if (EarnedMoney >= 10000)
        {
            ResultRank.text = "第二宇宙速度";
        }
        else if (EarnedMoney >= 8000)
        {
            ResultRank.text = "孤高の配達人";
        }
        else if (EarnedMoney >= 6000)
        {
            ResultRank.text = "必殺配達人";
        }
        else if (EarnedMoney >= 4000)
        {
            ResultRank.text = "エリート配達人";
        }
        else if (EarnedMoney >= 2500)
        {
            ResultRank.text = "配達上級者";
        }
        else if (EarnedMoney >= 1500)
        {
            ResultRank.text = "配達中級者";
        }
        else if (EarnedMoney >= 1)
        {
            ResultRank.text = "配達初心者";
        }
        else
        {
            ResultRank.text = "補助輪付き";
        }

        switch (MissionCleared)
        {
            case 1:
                break;
            case 5:
                break;
            case 10:
                break;
            case 15:
                break;
            case 20:
                break;
            case 25:
                break;
            case 30:
                break;
            case 35:
                break;
            case 40:
                break;
            case 45:
                break;
            case 50:
                break;
            default:
                ResultRank.text = ResultRank.text;
                break;
        }
    }
}

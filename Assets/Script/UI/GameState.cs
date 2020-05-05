using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public int Level = 1;
    public int MissionCleared = 0;
    public float EarnedMoney;

    private int[] LevelUpLib = {6, 13, 20, 9999};
    private int nextLevel = 6;

    private PlayerBehaviour Player;

    public Text ResultMonay, ResultCleared, ResultRank;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        ResultRank.text = "補助輪付き配達人";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MissionClear()
    {
        MissionCleared++;
        Player.speed += 0.8f;
        if (MissionCleared >= nextLevel)
        {
            nextLevel += LevelUpLib[Level];
            Level++;
        }

        ResultMonay.text = "収入：" + EarnedMoney.ToString("F0") + "円";
        ResultCleared.text = "配達数：" + MissionCleared.ToString() + "件";

        switch (MissionCleared)
        {
            case 1:
                ResultRank.text = "配達初心者";
                break;
            case 5:
                ResultRank.text = "配達中級者";
                break;
            case 10:
                ResultRank.text = "配達上級者";
                break;
            case 15:
                ResultRank.text = "エリート配達人";
                break;
            case 20:
                ResultRank.text = "必殺配達人";
                break;
            case 25:
                ResultRank.text = "音速の配達人";
                break;
            case 30:
                ResultRank.text = "競輪選手";
                break;
            case 35:
                ResultRank.text = "疾風迅雷";
                break;
            case 40:
                ResultRank.text = "超光速";
                break;
            case 45:
                ResultRank.text = "伝説の運び屋";
                break;
            case 50:
                ResultRank.text = "配達神";
                break;
            default:
                ResultRank.text = ResultRank.text;
                break;
        }
    }
}

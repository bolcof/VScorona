using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int Level = 1;
    public int MissionCleared = 0;
    public float EarnedMoney;

    private int[] LevelUpLib = {2, 2, 3, 4, 6, 9, 14, 15, 15, 15, 15, 15, 15, 15, 15, 9999};
    private int nextLevel = 2;

    private PlayerBehaviour Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MissionClear()
    {
        MissionCleared++;

        if(MissionCleared >= nextLevel)
        {
            nextLevel += LevelUpLib[Level];
            Level++;
            Player.speed += 0.8f;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int Level; /* enemy-> Lv.0 : 0,0,1,1,1,2,  Lv.1 : 1,1,1,2,2,2  Lv.2 : 1,1,2,2,2,3  Lv.3 : 1,2,2,3,3,3  Lv.4 : 2,2,3,3,3,4 */
    public int MissionCleared;

    // Start is called before the first frame update
    void Start()
    {
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

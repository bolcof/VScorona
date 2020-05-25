using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class how2PlayScript : MonoBehaviour
{
    public RectTransform basePanel;
    public int pages = 0;
    private float nowX = 2250, basePanelX = 2250;
    private float alpha = 1.0f;
    public CanvasGroup how2playCanvas;

    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    
    public Button rightAllow, leftAllow;

    public CanvasGroup parentCG;

    // Start is called before the first frame update
    void Start()
    {
        returnZeroPage();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            if (nowX < basePanelX)
            {
                if (basePanelX - nowX >= 120)
                {
                    nowX += 120;
                }
                else
                {
                    nowX = basePanelX;
                }
            }

            if (nowX > basePanelX)
            {
                if (nowX - basePanelX >= 120)
                {
                    nowX -= 120;
                }
                else
                {
                    nowX = basePanelX;
                }
                if (pages == 5)
                {
                    alpha -= 0.125f;
                    how2playCanvas.alpha = alpha;
                }
            }
            else if (pages == 5)
            {
                pushClose();
                pages = 0;
            }
        }

        basePanel.localPosition = new Vector3(nowX, 0, 0);

        if (parentCG.interactable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                touchStartPos = new Vector3(Input.mousePosition.x,
                                            Input.mousePosition.y,
                                            Input.mousePosition.z);
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                GetAxisHorizontal();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchEndPos = new Vector3(Input.mousePosition.x,
                                          Input.mousePosition.y,
                                          Input.mousePosition.z);
                GetDirection();
            }
        }

        if (pages == 0)
        {
            leftAllow.interactable = false;
        }
        else
        {
            leftAllow.interactable = true;
        }
    }

    public void returnZeroPage() {
        pages = 0;
        nowX = 2250;
        basePanelX = 2250;
        alpha = 1.0f;
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        string Direction = "noTouch";

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        } else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (30 < directionY) {
                //上向きにフリック
                Direction = "up";
            } else if (-30 > directionY) {
                //下向きのフリック
                Direction = "down";
            }
        } else {
            //タッチを検出
            Direction = "touch";
        }

        switch (Direction)
        {
            case "up":
                //上フリックされた時の処理
                break;

            case "down":
                //下フリックされた時の処理
                break;

            case "right":
                if (pages >= 1)
                {
                    pages--;
                    basePanelX = 2250 - (pages * 1125);
                }
                Direction = "noTouch";
                break;

            case "left":
                if (pages <= 4)
                {
                    pages++;
                    basePanelX = 2250 - (pages * 1125);
                }
                Direction = "noTouch";
                break;

            case "touch":
                //タッチされた時の処理
                break;

            case "noTouch":
                break;
            default:
                break;
        }
    }

    void GetAxisHorizontal()
    {
        float directionX = Input.mousePosition.x - touchStartPos.x;
        nowX = basePanelX + (directionX * 1125 / Screen.width * 3 / 4);
    }

    public void pushClose() {
        PlayerPrefs.SetInt("FirstBoot", 1);
        this.GetComponent<Animator>().SetBool("Open", false);
        GameObject.Find("StartPanel").GetComponent<Animator>().SetBool("Open", true);
    }

    public void LeftAllow() {
        if (pages >= 1)
        {
            pages--;
            basePanelX = 2250 - (pages * 1125);
        }
    }

    public void RightAllow()
    {
        if (pages <= 4)
        {
            pages++;
            basePanelX = 2250 - (pages * 1125);
        }
    }
}

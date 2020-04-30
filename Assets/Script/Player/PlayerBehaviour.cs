using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isPlaying;

    public float speed;
    public float PlayTime = 0.0f;
    private Rigidbody PlayerRB;

    public Shop.DISHTYPE DishType;

    public int Mask;
    public bool Resist;

    //テキストではなくす
    public Text maskText, resistText, dishText;
    public Text devText;
    public GameObject ResultPanel;

    float rotY = 0;
    float exPosX;

    public GameState Gstate;

    // Start is called before the first frame update
    void Start()
    {
        Mask = 2;
        Resist = true;
        PlayerRB = this.gameObject.GetComponent<Rigidbody>();
        ResultPanel = GameObject.Find("ResultPanel");
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {
        float posX = 0;

        if (!isPlaying)
        {
            if (speed != 0)
            {
                speed -= Time.deltaTime * 3.6f;
                if (speed < 0.05f) { speed = 0.0f; }
            }
        }
        else
        {
            posX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 2) * 5.4f;

            PlayTime += Time.deltaTime;
            if (PlayTime > 10.0f)
            {
                speed += 3.5f;
                PlayTime = 0.0f;
            }

            if (posX > 5.0f)
            {
                posX = 5.0f;
            }
            else if (posX < -5.0f)
            {
                posX = -5.0f;
            }

            rotY += (posX - exPosX) * 25;

            this.gameObject.transform.position = new Vector3(posX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }

        if (Mathf.Abs(rotY) <= 0.2f){
            rotY = 0;
        }
        else
        {
            rotY *= 0.9f;
        }

        if (rotY >= 15.0f)
        {
            rotY = 15.0f;
        }
        else if(rotY <= -15.0f)
        {
            rotY = -15.0f;
        }

        exPosX = posX;
        if (PlayerRB.velocity.z < speed)
        {
            PlayerRB.AddForce(0.0f, 0.0f, speed * 0.6f);
        }
        this.gameObject.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                if (Resist)
                {
                    Resist = false;
                    resistText.text = "免疫:ナシ";
                }
                else
                {
                    Mask--;
                    if (Mask == -1)
                    {
                        Mask = 0;
                        GameOver();
                        isPlaying = false;
                    }
                    maskText.text = "Mask:" + Mask.ToString();
                }
                break;

            case "Shop":

                if (DishType == Shop.DISHTYPE.NONE)
                {
                    DishType = other.GetComponent<Shop>().DishType;
                    dishText.text = "所持料理：" + DishType.ToString();
                }

                break;

            case "Customer":
                if (DishType == other.GetComponent<Customer>().DishType)
                {
                    devText.text = "届けた！";
                }
                else
                {
                    devText.text = "間違い";
                }

                break;
        }
    }

    private void GameOver()
    {
        ResultPanel.SetActive(true);
        ResultPanel.GetComponent<Animator>().SetBool("Open", true);
    }
}

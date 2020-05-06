﻿using System.Collections;
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
    
    public Text maskText;

    public Image Dish;
    public Sprite[] dishes = new Sprite[5];
    public Image nowDelivering;
    public GameObject Speedy;
    private float DeliveryTime = 0.0f;

    public Text speedText, moneyText;

    public GameObject StartPanel, MainUI, ResultPanel;

    float rotY = 0;
    float exPosX;

    public GameState Gstate;

    public AudioSource AS, BGM;
    public AudioClip[] ShopVoice = new AudioClip[2];
    public AudioClip[] CustomerVoice = new AudioClip[2];
    public AudioClip SpeedyAudio;
    public AudioClip EnemyHit;

    // Start is called before the first frame update
    void Start()
    {
        Mask = 2;
        Resist = false;
        PlayerRB = this.gameObject.GetComponent<Rigidbody>();
        ResultPanel = GameObject.Find("ResultPanel");
        Gstate = GameObject.Find("GameState").GetComponent<GameState>();
        moneyText.text = "収入：0円";
    }

    // Update is called once per frame
    void Update()
    {
        float posX = 0;

        if (!isPlaying)
        {
            BGM.Stop();
            if (speed != 0)
            {
                speed *= 0.9f;
                if (speed < 0.05f) { speed = 0.0f; }
            }
        }
        else
        {
            posX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 2) * 5.4f;

            PlayTime += Time.deltaTime;
            if (PlayTime > 12.0f)
            {
                speed += 2.05f;
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

        if (Mathf.Abs(rotY) <= 0.2f)
        {
            rotY = 0;
        }
        else
        {
            rotY *= 0.9f;
        }

        if (rotY >= 20.0f)
        {
            rotY = 20.0f;
        }
        else if (rotY <= -20.0f)
        {
            rotY = -20.0f;
        }

        exPosX = posX;
        speedText.text = "時速：" + PlayerRB.velocity.z.ToString("F0") + "km/h";
        if (PlayerRB.velocity.z < speed)
        {
            PlayerRB.AddForce(0.0f, 0.0f, speed * 0.6f);
        }
        this.gameObject.transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

        if (DishType != Shop.DISHTYPE.NONE)
        {
            DeliveryTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlaying)
        {
            switch (other.tag)
            {
                case "Enemy":
                    Debug.Log("Enemy");
                    AS.PlayOneShot(EnemyHit);
                    if (!Resist)
                    {
                        Mask--;
                        if (Mask == -1)
                        {
                            Mask = 0;
                            GameOver();
                            isPlaying = false;
                        }
                        maskText.text = Mask.ToString();
                    }
                    break;

                case "Shop":
                    Debug.Log("Shop_" + other.GetComponent<Shop>().DishType);
                    if (DishType == Shop.DISHTYPE.NONE)
                    {
                        AS.PlayOneShot(ShopVoice[Random.Range(0, 2)]);
                        DishType = other.GetComponent<Shop>().DishType;
                        Dish.sprite = dishes[DishType.GetHashCode()];
                        nowDelivering.enabled = true;

                    }
                    break;

                case "Customer":
                    Debug.Log("Customer_" + other.GetComponent<Customer>().DishType);
                    if (DishType == other.GetComponent<Customer>().DishType)
                    {
                        if(DeliveryTime <= 3.0f)
                        {
                            Speedy.GetComponent<Animator>().SetBool("Speedy", true);
                            Speedy.GetComponent<Text>().text = "超速達！";
                            Gstate.EarnedMoney += 200.0f;
                            AS.PlayOneShot(SpeedyAudio);
                        }
                        else if(DeliveryTime <= 6.0f)
                        {
                            Speedy.GetComponent<Animator>().SetBool("Speedy", true);
                            Speedy.GetComponent<Text>().text = "速達！";
                            Gstate.EarnedMoney += 100.0f;
                            AS.PlayOneShot(SpeedyAudio);
                        }
                        DeliveryTime = 0.0f;

                        AS.PlayOneShot(CustomerVoice[Random.Range(0, 2)]);
                        Debug.Log("配達成功");
                        nowDelivering.enabled = false;
                        DishType = Shop.DISHTYPE.NONE;
                        Dish.sprite = dishes[0];

                        Gstate.EarnedMoney += 500.0f;
                        moneyText.text = "収入：" + Gstate.EarnedMoney.ToString("F0") + "円";
                        Gstate.MissionClear();
                    }
                    break;
            }
        }
    }

    public void FadeEnd() {
        Debug.Log("こう?");
        Speedy.GetComponent<Animator>().SetBool("Speedy", false);
    }

    private void GameOver()
    {
        ResultPanel.GetComponent<Animator>().SetBool("Open", true);
        MainUI.GetComponent<Animator>().SetBool("Open", false);
        StartPanel.GetComponent<StartWindow>().goResult();
        this.gameObject.GetComponent<Rigidbody>().drag = 0.5f;
    }
}

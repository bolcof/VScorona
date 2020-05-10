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
    public AudioClip MaskSE, AmabieSE;

    public Animator BarriarAnim;
    private float ResistRemainTime = 0.0f;

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
        float nowTouchX = 0;
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
            nowTouchX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 2) * 5.6f;
            posX = exPosX + ((nowTouchX - exPosX) * 0.4f);

            PlayTime += Time.deltaTime;
            if (PlayTime > 12.0f)
            {
                speed += 2.05f;
                PlayTime = 0.0f;
                this.gameObject.GetComponent<Animator>().SetFloat("Speed", speed * 0.05f);
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

        if (ResistRemainTime > 0.0f)
        {
            ResistRemainTime -= Time.deltaTime;
            if (ResistRemainTime <= 0.0f)
            {
                LostBarriar();
            }
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
                    if (!Resist)
                    {
                        if (PlayerPrefs.GetInt("Mute", 0) == 0)
                        {
                            AS.PlayOneShot(EnemyHit);
                        }
                        Mask--;
                        if (Mask == -1)
                        {
                            Mask = 0;
                            GameOver();
                            isPlaying = false;
                        }
                        else
                        {
                            GetBarriar(1.8f);
                        }
                        maskText.text = Mask.ToString();
                    }
                    break;

                case "Shop":
                    Debug.Log("Shop_" + other.GetComponent<Shop>().DishType);
                    if (DishType == Shop.DISHTYPE.NONE)
                    {
                        if (PlayerPrefs.GetInt("Mute", 0) == 0)
                        {
                            AS.PlayOneShot(ShopVoice[Random.Range(0, 2)]);
                        }
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
                            if (PlayerPrefs.GetInt("Mute", 0) == 0)
                            {
                                AS.PlayOneShot(SpeedyAudio);
                            }
                        }
                        else if(DeliveryTime <= 6.0f)
                        {
                            Speedy.GetComponent<Animator>().SetBool("Speedy", true);
                            Speedy.GetComponent<Text>().text = "速達！";
                            Gstate.EarnedMoney += 100.0f;
                            if (PlayerPrefs.GetInt("Mute", 0) == 0)
                            {
                                AS.PlayOneShot(SpeedyAudio);
                            }
                        }
                        DeliveryTime = 0.0f;

                        if (PlayerPrefs.GetInt("Mute", 0) == 0)
                        {
                            AS.PlayOneShot(CustomerVoice[Random.Range(0, 2)]);
                        }
                        Debug.Log("配達成功");
                        nowDelivering.enabled = false;
                        DishType = Shop.DISHTYPE.NONE;
                        Dish.sprite = dishes[0];

                        Gstate.EarnedMoney += 500.0f;
                        moneyText.text = "収入：" + Gstate.EarnedMoney.ToString("F0") + "円";
                        Gstate.MissionClear();
                    }
                    break;
                case "Mask":
                    Destroy(other.gameObject);
                    Mask++;
                    maskText.text = Mask.ToString();
                    if (PlayerPrefs.GetInt("Mute", 0) == 0)
                    {
                        AS.PlayOneShot(MaskSE);
                    }

                    break;
                case "Amabie":
                    Destroy(other.gameObject);
                    if (PlayerPrefs.GetInt("Mute", 0) == 0)
                    {
                        AS.PlayOneShot(AmabieSE);
                    }
                    GetBarriar(6.3f);
                    break;
            }
        }
    }

    public void FadeEnd() {
        Speedy.GetComponent<Animator>().SetBool("Speedy", false);
    }

    private void GameOver()
    {
        this.GetComponent<Animator>().SetBool("Run", false);
        ResultPanel.GetComponent<Animator>().SetBool("Open", true);
        MainUI.GetComponent<Animator>().SetBool("Open", false);
        StartPanel.GetComponent<StartWindow>().goResult();
        this.gameObject.GetComponent<Rigidbody>().drag = 0.5f;
    }

    private void GetBarriar(float time)
    {
        ResistRemainTime = Mathf.Max(ResistRemainTime, time);
        BarriarAnim.SetBool("Resist", true);
        Resist = true;
    }

    private void LostBarriar()
    {
        BarriarAnim.SetBool("Resist", false);
        Invoke("LostResist", 1.2f);
    }

    private void LostResist()
    {
        if (ResistRemainTime <= 0.0f)
        {
            Resist = false;
        }
    }
}

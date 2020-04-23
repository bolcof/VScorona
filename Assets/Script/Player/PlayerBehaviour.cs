using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isPlaying;

    public float speed;

    public int Mask;
    public bool Resist;

    //テキストではなくす
    public Text maskText, resistText;

    // Start is called before the first frame update
    void Start()
    {
        Mask = 2;
        Resist = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            if (speed != 0)
            {
                speed -= Time.deltaTime*3f;
                if (speed < 0.05f) { speed = 0.0f; }
            }
        }

        float posX = (Input.mousePosition.x - Screen.width / 2) / (Screen.width / 2) * 3.2f;
        if (posX > 2.5f)
        {
            posX = 2.5f;
        }else if (posX < -2.5f)
        {
            posX = -2.5f;
        }
        this.gameObject.transform.position = new Vector3(posX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        this.gameObject.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
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
                    Debug.Log("gameover");
                    isPlaying = false;
                }
                maskText.text = "Mask:" + Mask.ToString();
            }
        }
    }
}

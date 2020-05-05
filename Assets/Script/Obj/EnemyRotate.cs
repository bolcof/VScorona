using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : MonoBehaviour
{
    Vector3 vel;
    float d;
        
    private void Start()
    {
        d = Random.Range(0.0f, 2.0f);
        vel = new Vector3(Random.Range(-60.0f, 60.0f), Random.Range(-60.0f, 60.0f), Random.Range(-60.0f, 60.0f));
    }

    void Update()
    {
        this.transform.Rotate(vel * Time.deltaTime);
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, Mathf.Sin(d * Mathf.PI) * 0.35f, this.transform.localPosition.z);
        d += Time.deltaTime;
    }
}

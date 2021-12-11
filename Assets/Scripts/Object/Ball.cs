using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector2 StartV = new Vector2(0.0f, 0.0f);
    public float ReflectPower = 10.0f;
    private float TimeMax;
    private Rigidbody2D rg;
    private float timeCount = 0.0f;   
    bool timeLock = false;
    private GameObject timeCountTex;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        rg.velocity = StartV;
        timeCount = 0;
        timeCountTex = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!timeLock) timeCount += Time.deltaTime;
        //Debug.Log(timeCount);
        if (timeCount >= TimeMax)
        {
            //Debug.Log("dest");
            Destroy(gameObject);
        }

        //timeCountTex.transform.localScale = ((TimeMax - timeCount)/TimeMax) * Vector3.one;
        transform.localScale = ((TimeMax - timeCount /1.5f) / TimeMax) * Vector3.one ;
    }

    public void SetStartV_And_Power(Vector2 v2, float p, float tamaAge)
    {
        StartV = v2;
        ReflectPower = p;
        TimeMax = tamaAge;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Vector3 dir = transform.position - collision.transform.position;
            rg.velocity = dir * ReflectPower;
            //timeCount = 0.0f;
        }

        
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.transform.tag == "Switch")
    //    {
    //        //timeLock = true;
    //        //Debug.Log("OnSwitch");
    //        //timeCount = 0;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if(collision.transform.tag == "Switch")
    //    {
    //        //timeLock = false;
    //    }
    //}
}

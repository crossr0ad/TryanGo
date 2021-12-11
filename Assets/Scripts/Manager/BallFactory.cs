using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFactory : MonoBehaviour
{
    public GameObject tamaPrfab;
    public Vector2 StartV = new Vector2(0.0f, 0.0f);
    public float ReflectPower = 5.0f;
    public float SwaponTime = 3.0f;
    public float tamaAge = 5.0f;

    private float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

        if(timeCount >= SwaponTime)
        {
            timeCount = 0;
            GameObject go = Instantiate(tamaPrfab);
            go.transform.position = transform.position;
            go.GetComponent<Ball>().SetStartV_And_Power(StartV, ReflectPower,tamaAge);
        }
    }
}

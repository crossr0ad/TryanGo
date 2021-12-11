using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBall : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    
    private float deleteTime=1;

    private Vector3 vector;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = vector;
    }

    public void SetVelocity(Vector3 vector)
    {
        this.vector = vector;
    }
    public void SetDeleteTime(float time)
    {
        deleteTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (deleteTime < timer)
        {
            Destroy(this.gameObject);
        }
    }

}

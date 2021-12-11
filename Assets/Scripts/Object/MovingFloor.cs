using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private float moveTimer;

    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    [SerializeField]
    private Rigidbody2D rigidbody;

    private bool isReturn;
    // Start is called before the first frame update
    void Start()
    {
        isReturn = false;
   
    }

    void Update()
    {
        moveTimer += Time.deltaTime;
    }
   
    void FixedUpdate()
    {

        Vector2 pos = Vector2.Lerp(start.position,end.position, Mathf.Pow( Mathf.Sin(Time.time*moveSpeed),2));


        rigidbody.MovePosition(pos);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    [SerializeField]
    private Vector2 buoyancy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Rigidbody2D rigidbody;
            rigidbody = collision.GetComponent<Rigidbody2D>();
            float dis =  this.transform.position.y -collision.transform.position.y;
            rigidbody.AddForce(buoyancy * dis);
        }
    }

 
}

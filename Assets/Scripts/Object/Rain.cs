using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    [SerializeField]
    private GameObject drop;
    [SerializeField]
    private float right;
    [SerializeField]
    private float left;
    [SerializeField]
    private float spawnTime;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (spawnTime < timer)
        {
            timer = 0;
            GameObject d;
            d = Instantiate(drop, this.transform.position+new Vector3(Random.Range(-left, right), 0,0), this.transform.rotation);
           
        }
    }
}

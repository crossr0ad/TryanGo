using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarterShaderController : MonoBehaviour
{    
    private List<GameObject> playerList = new List<GameObject>();
    private List<Vector4> playerPosList = new List<Vector4>();
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = gameObject.GetComponent<SpriteRenderer>().sharedMaterial;
        
    }

    // Update is called once per frame
    void Update()
    {

        int startPoint = 0;
        int maxCount = 3;
        int tempPoint = playerList.Count - maxCount;
        if (tempPoint > 0) startPoint = tempPoint;
        for (int i = 0; i < playerList.Count; i++)
        {
            //int p = i;
            if (i >= maxCount) continue;
            GameObject go = playerList[i + startPoint];
            Vector3 pos = go.transform.position;
            Vector3 vel = go.GetComponent<Rigidbody2D>().velocity;
            float wavePower = Vector3.Magnitude(vel) * 0.2f;
            float power = playerPosList[i + startPoint].w;
            if (wavePower > 0.01f)
            {
                if (wavePower > 1) wavePower = 1;
                if (power < wavePower) power = wavePower;
                //Debug.Log(power);
            }               
                                  
            power -= Time.deltaTime;
            if (power < 0) power = 0;
            playerPosList[i + startPoint] = new Vector4(pos.x, pos.y, pos.z, power);

            //playerPosList[i]
            string Iname = "_InputPoint" + i.ToString();
            mat.SetVector(Iname, playerPosList[i + startPoint]);
        };
                    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //wavePowerValue = 1.0f;
            if (!playerList.Contains(collision.gameObject))
            {
                playerList.Add(collision.gameObject);
                playerPosList.Add(collision.transform.position);
            }
        }
    }
}

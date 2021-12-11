using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject mSwitch;
    private GameObject mDoar;

    private Vector3 oriPositionD;
    private Vector3 tarPositionD;
    public Vector3 tarDirection = new Vector3(0, -4, 0);
    private float lerpValueD = 0;

    private Vector3 oriPositionS;
    private Vector3 tarPositionS;
    private float lerpValueS = 0;

    private Transform player;

    bool isDoarOpen = false;
    bool isSwitchOn = false;

    void Start()
    {
        mSwitch = transform.GetChild(0).gameObject;
        mDoar = transform.GetChild(1).gameObject;
        //isDoarOpen = false;
        oriPositionD = mDoar.transform.position;
        tarPositionD = mDoar.transform.position + tarDirection;

        oriPositionS = mSwitch.transform.position;
        tarPositionS = mSwitch.transform.position - new Vector3(0, 0.5f, 0);

        player = GameObject.FindWithTag("Player").transform;
    }




    // Update is called once per frame
    void Update()
    {

        if (isDoarOpen)
        {
            mDoar.transform.position = Vector3.Lerp(oriPositionD, tarPositionD, lerpValueD);
            lerpValueD += 0.01f;
            if (lerpValueD > 1.0f) lerpValueD = 1.0f; 
        }
        else
        {
            mDoar.transform.position = Vector3.Lerp(oriPositionD, tarPositionD, lerpValueD);
            lerpValueD -= 0.01f;
            if (lerpValueD < 0.0f) lerpValueD = 0;
        }


        if (isSwitchOn)
        {
            mSwitch.transform.position = Vector3.Lerp(oriPositionS, tarPositionS, lerpValueS);
            lerpValueS += 0.01f;
            if (lerpValueS > 1.0f) lerpValueS = 1.0f;
            if (lerpValueS >= 1) isDoarOpen = true;
            
        }
        else
        {
            mSwitch.transform.position = Vector3.Lerp(oriPositionS, tarPositionS, lerpValueS);
            lerpValueS -= 0.01f;
            if (lerpValueS < 0.0f) lerpValueS = 0;
            if (lerpValueS <= 1) isDoarOpen = false;
        }
        //Debug.Log("On");
       // PlayerCheck();
    }



    //void PlayerCheck()
    //{
    //    Vector3 pos = player.position;
    //    //Vector3.Distance
    //    //float dis = Vector3.Distance(pos ,oriPositionS);
    //    Debug.Log(pos);
    //    //if (Vector3.Vector3.Distance(pos - (oriPositionS)) < 1.0f){
    //    //    isSwitchOn = true;
    //    //    //Debug.Log();
    //    //}
    //    //else
    //    //{
    //    //    Debug.Log("Out");
    //    //    isSwitchOn = false;
    //    //}
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.tag == "Player")
    //    {
    //        isSwitchOn = true;
    //    }
    //    //Debug.Log("On");
    //    if(collision.tag == "Tama")
    //    {
    //        Debug.Log("On");
    //        isSwitchOn = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Tama")
        {
            SoundController.I.PlaySE(SoundData.SE.PlayerSwitch);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isSwitchOn = true;
        }
        //Debug.Log("On");
        if (collision.tag == "Tama")
        {
            
            isSwitchOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isSwitchOn = false;
        }

        if(collision.tag == "Tama")
        {
            isSwitchOn = false;
        }

        //Debug.Log("Out");
    }

   
}

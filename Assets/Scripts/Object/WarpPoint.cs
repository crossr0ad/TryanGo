using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public GameObject nextWarp;
    private Vector3 warpPosition;
    CameraController _cameraController;

    // Start is called before the first frame update
    void Start()
    {
        warpPosition = transform.GetChild(0).position;
        transform.GetChild(0).gameObject.SetActive(false);

        _cameraController = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public Vector3 GetWarpPosition()
    {
        return warpPosition;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundController.I.PlaySE(SoundData.SE.PlayerWarp);
            var targetPostition = nextWarp.GetComponent<WarpPoint>().GetWarpPosition();
            _cameraController.MoveSmoothly(targetPostition);
            collision.transform.position = targetPostition;
        }
    }
}

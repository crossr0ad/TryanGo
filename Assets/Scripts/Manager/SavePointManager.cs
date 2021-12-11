using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    Vector2 _DefaultRespawnPosition;
    SavePoint _Current;

    public Vector2 RespawnPosition
    {
        get
        {
            if (_Current == null)
            {
                return _DefaultRespawnPosition;
            }

            return _Current.transform.position + new Vector3(0, 1, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _DefaultRespawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrent(SavePoint savePoint)
    {
        if (savePoint == _Current)
        {
            return;
        }

        Debug.Log($"Respawn Postion is set to {savePoint.transform.position}");
        if (_Current != null)
        {
            _Current.SetNormalColor();
        }
        _Current = savePoint;
        _Current.SetRespawnColor();

        SoundController.I.PlaySE(SoundData.SE.PlayerRevive);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] Color _NormalColor = Color.yellow;
    [SerializeField] Color _RespawnColor = Color.cyan;
    [SerializeField] ParticleSystem _ParticleSystem;

    private SavePointManager savePointManager;

    void Start()
    {
        savePointManager = GameObject.Find("SavePointManager").GetComponent<SavePointManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(other.GetComponent<Player>()._contorollable)
                savePointManager.SetCurrent(this);
        }
    }

    public void SetNormalColor()
    {
        _ParticleSystem.startColor = _NormalColor;
    }

    public void SetRespawnColor()
    {
        _ParticleSystem.startColor = _RespawnColor;
    }
}

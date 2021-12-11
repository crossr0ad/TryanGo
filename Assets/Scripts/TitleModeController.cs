using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleModeController : MonoBehaviour
{
    [SerializeField]
    float _waitTime = 0.5f;

    float delta;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Title scene loaded");
        delta = 0.0f;

        SoundController.I.PlayBGM(SoundData.BGM.Title);
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > _waitTime && Input.anyKeyDown)
        {
            SoundController.I.PlaySE(SoundData.SE.PlayerSwitch);
            SceneManager.LoadScene("Game");
        }
    }
}

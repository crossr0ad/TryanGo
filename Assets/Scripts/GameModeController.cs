using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeController : MonoBehaviour
{
    [SerializeField]
    float _waitTimeOnFinish = 3.0f;

    CameraController _cameraController;
    ClearCanvas _clearCanvasContoroller;
    Player _player;
    TimeCounter _timeCounter;

    bool cleared;
    float delta = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game scene loaded");
        SoundController.I.PlayBGM(SoundData.BGM.Game);
        cleared = false;

        _cameraController = FindObjectOfType<CameraController>();
        if (_cameraController == null)
        {
            Debug.LogError("Camera object is not found!");
        }

        _clearCanvasContoroller = FindObjectOfType<ClearCanvas>();
        if (_clearCanvasContoroller == null)
        {
            Debug.LogError("Clear Canvas Controller object is not found!");
        }
        _timeCounter = FindObjectOfType<TimeCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        // tentative implementation
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }
        if (cleared)
        {
            delta += Time.deltaTime;
            if (delta > _waitTimeOnFinish && Input.anyKeyDown)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear()
    {
        if (cleared)
        {
            return;
        }
        _cameraController.Finish();
        _clearCanvasContoroller.Finish();
        _timeCounter.Finished = true;

        FindObjectOfType<Player>().Finish();

        cleared = true;
    }
}

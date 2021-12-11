using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoalPoint : MonoBehaviour
{
    GameModeController _gameModeController;

    // Start is called before the first frame updat
    void Start()
    {
        _gameModeController = FindObjectOfType<GameModeController>();
        if (_gameModeController == null)
        {
            Debug.LogError("Game Mode Controller is not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //ゴール処理
            Debug.Log("GoalPoint:goal");
            GameClear();
        }
    }

    void GameClear()
    {
        _gameModeController.GameClear();
    }
}

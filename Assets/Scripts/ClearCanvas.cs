using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearCanvas : MonoBehaviour
{
    // This script should be attached to ClearCanvas

    PlayerFactory _playerFactory;
    GameObject _panel;

    string _clearText(int numDeadBody) => $"You have sacrificed {numDeadBody} players ;)";

    // Start is called before the first frame update
    void Start()
    {
        _panel = GameObject.Find("ClearPanel");
        if (_panel == null)
        {
            Debug.LogError("Clear Panel object is not found!");
        }
        _panel.SetActive(false);

        _playerFactory = FindObjectOfType<PlayerFactory>();
        if (_playerFactory == null)
        {
            Debug.LogError("Player Factory object is not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Finish()
    {
        SoundController.I.PlayBGM(SoundData.BGM.Clear);

        _panel.SetActive(true);
        var textComponent = GameObject.Find("YouHaveSacrificed")?.GetComponent<Text>();
        if (textComponent == null)
        {
            Debug.LogError("Clear Text object is not found!");
        }
        textComponent.text = _clearText(_playerFactory.NumDeadBody);
    }
}

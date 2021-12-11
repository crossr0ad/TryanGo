using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    [SerializeField]
    GameObject _playerPrefab;
    public int NumDeadBody { get; set; }

    //現在操作中のプレイヤー
    GameObject _currentPlayer = null;

    SavePointManager _savePointManager;

    // Start is called before the first frame update
    void Start()
    {
        if (_playerPrefab == null)
        {
            Debug.LogError("Player Prefab is not specified!");
        }
        _savePointManager = FindObjectOfType<SavePointManager>();
        if (_savePointManager == null)
        {
            Debug.LogError("Save Point Manager is not found!");
        }

        //最初に生成されているプレイヤーを検索。時間があれば最初の1体もFactoryから作ってもいい。
        _currentPlayer = GameObject.Find("Player");
        NumDeadBody = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MakePlayer()
    {
        //生産するオブジェクトがなければreturn;
        if (_playerPrefab == null)
        {
            return;
        }

        //ここでリスポーンマネージャーからの座標を受け取って格納
        var newPosition = _savePointManager?.RespawnPosition ?? Vector2.zero;
        //新規プレイヤーを生成
        _currentPlayer = Instantiate(_playerPrefab, newPosition, Quaternion.identity);
        Debug.Log($"Create a new player at {newPosition}");

        NumDeadBody++;
    }

    //現在の操作プレイヤーの座標を取得
    public Vector3 GetCurrentPlayerPosition()
    {
        return _currentPlayer.transform.position;
    }
}

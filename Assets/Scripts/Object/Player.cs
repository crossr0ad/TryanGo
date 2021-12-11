using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //プレイヤーの剛体コンポーネント
    Rigidbody2D _rb2D = null;

    //プレイヤーのトランスフォームコンポーネント
    Transform _playerTransform = null;

    //プレイヤーのスプライトレンダラー
    SpriteRenderer _playerSpriteRenderer = null;

    //ランダムに設定されるプレイヤーの色の配列
    [SerializeField]
    Color[] _playerColors = new Color[]
    {
        new Color(0, 157, 174),
        new Color(113, 223, 231),
        new Color(194, 255, 249),
    };

    //プレイヤーに設定された色
    Color _playerColor = Color.white;

    //Resources内のプレイヤー画像のディレクトリ
    const string EMOTION_DIRECTORY = "PlayerEmotions";

    //プレイヤーの表情の列挙体
    enum PlayerEmotions
    {
        Alive = 0,
        Dead = 1
    };

    //プレイヤーの表情のスプライト
    Sprite[] _playerSprites;

    //プレイヤーのジャンプ力
    [SerializeField]
    float _jumpPower = 500;

    //ジャンプできる角度
    [SerializeField]
    float _jumpResetAngle = 45.0f;

    //プレイヤーが横に移動する時の回転力(角速度)
    [SerializeField]
    float _angularVelocity = 200;


    //ジャンプできるかのフラグ
    bool _canJump = true;

    //死亡したかのフラグ
    public bool _contorollable;

    // Start is called before the first frame update
    void Start()
    {
        //剛体とトランスフォームとスプライトレンダラーのコンポーネントを取得
        _rb2D = GetComponent<Rigidbody2D>();
        _playerTransform = GetComponent<Transform>();
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();

        //スタート時に色をランダムで決定
        int colorIndex = Random.Range(0, _playerColors.Length);
        _playerColor = _playerColors[colorIndex];
        _playerSpriteRenderer.color = _playerColor;

        //プレイヤー画像の読み込み
        _playerSprites = Resources.LoadAll<Sprite>(EMOTION_DIRECTORY);

        //表情を変更
        _playerSpriteRenderer.sprite = _playerSprites[(int)PlayerEmotions.Alive];

        _contorollable = true;

        //自らをカメラに追尾させるようにする
        SubmitPlayerToCamera();
    }

    // Update is called once per frame
    void Update()
    {
        //死んでいたら何もしない
        if (!_contorollable)
        {
            return;
        }

        //移動処理
        Move();

        //ジャンプ処理
        Jump();

        //プレイヤーの身体を残しリスポーンする処理
        Suiside();
    }

    //移動処理
    void Move()
    {
        //入力キーに応じた方向に回転する。
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rb2D.angularVelocity = _angularVelocity;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rb2D.angularVelocity = _angularVelocity * -1.0f;
        }
    }

    //ジャンプ処理
    void Jump()
    {
        //プレイヤー自身の上方向を取得
        Vector2 localUp = _playerTransform.up;

        //ジャンプキーを押し、プレイヤーの上方向が水平よりも高ければジャンプ
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (_canJump)
            {
                SoundController.I.PlaySE(SoundData.SE.PlayerJump);

                _rb2D.AddForce(localUp * _jumpPower);
                _canJump = false;
            }
        }
    }

    //死亡
    void Suiside()
    {
        //Rを押すと能動的にプレイヤーの身体を残しリスポーン
        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
    }

    //死の瞬間の処理
    void Death()
    {
        SoundController.I.PlaySE(SoundData.SE.PlayerRevive);

        //死亡したフラグをオン
        _contorollable = false;

        //死亡用の表情に変更
        GetComponent<SpriteRenderer>().sprite = _playerSprites[(int)PlayerEmotions.Dead];

        //プレイヤーファクトリーを検索
        GameObject playerFactory = GameObject.Find("PlayerFactory");

        //プレイヤーファクトリーにプレイヤーを生産してもらう。
        playerFactory.SendMessage("MakePlayer");
    }

    //プレイヤーをカメラに登録する
    void SubmitPlayerToCamera()
    {
        //カメラを取得
        GameObject camera = GameObject.Find("Main Camera");

        //カメラスクリプトのターゲット変更関数で自らを渡す
        camera.GetComponent<CameraController>().SetTargetObject(gameObject);
    }

    //他オブジェクトとの接触
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //接触した壁の法線を取得する
        Vector3 hitNormal = collision.contacts[0].normal;

        //法線と上方向のベクトルとの内積で角度を計算する
        float hitAngle = Vector3.Dot(Vector3.up, hitNormal);

        //法線の角度が設定された角度より低ければジャンプ可能に
        if (Mathf.Abs(hitAngle) >= Mathf.Cos(_jumpResetAngle))
        {
            _canJump = true;
        }

        if (!_contorollable)
        {
            return;
        }

        //DeadPlayerタグのついているオブジェクトとの衝突なら死亡する。
        if (collision.gameObject.tag == "DeadPlayer")
        {
            Death();
        }
    }

    public void Finish()
    {
        _contorollable = false;
        StartCoroutine(ClearMove());

    }
    private IEnumerator ClearMove()
    {
        Debug.Log("Player's coroutine ClearMove started");
        while (true)
        {
            if (_canJump)
            {
                //SoundController.I.PlaySE(SoundData.SE.PlayerJump);

                _rb2D.AddForce(_jumpPower * Vector2.up);
                _canJump = false;
            }
            yield return null;
        }
    }

    private void OnMouseDown()
    {
        if(!_contorollable)
            Destroy(this.gameObject);
    }
}

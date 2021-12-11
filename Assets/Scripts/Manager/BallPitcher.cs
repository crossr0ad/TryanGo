using UnityEngine;
using UnityEngine.Serialization;

public class BallPitcher : MonoBehaviour
{
    [SerializeField]
    GameObject _ballPrefab;

    [SerializeField]
    Vector2 _startVelocity = 15 * Vector2.left;

    [SerializeField]
    float _interval = 3.0f;

    [SerializeField]
    float _ballLifeTime = 30.0f;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (_interval < timer)
        {
            timer = 0.0f;
            var ball = Instantiate(_ballPrefab, transform.position, transform.rotation);
            ball.GetComponent<PushBall>().SetVelocity(_startVelocity);
            ball.GetComponent<PushBall>().SetDeleteTime(_ballLifeTime);
        }
    }
}
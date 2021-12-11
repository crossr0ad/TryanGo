using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Gradient _Gradient = default;

    [SerializeField]
    private AnimationCurve _Curve = default;

    [SerializeField]
    private float _moveDuration = 0.2f;

    [SerializeField]
    private float _finishMoveDuration = 0.5f;

    [SerializeField]
    float _positionZ = -10.0f;

    [SerializeField]
    float _sizeOnFinish = 50.0f;

    [SerializeField]
    Vector2 _positionOnFinish = Vector2.zero;

    GameObject _playerObject;
    Camera _camera;
    bool _tracking;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _playerObject = GameObject.Find("Player");
        _tracking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerObject == null)
        {
            return;
        }

        if (_tracking)
        {
            transform.position = _playerObject.transform.position + _positionZ * Vector3.forward;
        }

        var rot = _playerObject.transform.localEulerAngles.z;
        var rate = _Curve.Evaluate(rot / 360.0f);
        _camera.backgroundColor = _Gradient.Evaluate(rate);
    }

    public void Finish()
    {
        _tracking = false;
        StartCoroutine(FinishMove());
    }

    private IEnumerator FinishMove()
    {
        var position = transform.position;
        var targetPosition = (Vector3)_positionOnFinish + _positionZ * Vector3.forward;
        Debug.Log($"Camera position will be set from {position} to {targetPosition}");

        var size = _camera.orthographicSize;
        var targetSize = _sizeOnFinish;
        Debug.Log($"Camera size will be set from {size} to {targetSize}");

        var elapsed = 0.0f;
        while (elapsed < _finishMoveDuration)
        {
            transform.position = Vector3.Slerp(position, targetPosition, elapsed / _finishMoveDuration);
            _camera.orthographicSize = Mathf.Lerp(size, targetSize, elapsed / _finishMoveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Coroutine FinishMove stopped");
        yield break;
    }

    public void SetTargetObject(GameObject targetObject)
    {
        _playerObject = targetObject;
        StartCoroutine(SwitchUser(targetObject));
    }

    private IEnumerator SwitchUser(GameObject targetObject)
    {
        var position = transform.position;
        var targetPosition = targetObject.transform.position + _positionZ * Vector3.forward;
        Debug.Log($"Camera target is switching from {position} to {targetPosition}");

        var elapsed = 0.0f;
        while (elapsed < _moveDuration)
        {
            transform.position = Vector3.Slerp(position, targetPosition, elapsed / _moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Coroutine SwitchUser stopped");
        yield break;
    }

    public void MoveSmoothly(Vector2 targetPosition)
    {
        StartCoroutine(_MoveSmoothly(targetPosition));
    }

    private IEnumerator _MoveSmoothly(Vector2 targetPosition)
    {
        var position = transform.position;
        Debug.Log($"Camera position is switching from {position} to {targetPosition}");

        var elapsed = 0.0f;
        while (elapsed < _moveDuration)
        {
            transform.position = Vector3.Slerp(position, targetPosition, elapsed / _moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("CameraScript's coroutine _MoveSmoothly stopped");
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFallDeadPlayer : MonoBehaviour
{
    [SerializeField]
    float _Interval = 0.5f;
    float _TIme = 0.0f;
    [SerializeField]
    GameObject _GameObject;

    int _Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_Count > 50)
            return;

        _TIme += Time.deltaTime;
        if (_TIme >= _Interval)
        {
            _TIme = 0.0f;
            _Count++;
            var obj = Instantiate(_GameObject);
            obj.transform.position = transform.position;
            var angle = obj.transform.eulerAngles;
            angle.z = Random.Range(0.0f, 360.0f);
            obj.transform.eulerAngles = angle;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    string _timeCountText(float time) => $"Time: {(int)time} sec";
    float _elapsed;
    Text _textComponent;

    public bool Finished { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _elapsed = 0.0f;
        _textComponent = GetComponent<Text>();
        Finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Finished)
        {
            _elapsed += Time.deltaTime;
            _textComponent.text = _timeCountText(_elapsed);
        }
    }
}

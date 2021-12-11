using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController _Instance;
    public static SoundController I
    {
        get
        {
            if (_Instance != null)
                return _Instance;

            var soundManager = FindObjectOfType<SoundController>();
            _Instance = soundManager;
            return _Instance;
        }
    }

    int _CurrentIndex = 1;
    int OtherIndex
    {
        get { return _CurrentIndex == 0 ? 1 : 0; }
    }

    [SerializeField] List<AudioSource> _AudioSourceList = new List<AudioSource>();
    [SerializeField] AudioSource _AudioSourceSE;
    [SerializeField] SoundDataBehavior _SoundData;

    float _Timer = 0.0f;
    float _CrossFadeTime = 3.0f;
    bool _IsCrossFade = false;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        var soundManagers = FindObjectsOfType<SoundController>();
        if (soundManagers.Length >= 2)
        {
            Debug.Log("SoundManagerが2つ以上存在したので消しました.");
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_IsCrossFade)
        {
            _Timer += Time.deltaTime;

            var rate = _Timer / _CrossFadeTime;
            _AudioSourceList[_CurrentIndex].volume = rate;
            _AudioSourceList[OtherIndex].volume = 1.0f - rate;

            if (_Timer > _CrossFadeTime)
            {
                _IsCrossFade = false;
                _AudioSourceList[OtherIndex].Stop();
            }
        }
    }

    public void PlaySE(SoundData.SE se)
    {
        var clip = _SoundData.GetSE(se);
        if (clip == null)
            return;
        _AudioSourceSE.PlayOneShot(clip);
    }

    public void PlayBGM(SoundData.BGM bgm, float fadeTime = 0.5f)
    {
        var clip = _SoundData.GetBGM(bgm);
        if (clip == null)
            return;

        _CrossFadeTime = fadeTime;
        _Timer = 0.0f;
        _CurrentIndex = _CurrentIndex == 1 ? 0 : 1;
        _AudioSourceList[_CurrentIndex].loop = true;
        _AudioSourceList[_CurrentIndex].clip = clip;
        _AudioSourceList[_CurrentIndex].Play();
        _IsCrossFade = true;
    }

    [ContextMenu("TestStopBGM")]
    public void StopBGM()
    {
        _AudioSourceList[0].Stop();
        _AudioSourceList[1].Stop();
    }

#if UNITY_EDITOR
    [SerializeField] SoundData.BGM _TestBGM;
    [SerializeField] SoundData.SE _TestSE;

    [ContextMenu("TestPlayBGM")]
    public void TestPlayBGM()
    {
        PlayBGM(_TestBGM);
    }
    [ContextMenu("TestPlaySE")]
    public void TestPlaySE()
    {
        PlaySE(_TestSE);
    }
#endif
}

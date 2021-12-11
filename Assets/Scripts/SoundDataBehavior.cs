using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData
{
    public enum BGM
    {
        None = -1,
        Game = 0,
        Title = 1,
        Clear = 2,
    }

    public enum SE
    {
        None = -1,
        PlayerJump = 0,
        PlayerRevive = 1,
        PlayerSwitch = 2,
        PlayerWarp = 3,
    }
}

public class SoundDataBehavior : MonoBehaviour
{
    [SerializeField] List<AudioClip> _BGMList = new List<AudioClip>();

    [SerializeField] List<AudioClip> _SEList = new List<AudioClip>();


    public AudioClip GetBGM(SoundData.BGM bgm)
    {
        if (bgm == SoundData.BGM.None)
            return null;

        var index = (int)bgm;
        if (index < 0 || _BGMList.Count <= index)
        {
            Debug.LogError("BGMが存在しません." + bgm.ToString());
            return null;
        }

        return _BGMList[index];
    }

    public AudioClip GetSE(SoundData.SE se)
    {
        if (se == SoundData.SE.None)
            return null;

        var index = (int)se;
        if (index < 0 || _SEList.Count <= index)
        {
            Debug.LogError("SEが存在しません." + se.ToString());
            return null;
        }

        return _SEList[index];
    }
}

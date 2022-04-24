using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyControl
{
    MoveLeft,
    MoveRight,
}
public class GameSetting : Singleton<GameSetting>
{
    public int DifficultyValue { get; set; }
    public float MusicVolume { get; set; }
    public float SoundEffectVolume { get; set; }

    private AudioSource bgmusic;

    private void Awake()
    {
        bgmusic = GetComponent<AudioSource>();
        SoundEffectVolume = .6f;
        MusicVolume = .6f;

        MoveLeft = KeyCode.LeftArrow;
        MoveRight = KeyCode.RightArrow;
    }

    public void SetVolumeMusic(float value)
    {
        MusicVolume = value;
        bgmusic.volume = MusicVolume;
    }
    public void SetSoundEffectVolume(float value){
        SoundEffectVolume = value;
        if (Setting.Instance!=null)
        {
            Setting.Instance.gameObject.GetComponent<AudioSource>().volume = SoundEffectVolume;
        } 
    }
    public KeyCode MoveLeft { get; set; }
    public KeyCode MoveRight { get; set; }
}

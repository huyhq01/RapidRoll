using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : Singleton<GameSetting>
{
    public int DifficultyValue { get; set; }
    public float SoundEffectVolume { get; set; }

    private AudioSource bgmusic;

    private void Awake()
    {
        bgmusic = GetComponent<AudioSource>();
    }

    public void SetVolumeMusic(float value){
        bgmusic.volume = value;
    }
}

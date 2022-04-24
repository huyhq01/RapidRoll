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
    public float SoundEffectVolume { get; set; }

    private AudioSource bgmusic;

    private void Awake()
    {
        bgmusic = GetComponent<AudioSource>();
        MoveLeft = KeyCode.LeftArrow;
        MoveRight = KeyCode.RightArrow;
    }

    public void SetVolumeMusic(float value)
    {
        bgmusic.volume = value;
    }
    public KeyCode MoveLeft { get; set; }
    public KeyCode MoveRight { get; set; }
}

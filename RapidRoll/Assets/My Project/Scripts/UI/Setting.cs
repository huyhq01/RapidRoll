using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy, Medium, Hard
}
public class Setting : Singleton<Setting>
{
    [HideInInspector] public string[] difficultyArray = { Difficulty.Easy.ToString(), Difficulty.Medium.ToString(), Difficulty.Hard.ToString() };
    [HideInInspector] public int difficultyValue;
    [HideInInspector] public string currentControlKey;

    [SerializeField] private Text difficultyText;
    [SerializeField] private GameObject bindingKey;
    [SerializeField] private AudioClip soundTest;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;

    private AudioSource soundSource;

    public string NameOfKeyControl { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.volume = GameSetting.Instance.SoundEffectVolume;

        difficultyValue = 1;
        UpdateDifficulty(difficultyValue);

        musicSlider.value = GameSetting.Instance.MusicVolume;
        effectSlider.value = GameSetting.Instance.SoundEffectVolume;
    }

    public void GoNextDiff()
    {
        if (difficultyValue + 1 > difficultyArray.Length - 1)
        {
            return;
        }
        difficultyValue++;
        UpdateDifficulty(difficultyValue);
    }

    public void GoPrevDiff()
    {
        if (difficultyValue == 0)
        {
            return;
        }
        difficultyValue--;
        UpdateDifficulty(difficultyValue);
    }

    private void UpdateDifficulty(int value)
    {
        difficultyText.text = difficultyArray[value];
        GameSetting.Instance.DifficultyValue = value;
    }

    public void SetBindingKey(string controlName)
    {
        bindingKey.SetActive(true);
        currentControlKey = controlName;
    }

    public void BackToMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void TestSound()
    {
        soundSource.PlayOneShot(soundTest);
    }
}

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
    [SerializeField] Text difficultyText;

    // Start is called before the first frame update
    void Start()
    {
        difficultyValue = 1;
        UpdateDifficulty(difficultyValue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoNextDiff()
    {
        if (difficultyValue+1 > difficultyArray.Length-1)
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
}

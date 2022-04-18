using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = UnityEngine.UI.Text;
public class UIGameplay : Singleton<UIGameplay>
{
    [SerializeField] Text scoreText;
    [SerializeField] Text LifeText;

    public void SetScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void SetLife(int lifeLeft)
    {
        LifeText.text = "Lifes: " + lifeLeft;
    }
}

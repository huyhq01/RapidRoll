using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = UnityEngine.UI.Text;
public class UIGameplay : Singleton<UIGameplay>
{
    [SerializeField] Text scoreText, LifeText, countDownText;
    private int time{get;set;}
    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
    }
    void OnStateWait(GameState state)
    {
        if (state == GameState.Wait)
        {
            InvokeCountDown();
        }
    }

    public void InvokeCountDown()
    {
        countDownText.gameObject.SetActive(true);
        time = 3;
        countDownText.text = time.ToString();
        InvokeRepeating(nameof(CountDown), 1, 1);
    }

    void CountDown()
    {
        time--;
        countDownText.text = time.ToString();
        if (time == 0)
        {
            countDownText.gameObject.SetActive(false);
            CancelInvoke(nameof(CountDown));
        }
    }

    public void SetScore(int score)
    {
        scoreText.text = "Score : " + score;
    }

    public void SetLife(int lifeLeft)
    {
        LifeText.text = "Lifes: " + lifeLeft;
    }
}

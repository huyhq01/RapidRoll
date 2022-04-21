using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGameplay : Singleton<UIGameplay>
{
    [SerializeField] Text scoreText, LifeText, countDownText;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Image OnPauseBackground;
    private int time { get; set; }
    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
        OnPauseBackground.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
    void OnStateWait(GameState state)
    {
        if (state == GameState.Wait)
        {
            InvokeCountDown();
        }

        if (state == GameState.Pause)
        {
            PauseMenu.gameObject.SetActive(true);
        }
        else
        {
            PauseMenu.gameObject.SetActive(false);
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
            GameManager.Instance.HandleState(GameState.Start);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGameplay : Singleton<UIGameplay>
{
    [SerializeField] Text scoreText, LifeText, countDownText, totalScoreText;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject LoseMenu;
    [SerializeField] Image TransparentBackground;
    private int time { get; set; }
    private void Awake()
    {
        GameManager.UpdateState += UIGameplayOnStateChange;
        TransparentBackground.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);

    }
    void UIGameplayOnStateChange(GameState state)
    {
        if (state == GameState.Wait)
        {
            InvokeCountDown();
        }

        TransparentBackground.gameObject.SetActive(state == GameState.Pause || state == GameState.Lose);
        PauseMenu.gameObject.SetActive(state == GameState.Pause);
        LoseMenu.gameObject.SetActive(state == GameState.Lose);

        if (state == GameState.Lose)
        {
            totalScoreText.text = "Your score: " + PlayerControl.Instance.score;
        } 
        if (state == GameState.Restart)
        {
            GameManager.UpdateState -= UIGameplayOnStateChange;
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
            GameManager.Instance.HandleState(GameState.Continue);
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

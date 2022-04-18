using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = UnityEngine.UI.Text;

#region Enum

public enum Difficult
{
    Easy, Normal, Hard,
}
public enum Tag
{
    Danger, Platform
}
public enum GameState
{
    Menu, Wait, Start, Pause, Death, Lose,
}
#endregion

public class GameManager : Singleton<GameManager>
{
    GameState state;
    public static event Action<GameState> OnGameStateChanged;

    private int life;

    private UIGameplay _UIGameplay;

    // Start function
    private void Start()
    {
        UpdateState(GameState.Wait);
        _UIGameplay = UIGameplay.Instance;
        life = 3;
        StartCoroutine(nameof(WaitForStart));
    }

    IEnumerator WaitForStart(){
        yield return new WaitForSeconds(3);
        UpdateState(GameState.Start);
    }

    public void UpdateState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Menu:
                break;
            case GameState.Start:
                HandleStart();
                break;
            case GameState.Pause:
                break;
            case GameState.Death:
                HandleDeath();
                break;
            case GameState.Lose:
                break;
        }

        // OnGameStateChanged(newState);
        OnGameStateChanged?.Invoke(newState);
    }

    private void HandleStart()
    {
        _UIGameplay.SetLife(life);
    }
    private void HandleDeath()
    {
        if (life > 1)
        {
            life--;
            _UIGameplay.SetLife(life);
        }

    }

}

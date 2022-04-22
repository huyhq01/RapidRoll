using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Menu,
    Wait,
    Start,
    Pause,
    Death,
    Lose,
}
#endregion

public class GameManager : Singleton<GameManager>
{
    GameState state;
    public static event Action<GameState> UpdateState;

    private int life;

    private UIGameplay _UIGameplay;

    // Start function
    private void Start()
    {
        _UIGameplay = UIGameplay.Instance;
        life = 3;
        HandleState(GameState.Wait);
    }

    public void HandleState(GameState newState)
    {
        state = newState;
        Debug.Log("Current state: " + state);

        switch (state)
        {
            case GameState.Menu:
                break;
            case GameState.Wait:
                HandleWait();
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
        UpdateState?.Invoke(newState);

    }
    private void HandlePause()
    {

    }
    private void HandleWait()
    {
        // find platform in range and put player on one of them (random)
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(Tag.Platform.ToString());
        foreach (GameObject item in platforms)
        {
            if (item.transform.position.y <= 2)
            {
                PlayerControl.Instance.gameObject.transform.position =
                    new Vector2(item.transform.position.x, item.transform.position.y +.5f);
                break;
            }
        }
    }

    private void HandleStart()
    {
        _UIGameplay.SetLife(life);
    }

    private void HandleDeath()
    {
        life--;
        _UIGameplay.SetLife(life);

        // from Dead to lose
        if (life == 0)
        {
            HandleState(GameState.Lose);
        }
    }

    
    public void ResumeGame(){
        HandleState(GameState.Start);
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }
}

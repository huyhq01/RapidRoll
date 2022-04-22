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
    Continue,
    Pause,
    Death,
    Lose,
    Restart,
}
#endregion

public class GameManager : Singleton<GameManager>
{
    GameState state;
    public static event Action<GameState> UpdateState;

    private UIGameplay _UIGameplay;

    // Start function
    private void Start()
    {
        _UIGameplay = UIGameplay.Instance;
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
            case GameState.Continue:
                break;
            case GameState.Pause:
                break;
            case GameState.Death:
                break;
            case GameState.Lose:
                break;
            case GameState.Restart:
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
  
    public void ResumeGame(){
        HandleState(GameState.Continue);
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }
    public void Restart(){
        HandleState(GameState.Restart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

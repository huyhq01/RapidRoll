using System;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Enum
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
        UpdateDifficulty();
        InvokeRepeating(nameof(ChangeDifficulty), 20, 20);
    }

    void ChangeDifficulty()
    {
        if (GameSetting.Instance.DifficultyValue != 2)
        {
            GameSetting.Instance.DifficultyValue++;
            UpdateDifficulty();
        }
        else
        {
            CancelInvoke(nameof(ChangeDifficulty));
        }
    }
    void UpdateDifficulty()
    {
        switch (GameSetting.Instance.DifficultyValue)
        {
            case 0:
                PlayerControl.Instance.speed = 10;
                SpawnManager.Instance.spawnRate = 1.5f;
                UpdatePlatformSpeed(2);
                break;
            case 1:
                PlayerControl.Instance.speed = 12;
                SpawnManager.Instance.spawnRate = 1f;
                UpdatePlatformSpeed(3);
                break;
            case 2:
                PlayerControl.Instance.speed = 15;
                SpawnManager.Instance.spawnRate = 0.5f;
                UpdatePlatformSpeed(5);
                break;
        }
    }

    void UpdatePlatformSpeed(int speed){
        Platform[] platforms = FindObjectsOfType<Platform>();
        foreach (Platform item in platforms)
        {
            item.speed = speed;
        }
    }

    public void HandleState(GameState newState)
    {
        state = newState;

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
                    new Vector2(item.transform.position.x, item.transform.position.y + .3f);
                break;
            }
        }
    }

    public void ResumeGame()
    {
        HandleState(GameState.Continue);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        HandleState(GameState.Restart);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

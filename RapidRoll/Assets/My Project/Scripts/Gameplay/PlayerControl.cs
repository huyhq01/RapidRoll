using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singleton<PlayerControl>
{
    const string HORIZONTAL_INPUT = "Horizontal";

    // PRVATE VARIABLES
    private float horizontalInput;
    public int score { get; private set; }
    private UIGameplay _UIGameplay;
    private bool isAddScore { get; set; }
    private bool isStart { get; set; }
    private bool isPause { get; set; }
    private int life;

    // SERIALIZE FIELD
    [SerializeField] private float speed;

    private void Awake()
    {
        GameManager.UpdateState += PlayerControlOnStateChange;
    }
    void PlayerControlOnStateChange(GameState state)
    {
        isStart = (state == GameState.Continue);
        isPause = (state == GameState.Pause);
        if (state == GameState.Restart)
        {
            GameManager.UpdateState -= PlayerControlOnStateChange;
        }
    }

    private void Start()
    {
        score = 0;
        life = 3;
        _UIGameplay = UIGameplay.Instance;
        _UIGameplay.SetLife(life);
        _UIGameplay.SetScore(score);
        RandomSpawn();
    }

    void RandomSpawn()
    {
        GameObject[] platforms = GameObject.FindGameObjectsWithTag(Tag.Platform.ToString());
        foreach (GameObject item in platforms)
        {
            if (item.transform.position.y <= 2f)
            {
                this.transform.position = new Vector2(item.transform.position.x, item.transform.position.y + .5f);
                break;
            }
        }
    }

    private void Update()
    {
        if (isStart)
        {
            AllowControl();
        }
        else
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            if (Input.GetKeyDown(KeyCode.Escape) && isPause)
                GameManager.Instance.HandleState(GameState.Continue);
        }
        if (isAddScore)
        {
            score++;
            _UIGameplay.SetScore(score);
        }
    }

    void AllowControl()
    {
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        // horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (Input.GetKey(GameSetting.Instance.MoveLeft))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        } else if (Input.GetKey(GameSetting.Instance.MoveRight))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            GameManager.Instance.HandleState(GameState.Pause);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isAddScore = true;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        isAddScore = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tag.Danger.ToString()))
        {
            GameManager.Instance.HandleState(GameState.Death);
            life--;
            _UIGameplay.SetLife(life);
            if (life > 0)
            {
                GameManager.Instance.HandleState(GameState.Wait);
            }
            else
            {
                GameManager.Instance.HandleState(GameState.Lose);

            }

        }
    }

}

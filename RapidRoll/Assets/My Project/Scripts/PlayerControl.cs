using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : Singleton<PlayerControl>
{
    const string HORIZONTAL_INPUT = "Horizontal";

    // PRVATE VARIABLES
    private float horizontalInput;
    private int score;
    private UIGameplay _UIGameplay;
    private bool isAddScore { get; set; }
    private bool isStart { get; set; }
    private bool isPause { get; set; }

    // SERIALIZE FIELD
    [SerializeField] private float speed;

    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
    }
    void OnStateWait(GameState state)
    {
        isStart = (state == GameState.Start);
        isPause = (state == GameState.Pause);
    }

    private void Start()
    {
        score = 0;
        _UIGameplay = UIGameplay.Instance;
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
                GameManager.Instance.HandleState(GameState.Start);
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

        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

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
            GameManager.Instance.HandleState(GameState.Wait);
        }
    }

}

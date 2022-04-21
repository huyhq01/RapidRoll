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
    private bool isWaiting { get; set; }

    // SERIALIZE FIELD
    [SerializeField] private float speed;

    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
    }
    void OnStateWait(GameState state)
    {
        isWaiting = (state == GameState.Wait);
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
                Debug.Log("change position");
                this.transform.position = new Vector2(item.transform.position.x, item.transform.position.y + .6f);
                break;
            }
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (!isWaiting)
        {
            horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
        if (isAddScore)
        {
            score++;
            _UIGameplay.SetScore(score);
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

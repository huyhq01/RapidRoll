using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const string HORIZONTAL_INPUT = "Horizontal";

    // PRVATE VARIABLES
    private float horizontalInput;
    private int score;
    private UIGameplay _UIGameplay;
    private bool isAddScore { get; set; }
    private bool isWaiting;

    // SERIALIZE FIELD
    [SerializeField] private float speed;
    [SerializeField] private bool isScoreCal;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnStateWait;
    }
    void OnStateWait(GameState state)
    {
        isWaiting = (state == GameState.Wait);
        if (state != GameState.Wait)
        {
            GameManager.OnGameStateChanged -= OnStateWait;
        }
    }

    private void Start()
    {
        score = 0;
        _UIGameplay = UIGameplay.Instance;
        _UIGameplay.SetScore(score);
    }
    private void FixedUpdate()
    {
        if (!isWaiting)
        {
            horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        }
    }

    private void Update()
    {
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
            Debug.Log("ooppps");
            GameManager.Instance.UpdateState(GameState.Death);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Platform : MonoBehaviour
{
    private bool isWaiting;
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
    [SerializeField] float speed;
    private void FixedUpdate()
    {
        if (!isWaiting)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.Danger.ToString()))
        {
            SpawnManager.Instance.Deactive(this.gameObject);
        }
    }
}

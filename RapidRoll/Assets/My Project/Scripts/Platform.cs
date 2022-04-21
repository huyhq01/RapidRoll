using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Platform : MonoBehaviour
{
    private bool isStopped { get; set; }
    [SerializeField] float speed;

    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
    }
    void OnStateWait(GameState state)
    {
        isStopped = (state == GameState.Wait || state == GameState.Pause);
    }

    void Update()
    {
        if (!isStopped)
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tag.Danger.ToString()))
        {
            SpawnManager.Instance.Deactive(this.gameObject);
        }
    }
}

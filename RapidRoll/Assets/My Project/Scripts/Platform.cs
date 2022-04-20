using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Platform : MonoBehaviour
{
    private bool isWaiting { get; set; }
    [SerializeField] float speed;

    private void Awake()
    {
        GameManager.UpdateState += OnStateWait;
        // isWaiting = true;
    }
    void OnStateWait(GameState state)
    {
        isWaiting = (state == GameState.Wait);
        Debug.Log("State in platform script: " + state + " and iswaiting = " + isWaiting);
    }

    void Update()
    {
        if (!isWaiting)
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

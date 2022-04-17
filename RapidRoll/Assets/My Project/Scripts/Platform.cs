using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Platform : MonoBehaviour
{
    SpawnManager spawnManager;
    [SerializeField] float speed;

    private void OnEnable()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            spawnManager.Deactive(this.gameObject);
        }
    }
}

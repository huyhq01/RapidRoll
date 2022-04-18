using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(RandomSpawn), .1f);
    }

    void RandomSpawn() {
        transform.position = new Vector2(
            Random.Range(
                SpawnManager.Instance.GetLeftBound(), 
                -SpawnManager.Instance.GetLeftBound()),
            Random.Range(-4f,-1f));
    }
}

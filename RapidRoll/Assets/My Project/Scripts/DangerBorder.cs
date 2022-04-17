using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerBorder : MonoBehaviour
{
    float width;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<BoxCollider2D>().size.x;
        transform.localScale = new Vector2(width, transform.localScale.y);
        transform.position = new Vector2(0, SpawnManager.Instance.GetTopBound());
    }
}

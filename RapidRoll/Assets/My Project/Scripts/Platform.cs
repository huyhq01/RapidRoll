using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Platform : MonoBehaviour
{
    [SerializeField] float speed;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tag.TopBorder.ToString()))
        {
            SpawnManager.Instance.Deactive(this.gameObject);
        }
    }
}

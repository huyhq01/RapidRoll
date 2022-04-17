using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const string HORIZONTAL_INPUT = "Horizontal";
    const string TAG_HORIZONTAL_BOUND = "HorizontalBound";
    [SerializeField] float speed;
    Rigidbody2D rbody;
    float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);   
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    const string HORIZONTAL_INPUT = "Horizontal";
    [SerializeField] float speed;
    Rigidbody2D rbody;
    float horizontalInput;

    [SerializeField] bool isScoreCal;
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
    private void OnCollisionExit2D(Collision2D other)
    {
        Score.Instance.IncreaseScore(true);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        Score.Instance.IncreaseScore(false);    
    }
}

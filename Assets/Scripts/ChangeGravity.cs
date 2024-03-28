using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool lowGravity;
    public bool highGravity;
    public bool neutralGravity;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && lowGravity)
        {
            rb.gravityScale = 1.0f;
        }
        else if (collision.CompareTag("Player") && highGravity)
        {
            rb.gravityScale = 4.0f;
        }
        else if(collision.CompareTag("Player") && neutralGravity)
        {
            rb.gravityScale = 2.0f;
        }
    }
}

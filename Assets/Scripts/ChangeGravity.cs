using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Rendering;
using Unity.VisualScripting;

public class ChangeGravity : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator animator;

    public bool lowGravity;
    public bool highGravity;
    public bool neutralGravity;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && lowGravity)
        {
            rb.gravityScale = 0.75f;
            animator.SetBool("low", true);


        }
        else if (collision.CompareTag("Player") && highGravity)
        {
            rb.gravityScale = 4.0f;
            animator.SetBool("high", true);
        }
        else if (collision.CompareTag("Player") && neutralGravity)
        {
            rb.gravityScale = 2.0f;
            animator.SetBool("none", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            animator.SetBool("low", false);
            animator.SetBool("high", false);
            animator.SetBool("none", false);
        }
    }
}

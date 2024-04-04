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

    public void LowGravity()
    {
        rb.gravityScale = 0.75f;
        animator.SetBool("low", true);

    }

    public void HighGravity()
    {
        rb.gravityScale = 4.0f;
        animator.SetBool("high", true);

    }

    public void NeutralGravity()
    {
        rb.gravityScale = 2.0f;
        animator.SetBool("none", true);

    }

    public void ResetBools()
    {
        animator.SetBool("low", false);
        animator.SetBool("high", false);
        animator.SetBool("none", false);
    }
}

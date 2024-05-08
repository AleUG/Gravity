using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Rendering;

public class ChangeGravity : MonoBehaviour
{
    private Rigidbody2D rb;

    public Animator animator;

    private GravitySystem gravitySystem;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        gravitySystem = FindObjectOfType<GravitySystem>();
    }

    public void LowGravity()
    {
        rb.gravityScale = 0.75f;
        gravitySystem.lowGravity = true;
        animator.SetBool("low", true);
        gravitySystem.highGravity = false;
        gravitySystem.neutralGravity = false;

        animator.SetBool("high", false);
        animator.SetBool("none", false);

    }

    public void HighGravity()
    {
        rb.gravityScale = 4.0f;
        
        gravitySystem.highGravity = true;
        animator.SetBool("high", true);
        gravitySystem.lowGravity = false;
        gravitySystem.neutralGravity = false;

        animator.SetBool("low", false);
        animator.SetBool("none", false);
    }

    public void NeutralGravity()
    {
        rb.gravityScale = 2.0f;
        
        gravitySystem.neutralGravity = true;
        animator.SetBool("none", true);
        gravitySystem.highGravity = false;
        gravitySystem.lowGravity = false;

        animator.SetBool("low", false);
        animator.SetBool("high", false);

    }

    public void ResetBools()
    {
        animator.SetBool("low", false);
        animator.SetBool("high", false);
        animator.SetBool("none", false);
    }
}

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

    private GravitySystem gravitySystem;

    private void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        gravitySystem = FindObjectOfType<GravitySystem>();
    }

    public void LowGravity()
    {
        rb.gravityScale = 0.75f;
        animator.SetBool("low", true);
        gravitySystem.lowGravity = true;

        gravitySystem.highGravity = false;
        gravitySystem.neutralGravity = false;

    }

    public void HighGravity()
    {
        rb.gravityScale = 4.0f;
        animator.SetBool("high", true);
        gravitySystem.highGravity = true;

        gravitySystem.lowGravity = false;
        gravitySystem.neutralGravity = false;  

    }

    public void NeutralGravity()
    {
        rb.gravityScale = 2.0f;
        animator.SetBool("none", true);
        gravitySystem.neutralGravity = true;

        gravitySystem.highGravity = false;
        gravitySystem.lowGravity = false;

    }

    public void ResetBools()
    {
        animator.SetBool("low", false);
        animator.SetBool("high", false);
        animator.SetBool("none", false);
    }
}

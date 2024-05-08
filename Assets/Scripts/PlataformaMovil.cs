using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
public class PlataformaMovil : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.transform.parent = this.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.transform.parent = null;
    }
}
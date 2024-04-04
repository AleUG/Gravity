using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionesParticulas : MonoBehaviour
{
    public ParticleSystem particulas;


    public void CambiarGravedad(float g)
    {
        var main = particulas.main;
        main.gravityModifier = g;
    }
}

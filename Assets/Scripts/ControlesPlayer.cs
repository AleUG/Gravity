using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlesPlayer : MonoBehaviour
{
    //Public
    public bool puedeDisparar, puedeMoverse, puedeSaltar;
    public KeyCode botonDisparo, botonSalto;
    public LayerMask layerPiso;

    public DatosSalto datosSalto;
    public Animator anim;

    public float velocidadMovimiento;
    public AudioClip sonidoSalto, sonidoAterrizaje;
    public LibreriaDeSonidos sonidosPasos;
    //Private

    Rigidbody2D rb2d;
    Disparar disparar;
    float horizontal;
    float gravedad;
    public float tiempoEntrePasos;
    float tiempoUltimoPaso;

    public bool grounded;


    Collider2D col2D;
    PrevenirDispararPiso prevenirDispararPiso;

    bool checkCayendo;
    bool saltando;

    //Empinadas
    public float slideSpeedMultiplier = 2f; // Multiplicador de velocidad de deslizamiento
    public float slopeAngleLimit = 45f; // Límite de ángulo de la pendiente para activar el deslizamiento
    public LayerMask groundMask; // Máscara de capas para detectar suelo

    //Plataformas
    public bool isOnPlatform;
    public Rigidbody2D platformRb;


    // esto es para chequear el daño de caida
    Vector3 posicionAnterior;
    Vector3 direccion;
    int sueloCount;
    Collider2D col;
    Vector2[] groundCheckPos;
    bool prevGround;
    public float velocidadDañoCaida;

    private void Awake()
    {
        groundCheckPos = new Vector2[3];
        rb2d = GetComponent<Rigidbody2D>();
        disparar = GetComponent<Disparar>();
        gravedad = Physics2D.gravity.y;
        prevenirDispararPiso = GetComponentInChildren<PrevenirDispararPiso>();

       col = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        CheckPointSystem.instance.ActualizarUltimaPos(transform.position);
    }

    private void Update()
    {
        CheckSuelo();
        Saltar();
        Moverse();
        Disparar();
        DatosAnimator();
        //Empinada();
    }

    void DatosAnimator()
    {
        anim.SetBool("ground", grounded);
        anim.SetFloat("velocidadX", (rb2d.velocity.x != 0)&&(horizontal !=0) ? 1 : 0);
    }

    void Saltar()
    {
        if (!puedeSaltar) return;


        if(grounded && Input.GetKeyDown(botonSalto))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x,  datosSalto.velocidadSalto);
            SoundFXManager.instance.ReproducirSFX(sonidoSalto);
            StartCoroutine(CheckAterrizaje());
            saltando = true;
            anim.SetTrigger("salto");
        }


        /*if (rb2d.velocity.y < 0)
            rb2d.velocity += new Vector2(0,  gravedad * (datosSalto.multiplicadorCaida - 1) * Time.deltaTime);
        else if (rb2d.velocity.y > 0 && !Input.GetKey(botonSalto))
            rb2d.velocity += new Vector2(0, gravedad * (datosSalto.multiplicadorSaltoBajo - 1) * Time.deltaTime);
        */
    }

    void Disparar()
    {
        if (!puedeDisparar) return;

        if (Input.GetKeyDown(botonDisparo) && disparar.t > disparar.tiempoCadencia && !prevenirDispararPiso.ArmaEnPiso)
        {
            disparar.Shoot();
            anim.Play(AnimacionesPlayer.disparar,1,0);
            rb2d.AddForce(-transform.right * 10, ForceMode2D.Impulse);
        }
    }

    void Moverse()
    {
        if (!puedeMoverse) return;

        if(!saltando && !grounded && !checkCayendo)
        {
            checkCayendo = true;
            StartCoroutine(CheckAterrizaje());
        }

        horizontal = Input.GetAxis("Horizontal") * velocidadMovimiento ;
        SonidosPaso();
    }

    private void FixedUpdate()
    {
        if (isOnPlatform)
        {
            rb2d.velocity = new Vector2(horizontal+platformRb.velocity.x, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(horizontal, rb2d.velocity.y);
        }
    }

    void SonidosPaso()
    {
        if(Time.time > tiempoUltimoPaso + tiempoEntrePasos && horizontal != 0 && grounded)
        {
            SoundFXManager.instance.ReproducirSFX(sonidosPasos);
            tiempoUltimoPaso = Time.time;
        }
    }


    ///////////////////////////////////////

    public void HaySuelo(bool state)
    {
        grounded = state;
    }



    IEnumerator CheckAterrizaje()
    {
        yield return new WaitForSeconds(0.1f);

        while(!grounded)
        {
            yield return null;
        }

        SoundFXManager.instance.ReproducirSFX(sonidoAterrizaje);
        anim.Play(AnimacionesPlayer.aterrizar);
        saltando = false;
        checkCayendo = false;
    }



    void CheckSuelo()
    {
        // obtener la direccion del objeto calculando la posición en el frame anterior y restandole la posicion actual
        direccion = (transform.position - posicionAnterior) / Time.deltaTime;
        posicionAnterior = transform.position;

        // esto es para saber si en el frame anterior estaba o no en el suelo
        prevGround = grounded;

        // acá viene la magia negra
        sueloCount = 0;
        Bounds bounds = col.bounds;

        // abajo Izquierda
        groundCheckPos[0] = new Vector2(bounds.center.x - bounds.extents.x, bounds.center.y - bounds.extents.y);

        // abajo Centro
        groundCheckPos[1] = new Vector2(bounds.center.x, bounds.center.y - bounds.extents.y);

        // abajo Derecha
        groundCheckPos[2] = new Vector2(bounds.center.x + bounds.extents.x, bounds.center.y - bounds.extents.y);

        for (int i = 0; i < 3; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundCheckPos[i], Vector2.down, 0.05f, layerPiso);
            if (hit.collider != null)
                sueloCount++;
        }

        // si alguno de los 3 raycast toca suelo, entonces hay piso
        grounded = sueloCount > 0;
    }









}

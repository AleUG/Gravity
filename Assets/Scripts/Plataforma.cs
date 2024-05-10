using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

public class Plataforma : MonoBehaviour
{
    Collision2D collision;

    public float speed;
    Vector3 targetPos;

    ControlesPlayer playerController;
    Rigidbody2D rb;
    Vector3 moveDirection;

    Rigidbody2D playerRb;

    public GameObject ways;
    public Transform[] wayPoints;
    int pointIndex;
    int pointCount;
    int direction = 1;

    public bool isVertical;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ControlesPlayer>();
        rb = GetComponent<Rigidbody2D>();

        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    void Start()
    {
        pointIndex = 1;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[1].transform.position;
        DirectionCalculate();
    }

    private void FixedUpdate()
    {
        // Aumentar el umbral de tolerancia para el cambio de waypoint
        float threshold = 0.1f; // Ajusta el valor según sea necesario
        if (Vector2.Distance(transform.position, targetPos) < threshold)
        {
            NextPoint();
        }

        rb.velocity = moveDirection * speed;
    }

    void NextPoint()
    {
        transform.position = targetPos;

        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }
        else if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;
        DirectionCalculate();
    }

    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.isOnPlatform = true;
            playerController.platformRb = rb;

            if (isVertical)
            {
                other.transform.parent = this.transform;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController.isOnPlatform = false;

            if (isVertical)
            {
                other.transform.parent = null;
            }
        }
    }
}
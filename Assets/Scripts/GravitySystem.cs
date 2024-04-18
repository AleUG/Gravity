using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    public GameObject player;
    public bool lowGravity, highGravity, neutralGravity;

    public List<GameObject> highPlatformList;
    public List<GameObject> lowPlatformList;

    private void Start()
    {
        // Buscamos objetos con los tags específicos
        GameObject[] highPlatformObjects = GameObject.FindGameObjectsWithTag("highPlatform");
        GameObject[] lowPlatformObjects = GameObject.FindGameObjectsWithTag("lowPlatform");

        // Inicializamos las listas y las llenamos con los objetos encontrados
        highPlatformList = new List<GameObject>(highPlatformObjects);
        lowPlatformList = new List<GameObject>(lowPlatformObjects);

        foreach (GameObject obj in highPlatformList)
        {
            obj.SetActive(false);

        }

        foreach (GameObject obj in lowPlatformList)
        {
            obj.SetActive(false);

        }
    }

    public void LowGravitySystem()
    {
        if (lowPlatformList.Count > 0)
        {
            foreach (GameObject obj in lowPlatformList)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in highPlatformList)
            {
                obj.SetActive(false);

            }
        }

        player.transform.parent = null;
        player.SetActive(true);
    }

    public void HighGravitySystem()
    {
        if (highPlatformList.Count > 0)
        {
            foreach (GameObject obj in highPlatformList)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in lowPlatformList)
            {
                obj.SetActive(false);

            }
        }

        player.transform.parent = null;
        player.SetActive(true);
    }

    public void NeutralGravitySystem()
    {
        foreach (GameObject obj in highPlatformList)
        {
            obj.SetActive(false);

        }

        foreach (GameObject obj in lowPlatformList)
        {
            obj.SetActive(false);

        }

        player.transform.parent = null;
        player.SetActive(true);
    }
}

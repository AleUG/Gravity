using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    AudioSource audioSource;

    private bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReproducirSFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void ReproducirOneShot(AudioClip clip)
    {
        if (!hasPlayed)
        {
            audioSource.PlayOneShot(clip);
            hasPlayed = true;
        }
    }
}

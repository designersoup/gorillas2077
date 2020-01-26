using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class soundEffects : MonoBehaviour
{
    public AudioClip explosion;
    public AudioClip throwBanana;
    public AudioClip playerDestroy;
    public AudioClip select;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosion, 0.7F);
        Debug.Log("Sound Effect Played");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

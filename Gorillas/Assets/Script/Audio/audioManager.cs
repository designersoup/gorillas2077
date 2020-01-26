using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    public sound[] sounds;
    public sound[] explosionFX;
    public bool playSFX;
    public bool playMusic;
    public cameraShake cameraShakeCR;
    private int randEx;
    // Start is called before the first frame update
    void Awake()
    {
        playSFX = true;
        playMusic = true;
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        

        foreach (sound s in explosionFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play (string name)
    {
        if (playSFX == true)
        {
            sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;

            // Won't play a sound that isn't there!
            s.source.Play();
        }
    }

    public void StopMusic()
    {
        sound s = Array.Find(sounds, sound => sound.name == "Music");
          s.source.Pause();
    }

    public void ExplosionSFX(int num)
    {
        if (playSFX == true)
        {
            sound ex = explosionFX[num];
            ex.source.Play();
            StartCoroutine(cameraShakeCR.Shake(0.15f, 0.4f));
            Debug.Log("Play Explosion SFX");
        }
    }
}

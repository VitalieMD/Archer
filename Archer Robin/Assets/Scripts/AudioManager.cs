using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }   
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (Sound i in sounds)
        {
            i.source = gameObject.AddComponent<AudioSource>();
            i.source.clip = i.clip;
            i.source.volume = i.volume;
            i.source.pitch = i.pitch;
            i.source.loop = i.loop;
        }
    }

    public void PlaySound (string name)
    {
        
        foreach( Sound sound in sounds)
        {
            if(sound.name == name)
            {
                sound.source.Play();
            }
        }
    }
}

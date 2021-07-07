using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public SoundScript[] sounds;

    // Use this for initialization
    void Awake()
    {
        foreach(SoundScript s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name)
    {
        SoundScript s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayOneShot(string name)
    {
        SoundScript s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(s.source.clip, s.volume);
    }    

    public void Stop(string name)
    {
        SoundScript s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}

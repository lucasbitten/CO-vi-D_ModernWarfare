using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    [SerializeField] AudioMixerGroup mixer;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);


        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
            sounds[i].source.playOnAwake = sounds[i].playOnAwake;

            if (mixer != null)
            {
                sounds[i].source.outputAudioMixerGroup = mixer;

            }
        }
    }

    public Sound GetCoughingSound()
    {
        List<Sound> coughingSounds = new List<Sound>();

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name.Contains("Coughing"))
            {
                coughingSounds.Add(sounds[i]);
            }
        }
        return coughingSounds[Random.Range(0, coughingSounds.Count)];
    }

    public void Stop()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source.Stop();
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}

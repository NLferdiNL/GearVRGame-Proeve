﻿using UnityEngine;

/// <summary>
/// Holds all the audio and plays the voice and music.
/// </summary>
public class SoundManager : MonoBehaviour
{

    [SerializeField] private  AudioClip[] sfx; // Hold sfx clips.
    public AudioClip[] SfxHolder // allow clip usage for other scripts.
    {
        get { return sfx; }
    }

    [SerializeField] private AudioClip[] music; // Hold the music clips.
    public AudioClip[] MusicHolder // Send music clips to other scripts and return new clips.
    {
        get { return music; }
        set { music = value; }
    }

    public AudioSource MusicPlayer; // Play the music one at a time.

    [SerializeField] private AudioClip[] voice; // Hold tutorial clips.
    public AudioClip[] VoiceHolder // Send tutorial clips to other scripts.
    {
        get { return voice; }
    }

    public AudioSource VoicePlayer; // Play an part of the tutorial when called.
}
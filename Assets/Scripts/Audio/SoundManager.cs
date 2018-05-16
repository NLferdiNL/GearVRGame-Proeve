using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerManager;

    [SerializeField] private  List<AudioSource> sfx;
    public List<AudioSource> SfxSender
    {
        get { return sfx; }
    }

    [SerializeField] private List<AudioSource> music;

    public List<AudioSource> MusicSender
    {
        get { return music; }
        
    }

    void SetFloat()
    {
        
        MixerManager.SetFloat("Music", 10);
    }

    void Start()
    {
        music[0].Play();
        music[0].loop = true;
        SetFloat();
    }
}
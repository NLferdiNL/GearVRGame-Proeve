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
    [SerializeField]private AudioSource music;

    public AudioSource MusicSender
    {
        get { return music; }
        set { music = value; }
    }

}
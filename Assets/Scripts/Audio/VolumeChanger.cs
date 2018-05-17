using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    public AudioMixer masterMixer;

    private int currentMusicVol = 0;
    private int currentSfxVol = 0;

    [SerializeField]
    private string mixerMusicVolName;

    [SerializeField]
    private string mixerSfxVolName;

    private void Awake()
    {
        //currentMusicVol = masterMixer.GetFloat(mixerMusicVolName, out int currentMusicVol);
        //currentSfxVol = masterMixer.GetFloat(mixerMusicVolName, currentSfxVol);
    }

    // Music
    public void AddMusicVol(int musicVolToAdd)
    {
        masterMixer.SetFloat(mixerMusicVolName, currentMusicVol += musicVolToAdd);
    }
    public void RemoveMusicVol(int musicVolToRemove)
    {
        masterMixer.SetFloat(mixerMusicVolName, currentMusicVol -= musicVolToRemove);
    }
    
    // Sfx
    public void AddSfxVol(int sfxVolToAdd)
    {
        masterMixer.SetFloat(mixerSfxVolName, currentSfxVol += sfxVolToAdd);
    }
    public void RemoveSfxVol(int sfxVolToRemove)
    {
        masterMixer.SetFloat(mixerSfxVolName, currentSfxVol -= sfxVolToRemove);
    }
}

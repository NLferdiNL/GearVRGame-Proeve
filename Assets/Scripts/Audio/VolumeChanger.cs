using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    public AudioMixer masterMixer;

    [SerializeField]
    private AudioVolumeHolder audioVolumeHolder;

    public float currentMusicVol = 0;
    public float currentSfxVol = 0;

    [SerializeField]
    private string mixerMusicVolName;

    [SerializeField]
    private string mixerSfxVolName;

    private void Awake()
    {
        audioVolumeHolder = GetComponent<AudioVolumeHolder>();
        currentMusicVol = audioVolumeHolder.MusicVolume;
        currentSfxVol = audioVolumeHolder.SfxVolume;
    }

    private void UpdateVolume()
    {
        masterMixer.GetFloat(mixerMusicVolName, out currentMusicVol);
        masterMixer.GetFloat(mixerSfxVolName, out currentSfxVol);

        audioVolumeHolder.MusicVolume = currentMusicVol;
        audioVolumeHolder.SfxVolume = currentSfxVol;
    }

    // Music
    public void AddMusicVol(int musicVolToAdd)
    {
        if (currentMusicVol < 20)
        {
            masterMixer.SetFloat(mixerMusicVolName, currentMusicVol += musicVolToAdd);
            UpdateVolume();
        }
    }
    public void RemoveMusicVol(int musicVolToRemove)
    {
        if (currentMusicVol > -80)
        {
            masterMixer.SetFloat(mixerMusicVolName, currentMusicVol -= musicVolToRemove);
            UpdateVolume();
        }
    }

    // Sfx
    public void AddSfxVol(int sfxVolToAdd)
    {
        if (currentMusicVol < 20)
        {
            masterMixer.SetFloat(mixerSfxVolName, currentSfxVol += sfxVolToAdd);
            UpdateVolume();
        }
    }
    public void RemoveSfxVol(int sfxVolToRemove)
    {
        if (currentMusicVol > -80)
        {
            masterMixer.SetFloat(mixerSfxVolName, currentSfxVol -= sfxVolToRemove);
            UpdateVolume();
        }
    }
}

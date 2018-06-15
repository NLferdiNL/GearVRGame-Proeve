using UnityEngine;
using UnityEngine.Audio;

public class VolumeChanger : MonoBehaviour
{
    public AudioMixer masterMixer;

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
        currentSfxVol = audioVolumeHolder.SFXVolume;
    }

    private void UpdateVolume()
    {
        masterMixer.GetFloat(mixerMusicVolName, out currentMusicVol);
        masterMixer.GetFloat(mixerSfxVolName, out currentSfxVol);

        audioVolumeHolder.MusicVolume = currentMusicVol;
        audioVolumeHolder.SFXVolume = currentSfxVol;
    }

	#region Music
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

	#endregion

	#region SFX
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
	#endregion
}

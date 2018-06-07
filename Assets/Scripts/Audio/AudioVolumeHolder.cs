using UnityEngine;
using UnityEngine.Events;

public class AudioVolumeHolder : MonoBehaviour
{
    private float musicVolume;
    private float sfxVolume;

	[System.Serializable]
	public class VolumeChangeEvent : UnityEvent<float> { }

	public VolumeChangeEvent OnSFXChange = new VolumeChangeEvent();
	public VolumeChangeEvent OnMusicChange = new VolumeChangeEvent();

	public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
			OnMusicChange.Invoke(musicVolume);
        }
    }

    public float SfxVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            sfxVolume = value;
			OnSFXChange.Invoke(sfxVolume);
		}
	}
}

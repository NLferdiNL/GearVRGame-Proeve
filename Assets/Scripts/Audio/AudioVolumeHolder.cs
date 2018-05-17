using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeHolder : MonoBehaviour
{
    private float musicVolume;
    private float sfxVolume;

    
    public float MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            musicVolume = value;
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
        }
    }
}

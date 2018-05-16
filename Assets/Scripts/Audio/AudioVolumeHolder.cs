using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeHolder : MonoBehaviour
{
    private static int musicVolume;
    private static int sfxVolume;

    public int MusicVolume
    {
        get
        {
            return musicVolume;
        }
        set
        {
            value = musicVolume;
        }
    }

    public int SfxVolume
    {
        get
        {
            return sfxVolume;
        }
        set
        {
            value = sfxVolume;
        }
    }
}

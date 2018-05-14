using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private List<AudioSource> sfx;

    public List<AudioSource> Sfx
    {
        get { return sfx; }
    }
    private List<AudioSource> music;

    public List<AudioSource> Music
    {
        get { return music; }
    }
}

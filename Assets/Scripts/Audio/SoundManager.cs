using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private  AudioClip[] sfx;
    public AudioClip[] SfxHolder
    {
        get { return sfx; }
    }

    //public UnityEvent SetSfx = new UnityEvent();

    [SerializeField] private AudioClip[] music;
    public AudioClip[] MusicHolder
    {
        get { return music; }
        set { music = value; }
    }

    public AudioSource MusicPlayer;

}
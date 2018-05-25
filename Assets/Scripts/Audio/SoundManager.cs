using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private  AudioClip[] sfx;
    public AudioClip[] SfxSender
    {
        get { return sfx; }
    }
    [SerializeField] private AudioClip[] music;

    public AudioClip[] MusicHolder
    {
        get { return music; }
        set { music = value; }
    }

    public AudioSource musicPlayer;

}
using UnityEngine;
using UnityEngine.Events;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private  AudioClip[] sfx; // Hold sfx clips.
    public AudioClip[] SfxHolder // allow clip usage for other scripts.
    {
        get { return sfx; }
    }

    //public UnityEvent SetSfx = new UnityEvent();

    [SerializeField] private AudioClip[] music; // Hold the music clips.
    public AudioClip[] MusicHolder // Send music clips to other scripts.
    {
        get { return music; }
        set { music = value; }
    }

    public AudioSource MusicPlayer; // Play the music one at a time.

}
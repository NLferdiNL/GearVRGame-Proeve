using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private static List<AudioSource> sfx;

    [SerializeField] public static List<AudioSource> Sfx
    {
        get { return sfx; }
    }

    [SerializeField] private static List<AudioSource> music;

    [SerializeField] public static List<AudioSource> Music
    {
        get { return music; }
    }
}
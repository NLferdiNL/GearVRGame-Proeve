using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerManager;
    [SerializeField] private SoundManager SM;

    private bool nextBuilding;

    //[0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

    void Start()
    {
        SM.MusicSender[0].Play(); // Fade1
        //Waitforseconds or till this one is done
        Debug.Log(SM.MusicSender[0].clip.name);
        Debug.Log(SM.MusicSender[1].clip.name);
        Debug.Log(SM.MusicSender[2].clip.name);
        Debug.Log(SM.MusicSender[3].clip.name);
        Debug.Log(SM.MusicSender[4].clip.name);
        Debug.Log(SM.MusicSender[5].clip.name);
        Debug.Log(SM.MusicSender[6].clip.name);
        Debug.Log(SM.MusicSender[7].clip.name);
        Debug.Log(SM.MusicSender[8].clip.name);
        Debug.Log(SM.MusicSender[9].clip.name);
        Debug.Log(SM.MusicSender[10].clip.name);
        Debug.Log(SM.MusicSender[11].clip.name);
        Debug.Log(SM.MusicSender[12].clip.name);
        Debug.Log(SM.MusicSender[13].clip.name);
        Debug.Log(SM.MusicSender[14].clip.name);
        Debug.Log(SM.MusicSender[15].clip.name);
        Debug.Log(SM.MusicSender[16].clip.name);
        Debug.Log(SM.MusicSender[17].clip.name);
    }

    void FirstBuilding()
    {
        SM.MusicSender[1].Play(); // Fade2
        StartCoroutine(BuildingOne());
        if (!nextBuilding)// Check unity event
        {
            BroadcastMessage("EndGame");
        }
    }
    void SecondBuilding()
    {
        SM.MusicSender[2].Play(); // FadeVocals
        StartCoroutine(BuildingTwo());
        if (!nextBuilding)// Check unity event
        {
            BroadcastMessage("EndGame");
        }
    }
    void ThirdBuilding()
    {
        SM.MusicSender[4].Play(); // Buildup into
        SM.MusicSender[5].Play(); // Drop
        StartCoroutine(BuildingThree());
        if (!nextBuilding)// Check unity event
        {
            BroadcastMessage("EndGame");
        }
    }

    IEnumerator BuildingOne()
    {
        // Delegate
        yield return null;
    }
    IEnumerator BuildingTwo()
    {
        // Delegate
        yield return null;
    }
    IEnumerator BuildingThree()
    {
        // Delegate
        yield return null;
    }

    void MidSectionOne()
    {
        SM.MusicSender[6].Play(); // Mid1
        StartCoroutine(Mid1());
    }
    void MidSectionTwo()
    {
        SM.MusicSender[7].Play(); // Mid2
        StartCoroutine(Mid2());
    }
    void MidSectionThree()
    {
        SM.MusicSender[8].Play(); // Mid3
        StartCoroutine(Mid3());
    }

    void StartSurvivalMode()
    {
        SM.MusicSender[9].Play(); // MidBuild
        SM.MusicSender[10].Play(); // MidBuildVocals
        SM.MusicSender[11].Play(); // MidBuild2
        SM.MusicSender[12].Play(); // MidbuildPlus
        SM.MusicSender[13].Play(); // MidBuildPlus2
        SM.MusicSender[14].Play(); // Drop
    }

    IEnumerator SurvivalStart()
    {
        SM.MusicSender[9].Play(); // MidBuild
        if (!SM.MusicSender[9].isPlaying)
        {
            SM.MusicSender[10].Play(); // MidBuildVocals
            if (!SM.MusicSender[10].isPlaying)
            {
                SM.MusicSender[11].Play(); // MidBuild2
                if (!SM.MusicSender[11].isPlaying)
                {
                    SM.MusicSender[12].Play(); // MidbuildPlus
                    if (!SM.MusicSender[12].isPlaying)
                    {
                        SM.MusicSender[13].Play(); // MidBuildPlus2
                        if (!SM.MusicSender[13].isPlaying)
                        {
                            SM.MusicSender[14].Play(); // Drop
                            if (!SM.MusicSender[14].isPlaying)
                            {
                                StartEndlessSurvival();
                                yield return null;
                            }
                        }
                    }
                }
            }
        }
        StartCoroutine(SurvivalStart());
    }

    void StartEndlessSurvival()
    {
        SM.MusicSender[15].Play(); // EndLoop
        SM.MusicSender[16].Play(); // EndLoop2
        SM.MusicSender[17].Play(); // EndLoopRust
    }

    IEnumerator Mid1()
    {
        if (!SM.MusicSender[6].isPlaying == false)
        {
            SM.MusicSender[6].Play();
            StartCoroutine(Mid1());
        }
        if (nextBuilding)
        {
            MidSectionTwo();
            yield return null;
        }
        else
        {
            StartCoroutine(Mid1());
        }
    }
    IEnumerator Mid2()
    {
        if (!SM.MusicSender[7].isPlaying == false)
        {
            SM.MusicSender[7].Play();
            StartCoroutine(Mid2());
        }
        if (nextBuilding)
        {
            MidSectionThree();
            yield return null;
        }
        else
        {
            StartCoroutine(Mid2());
        }
    }
    IEnumerator Mid3()
    {
        if (!SM.MusicSender[8].isPlaying == false)
        {
            SM.MusicSender[8].Play();
            StartCoroutine(Mid3());
        }
        if (nextBuilding)
        {
            StartSurvivalMode();
            yield return null;
        }
        else
        {
            StartCoroutine(Mid3());
        }
    }
}

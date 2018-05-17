using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerManager;
    [SerializeField] private SoundManager SM;

    public bool Building1;
    public bool Building2;
    public bool Building3;
    public bool BuildingMid1;
    public bool BuildingMid2;
    public bool BuildingMid3;
    public bool BuildingStartEnd;
    public bool BuildingEndless;
    //[0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

    void Start()
    {
        //SM.MusicSender[0].Play(); // Fade1
        FirstBuilding();
    }
    void FirstBuilding()
    {
        StartCoroutine(BuildingOne());
    }
    void SecondBuilding()
    {
        StartCoroutine(BuildingTwo());
    }
    void ThirdBuilding()
    {
        StartCoroutine(BuildingThree());
    }

    void BetweenThirdAndMid()
    {
        StartCoroutine(InBetween());
    }

    IEnumerator BuildingOne()
    {
        SM.MusicSender[1].Play(); // Fade2
        yield return new WaitForSeconds(SM.MusicSender[1].clip.length);
        if (Building1)
        {
            SecondBuilding();
            yield break;
        } // Fade2
        else
        {
            Debug.Log("YaFuckedIt");
            yield return null;
            
        }
        // Delegate
    }
    IEnumerator BuildingTwo()
    {
        SM.MusicSender[2].Play(); // FadeVocals
        yield return new WaitForSeconds(SM.MusicSender[2].clip.length);
        if (Building2)
        {
            ThirdBuilding();
            yield break;
        }
        else
        {
            Debug.Log("YaFuckedIt");
            yield return null;
        }
        // Delegate
    }
    IEnumerator BuildingThree()
    {
        SM.MusicSender[3].Play(); // Buildup into
        yield return new WaitForSeconds(SM.MusicSender[4].clip.length);
        if (Building3)
        {
            BetweenThirdAndMid();
            yield break;
        }
        else
        {
            Debug.Log("YaFuckedIt");
            yield return null;
        }
        // Delegate
    }

    IEnumerator InBetween()
    {
        SM.MusicSender[4].Play();
        yield return new WaitForSeconds(SM.MusicSender[4].clip.length);
        SM.MusicSender[5].Play();
        yield return new WaitForSeconds(SM.MusicSender[5].clip.length);
        MidSectionOne();
    }

    void MidSectionOne()
    {
        StartCoroutine(Mid1());
    }
    void MidSectionTwo()
    {
        StartCoroutine(Mid2());
    }
    void MidSectionThree()
    {
        StartCoroutine(Mid3());
    }

    void StartSurvivalMode()
    {
        StartCoroutine(SurvivalStart());
    }

    IEnumerator SurvivalStart()
    {
        SM.MusicSender[9].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[9].clip.length);
        SM.MusicSender[10].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[10].clip.length);
        SM.MusicSender[11].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[11].clip.length);
        SM.MusicSender[12].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[12].clip.length);
        SM.MusicSender[13].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[13].clip.length);
        SM.MusicSender[14].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[14].clip.length);
        StartEndlessSurvival();
    }

    IEnumerator SurvivalEndless()
    {
        SM.MusicSender[15].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[15].clip.length);
        SM.MusicSender[16].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[16].clip.length);
        SM.MusicSender[17].Play(); // 
        yield return new WaitForSeconds(SM.MusicSender[17].clip.length);
        StartCoroutine(SurvivalEndless());
    }

    void StartEndlessSurvival()
    {
        StartCoroutine(SurvivalEndless());
    }

    IEnumerator Mid1()
    {
        SM.MusicSender[6].Play(); // Mid1
        yield return new WaitForSeconds(SM.MusicSender[6].clip.length);
        if (BuildingMid1)
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
        SM.MusicSender[7].Play(); // Mid2
        yield return new WaitForSeconds(SM.MusicSender[7].clip.length);
        if (BuildingMid2)
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
        SM.MusicSender[8].Play(); // Mid3
        yield return new WaitForSeconds(SM.MusicSender[8].clip.length);
        if (BuildingMid3)
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

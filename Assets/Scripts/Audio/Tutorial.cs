using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private SoundManager SM;

    public bool TutorialBuilding1, TutorialBuilding2, TutorialBuilding3;

    //[0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

    void Start()
    {
        IntroSection();
    }

    public void CheckBuildingCharge(int BuildingNumber)
    {
        switch (BuildingNumber)
        {
            case 1:
                TutorialBuilding1 = true;
                Debug.Log("TB1");
                break;
            case 2:
                TutorialBuilding2 = true;
                Debug.Log("TB2");
                break;
            case 3:
                TutorialBuilding3 = true;
                Debug.Log("TB3");
                break;
        }
    }
    
    void IntroSection()
    {
        StartCoroutine(IntroStart());
    }
    IEnumerator IntroStart()
    {
        SM.musicPlayer.clip = SM.MusicHolder[0];
        SM.musicPlayer.PlayOneShot(SM.musicPlayer.clip, 1f); // FadeInto
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        if (TutorialBuilding1 == false)
        {
            BuildingNotCharged();
        }
        SM.musicPlayer.clip = SM.MusicHolder[1];
        SM.musicPlayer.PlayOneShot(SM.musicPlayer.clip, 1f); // FadeInto2
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        if (TutorialBuilding2 == false)
        {
            BuildingNotCharged();
        }
        SM.musicPlayer.clip = SM.MusicHolder[2];
        SM.musicPlayer.PlayOneShot(SM.musicPlayer.clip, 1f); // FadeIntoVocals
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        if (TutorialBuilding3 == false)
        {
            BuildingNotCharged();
        }
        InBetweenSection();
        TutorialBuilding1 = TutorialBuilding2 = TutorialBuilding3 = false;
    }

    void BuildingNotCharged()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void InBetweenSection()
    {
        StartCoroutine(InBetween());
    }
    IEnumerator InBetween()
    {
        SM.musicPlayer.clip = SM.MusicHolder[3];
        SM.musicPlayer.Play(); // BuildupInto
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[4];
        SM.musicPlayer.Play(); // Drop
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        MidSection();
    }

    void MidSection()
    {
        if (TutorialBuilding1 == false)
        {
            StartCoroutine(MidPartOne());
        }
        else if (TutorialBuilding1 && !TutorialBuilding2)
        {
            StartCoroutine(MidPartTwo());
        }
        else if (TutorialBuilding1 && TutorialBuilding2 && !TutorialBuilding3)
        {
            StartCoroutine(MidPartThree());
        }
        else if (TutorialBuilding1 && TutorialBuilding2 && TutorialBuilding3)
        {
            StartSurvivalMode();
        }
    }
    IEnumerator MidPartOne()
    {
        SM.musicPlayer.clip = SM.MusicHolder[5];
        SM.musicPlayer.Play(); // Mid1
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        MidSection();
    }
    IEnumerator MidPartTwo()
    {
        SM.musicPlayer.clip = SM.MusicHolder[6];
        SM.musicPlayer.Play(); // Mid2
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        MidSection();
    }
    IEnumerator MidPartThree()
    {
        SM.musicPlayer.clip = SM.MusicHolder[7];
        SM.musicPlayer.Play(); // Mid3
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        MidSection();
    }

    void StartSurvivalMode()
    {
        StartCoroutine(SurvivalStart());
    }
    IEnumerator SurvivalStart()
    {
        SM.musicPlayer.clip = SM.MusicHolder[8];
        SM.musicPlayer.Play(); // MidBuild
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[9];
        SM.musicPlayer.Play(); // MidVocalsBuild
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[10];
        SM.musicPlayer.Play(); // Midbuild2
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[11];
        SM.musicPlayer.Play(); // MidBuildPlus
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[12];
        SM.musicPlayer.Play(); // MidBuildPlus2
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[13];
        SM.musicPlayer.Play(); // Drop2
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        StartEndlessSurvival();
    }

    void StartEndlessSurvival()
    {
        StartCoroutine(SurvivalEndless());
    }
    IEnumerator SurvivalEndless()
    {
        SM.musicPlayer.clip = SM.MusicHolder[14];
        SM.musicPlayer.Play(); // EndLoop
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[15];
        SM.musicPlayer.Play(); // EndLoop2
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[16];
        SM.musicPlayer.Play(); // EndLoopRust
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        SM.musicPlayer.clip = SM.MusicHolder[17];
        SM.musicPlayer.Play(); // EndLoopOutro
        yield return new WaitForSeconds(SM.musicPlayer.clip.length);
        StartEndlessSurvival();
    }
}

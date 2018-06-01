using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private SoundManager SM;

    public UnityEvent OnReset = new UnityEvent();

    public bool TutorialBuilding1, TutorialBuilding2, TutorialBuilding3; // Check of 

    // [0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

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
                break;
            case 2:
                TutorialBuilding2 = true;
                break;
            case 3:
                TutorialBuilding3 = true;
                break;
        }
    }
    
    void IntroSection()
    {
        StartCoroutine(IntroStart());
    }
    IEnumerator IntroStart()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[0];
        SM.MusicPlayer.PlayOneShot(SM.MusicPlayer.clip, 1f); // FadeInto
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        /*if (TutorialBuilding1 == false)
        {
            BuildingNotCharged();
        }*/
        SM.MusicPlayer.clip = SM.MusicHolder[1];
        SM.MusicPlayer.PlayOneShot(SM.MusicPlayer.clip, 1f); // FadeInto2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        /*if (TutorialBuilding2 == false)
        {
            BuildingNotCharged();
        }*/
        SM.MusicPlayer.clip = SM.MusicHolder[2];
        SM.MusicPlayer.PlayOneShot(SM.MusicPlayer.clip, 1f); // FadeIntoVocals
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        /*if (TutorialBuilding3 == false)
        {
            BuildingNotCharged();
        }*/
        TutorialBuilding1 = TutorialBuilding2 = TutorialBuilding3 = false;
        OnReset.Invoke();

        InBetweenSection();
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
        SM.MusicPlayer.clip = SM.MusicHolder[3];
        SM.MusicPlayer.Play(); // BuildupInto
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[4];
        SM.MusicPlayer.Play(); // Drop
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
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
        SM.MusicPlayer.clip = SM.MusicHolder[5];
        SM.MusicPlayer.Play(); // Mid1
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        MidSection();
    }
    IEnumerator MidPartTwo()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[6];
        SM.MusicPlayer.Play(); // Mid2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        MidSection();
    }
    IEnumerator MidPartThree()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[7];
        SM.MusicPlayer.Play(); // Mid3
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        MidSection();
    }

    void StartSurvivalMode()
    {
        StartCoroutine(SurvivalStart());
    }
    IEnumerator SurvivalStart()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[8];
        SM.MusicPlayer.Play(); // MidBuild
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[9];
        SM.MusicPlayer.Play(); // MidVocalsBuild
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[10];
        SM.MusicPlayer.Play(); // Midbuild2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[11];
        SM.MusicPlayer.Play(); // MidBuildPlus
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[12];
        SM.MusicPlayer.Play(); // MidBuildPlus2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[13];
        SM.MusicPlayer.Play(); // Drop2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        StartEndlessSurvival();
    }

    void StartEndlessSurvival()
    {
        StartCoroutine(SurvivalEndless());
    }
    IEnumerator SurvivalEndless()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[14];
        SM.MusicPlayer.Play(); // EndLoop
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[15];
        SM.MusicPlayer.Play(); // EndLoop2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[16];
        SM.MusicPlayer.Play(); // EndLoopRust
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        SM.MusicPlayer.clip = SM.MusicHolder[17];
        SM.MusicPlayer.Play(); // EndLoopOutro
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        StartEndlessSurvival();
    }
}

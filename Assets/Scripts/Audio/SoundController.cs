using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SoundController : MonoBehaviour {

    [SerializeField] private SoundManager SM; // Hold the SoundManager.

    public UnityEvent OnReset = new UnityEvent(); // Reset the buildings from true to false and start second section.
    public UnityEvent NumberChange = new UnityEvent(); // Change in number notification.
    public UnityEvent StartEndless = new UnityEvent();
    public static SoundController Instance;

    public bool TutorialBuilding1, TutorialBuilding2, TutorialBuilding3; // Buildings checks.

    // [0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

    void Awake()
    {
		Instance = this;
    }

    private void Start()
    {
        IntroSection(); // Start the first section of the game.
    }

    public void CheckBuildingCharge(int BuildingNumber) // Set buildings to true if charged.
    {
        switch (BuildingNumber)
        {
            case 1:
                TutorialBuilding1 = true; // Sets the First building to true.
                break;
            case 2:
                TutorialBuilding2 = true; // Sets the Second building to  true.
                break;
            case 3:
                TutorialBuilding3 = true; // Sets the Third building to true.
                break;
        }
    }

    void IntroSection()
    {
        StartCoroutine(IntroStart()); // Start Ienumerator intro.
    }
    IEnumerator IntroStart() // 
    {
        SM.MusicPlayer.clip = SM.MusicHolder[0]; // Set audiosource clip from music holder.
        SM.MusicPlayer.PlayOneShot(SM.MusicPlayer.clip, 1f); // FadeInto clip.
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length); // Wait until clip is done then continue.
        NumberChange.Invoke(); // Invoke Number change for (Blank)
        /*if (TutorialBuilding1 == false)
        {
            BuildingNotCharged();
        }*/ // If the building is not charged game over screen.
        SM.MusicPlayer.clip = SM.MusicHolder[1];
        SM.MusicPlayer.PlayOneShot(SM.MusicPlayer.clip, 1f); // FadeInto2 clip.
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        /*if (TutorialBuilding2 == false)
        {
            BuildingNotCharged();
        }*/
        SM.MusicPlayer.clip = SM.MusicHolder[2];
        SM.MusicPlayer.PlayOneShot(SM.MusicPlayer.clip, 1f); // FadeIntoVocals clip.
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        /*if (TutorialBuilding3 == false)
        {
            BuildingNotCharged();
        }*/
        TutorialBuilding1 = TutorialBuilding2 = TutorialBuilding3 = false; // Set buildings to false for the second phase.
        OnReset.Invoke(); // Invoke event for buildings.

        InBetweenSection(); // Start the inbetween section of the game.
    }

    void BuildingNotCharged()
    {
        SceneManager.LoadScene("MainMenu"); // Sends player to game over screen.
    }

    void InBetweenSection()
    {
        StartCoroutine(InBetween()); // Start Ienumerator of in between section.
    }
    IEnumerator InBetween()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[3];
        SM.MusicPlayer.Play(); // BuildupInto clip.
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[4];
        SM.MusicPlayer.Play(); // Drop clip.
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        MidSection(); // Start the mid section of the game.
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
        NumberChange.Invoke();
        MidSection();
    }
    IEnumerator MidPartTwo()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[6];
        SM.MusicPlayer.Play(); // Mid2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        MidSection();
    }
    IEnumerator MidPartThree()
    {
        SM.MusicPlayer.clip = SM.MusicHolder[7];
        SM.MusicPlayer.Play(); // Mid3
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        MidSection();
    }

    void StartSurvivalMode()
    {
        StartCoroutine(SurvivalStart());
    }
    IEnumerator SurvivalStart()
    {
        StartEndless.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[8];
        SM.MusicPlayer.Play(); // MidBuild
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[9];
        SM.MusicPlayer.Play(); // MidVocalsBuild
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[10];
        SM.MusicPlayer.Play(); // Midbuild2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[11];
        SM.MusicPlayer.Play(); // MidBuildPlus
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[12];
        SM.MusicPlayer.Play(); // MidBuildPlus2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[13];
        SM.MusicPlayer.Play(); // Drop2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
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
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[15];
        SM.MusicPlayer.Play(); // EndLoop2
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[16];
        SM.MusicPlayer.Play(); // EndLoopRust
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        SM.MusicPlayer.clip = SM.MusicHolder[17];
        SM.MusicPlayer.Play(); // EndLoopOutro
        yield return new WaitForSeconds(SM.MusicPlayer.clip.length);
        NumberChange.Invoke();
        StartEndlessSurvival();
    }
}

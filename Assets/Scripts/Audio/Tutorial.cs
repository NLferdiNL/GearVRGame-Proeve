using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private AudioMixer MixerManager;
    [SerializeField] private SoundManager SM;

    //[0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

    void Start()
    {
        //SM.MusicSender[0].Play(); // Fade1
        IntroSection();
        //Test.clip = Resources.Load<AudioSource>("FadeInto").clip;
        //SM.MusicSender.clip = Resources.Load<AudioSource>("FadeInto").clip;
        //SM.MusicSender.PlayOneShot(SM.MusicClip, 1f);
    }

    void IntroSection()
    {
        StartCoroutine(IntroStart(""));
    }

    void InBetweenSection()
    {
        StartCoroutine(InBetween());
    }

    void MidSection()
    {
        StartCoroutine(MidPart());
    }

    void StartSurvivalMode()
    {
        StartCoroutine(SurvivalStart());
    }

    void StartEndlessSurvival()
    {
        StartCoroutine(SurvivalEndless());
    }

    IEnumerator IntroStart(string buildingOne)
    {
        SM.MusicSender.clip = Resources.Load<AudioSource>("FadeInto").clip;
        SM.MusicSender.PlayOneShot(SM.MusicSender.clip, 1f); // FadeInto
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/FadeInto2");
        SM.MusicSender.PlayOneShot(SM.MusicSender.clip, 1f); // FadeInto2
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/FadeIntoVocals");
        SM.MusicSender.PlayOneShot(SM.MusicSender.clip, 1f); // FadeIntoVocals
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        InBetweenSection();
    }

    IEnumerator InBetween()
    {
        SM.MusicSender = Resources.Load<AudioSource>("Music/BuildupInto");
        SM.MusicSender.Play(); // BuildupInto
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/Drop");
        SM.MusicSender.Play(); // Drop
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        MidSection();
    }

    IEnumerator MidPart()
    {
        SM.MusicSender = Resources.Load<AudioSource>("Music/Mid1");
        SM.MusicSender.Play(); // Mid1
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/Mid2");
        SM.MusicSender.Play(); // Mid2
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/Mid3");
        SM.MusicSender.Play(); // Mid3
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        StartSurvivalMode();
    }

    IEnumerator SurvivalStart()
    {
        SM.MusicSender = Resources.Load<AudioSource>("Music/MidBuild");
        SM.MusicSender.Play(); // MidBuild
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/MidVocalsBuild");
        SM.MusicSender.Play(); // MidVocalsBuild
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/MidBuild2");
        SM.MusicSender.Play(); // Midbuild2
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/MidBuildPlus");
        SM.MusicSender.Play(); // MidBuildPlus
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/MidBuildPlus2");
        SM.MusicSender.Play(); // MidBuildPlus2
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/Drop2");
        SM.MusicSender.Play(); // Drop2
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        StartEndlessSurvival();
    }

    IEnumerator SurvivalEndless()
    {
        SM.MusicSender = Resources.Load<AudioSource>("Music/EndLoop");
        SM.MusicSender.Play(); // EndLoop
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/EndLoop2");
        SM.MusicSender.Play(); // EndLoop2
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/EndLoopRust");
        SM.MusicSender.Play(); // EndLoopRust
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        SM.MusicSender = Resources.Load<AudioSource>("Music/EndLoopOutro");
        SM.MusicSender.Play(); // EndLoopOutro
        yield return new WaitForSeconds(SM.MusicSender.clip.length);
        StartCoroutine(SurvivalEndless());
    }
}

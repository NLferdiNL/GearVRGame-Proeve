using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// Controls the Music and Voice of the game.
/// </summary>
public class SoundController : MonoBehaviour
{

	[SerializeField] private SoundManager SM; // Hold the SoundManager.
    [SerializeField] private EnterGameFreeze EGF; // Hold the EnterGameFreeze.

	public UnityEvent OnReset = new UnityEvent(); // Reset the buildings from true to false and start second section.
	public UnityEvent NumberChange = new UnityEvent(); // Change in number notification.
	public UnityEvent StartEndless = new UnityEvent(); // Start of endless section.

	public static SoundController Instance;

	public bool[] buildingsCharged = new bool[3] { false, false, false }; // bool checks for if buildings are charged.

	bool survivalRunning; // Check if survivalRunning.

	// [0] = Start song. || [4] = First drop. || [5] = Start mid section. || [13] = Second drop. || [15] = Start end loop.

	void Awake()
    {
		Instance = this; // Instance this script.
        StartCoroutine(FreezeGame(12, 0));
    }

    IEnumerator FreezeGame(int freeze, int voiceNumber)
    {
        yield return new WaitForSeconds(.01f);
        SM.MusicPlayer.PlayDelayed(freeze);
        EGF.timeToFreeze = freeze;
        EGF.PauseGame();
        SM.VoicePlayer.clip = SM.VoiceHolder[voiceNumber];
        SM.VoicePlayer.Play();
        yield return SM.VoicePlayer;
    }

    private void Start()
    {
		StartCoroutine(IntroStart()); // Start Ienumerator intro.
	}

	public void CheckBuildingCharge(int buildingNumber) // Set charged buildings to true.
	{
		buildingsCharged[buildingNumber - 1] = true;
	}

	IEnumerator IntroStart() {
		for(int i = 0; i < buildingsCharged.Length; i++) // For each buildingCharged do below.
        {
			buildingsCharged[i] = false; // Set charged buildings to false.
		}

		for(int i = 0; i < 3; i++) {
			yield return PlayAndAwait(i);
		    if (!buildingsCharged[0])
		        BuildingNotCharged();
		    if (i == 1)
		    {
                StartCoroutine(FreezeGame(7, 1));
            }
		}

		yield return PlayRange(0, 3); // Play clips from MusicHolder 0 to 3.
		
		OnReset.Invoke(); // Invoke event for buildings.

		InBetweenSection(); // Start the inbetween section of the game.
	}

	IEnumerator BuildingNotCharged()
    {
		yield return new WaitForSeconds(2); // Wait a moment so the player is not instantly sent back.
		SceneManager.LoadScene("MainMenu"); // Send player to game over screen.
	}

	void InBetweenSection()
    {
		StartCoroutine(InBetween()); // Start Ienumerator of in between section.
	}
	IEnumerator InBetween()
    {
		yield return PlayRange(3, 5); // Play clips from MusicHolder 3 to 5.
        yield return StartCoroutine(FreezeGame(10, 2));
        MidSection(); // Start the mid section of the game.
	}

	void MidSection()
	{
		if(!buildingsCharged[0] && !buildingsCharged[1] && !buildingsCharged[2]) // If all buildings are not charged.
        {
			StartCoroutine(MidPartOne()); // Play first section.
		} else if(buildingsCharged[0] && !buildingsCharged[1] && !buildingsCharged[2]) // If building 1 is true and 2 & 3 are false.
        {
			StartCoroutine(MidPartTwo()); // Play second section.
		} else if(buildingsCharged[0] && buildingsCharged[1] && !buildingsCharged[2]) // If building 1 & 2 are true and 3 is false.
        {
			StartCoroutine(MidPartThree()); // Play third section.
		} else if(buildingsCharged[0] && buildingsCharged[1] && buildingsCharged[2]) // If all buildings are charged.
        {
			StartCoroutine(SurvivalStart()); // Start the survival section.
		}
	}
	IEnumerator MidPartOne()
    {
		yield return PlayAndAwait(5); // Play clip 5 from MusicHolder.
        NumberChange.Invoke(); // Call a change in number.
		MidSection(); // Return to midsection.
	}
	IEnumerator MidPartTwo()
    {
		yield return PlayAndAwait(6);
        NumberChange.Invoke();
        MidSection();
    }
	IEnumerator MidPartThree()
    {
		yield return PlayAndAwait(7);
        NumberChange.Invoke();
        MidSection();
    }
		
	IEnumerator SurvivalStart()
    {
        yield return StartCoroutine(FreezeGame(7, 3));
		StartEndless.Invoke(); // Invoke start of endless survival.
		yield return PlayRange(8, 14); // Play clips from MusicHolder 8 to 14.
		StartCoroutine(SurvivalEndless()); // Start endless survival section.
	}

    IEnumerator SurvivalEndless()
    {
        survivalRunning = true; // Sets survivalRunning to true

        while (survivalRunning) // While survivalRunning is true keep repeating the last section.
        {
            yield return PlayRange(14, 18);
        }
    }

    IEnumerator PlayRange(int min, int max) // Play clip from min index to max index.
    {
        for (int i = min; i < max; i++)
        {
            yield return PlayAndAwait(i); // Play and wait till done with clip.
            NumberChange.Invoke(); // Invoke a number change event.
        }
    }

    IEnumerator PlayAndAwait(int index)
    {
		SM.MusicPlayer.clip = SM.MusicHolder[index]; // Set clip Musicholder to musicplayer.
		SM.MusicPlayer.Play(); // Play the clip.
		yield return AwaitMusic(); // Wait for clip to finish.
	}

    IEnumerator AwaitMusic()
    {
        yield return new WaitForSeconds(.2f); // Wait 0.2f for clip.
        yield return new WaitUntil(() => !SM.MusicPlayer.isPlaying); // Wait until current clip is done playing.
    }
}

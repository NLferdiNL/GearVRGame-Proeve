using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
/// <summary>
/// Controls the Music and Voice audio of the game, and calls the spawning of enemies.
/// </summary>
public class SoundController : MonoBehaviour
{

	[SerializeField] private SoundManager sM; // Hold the SoundManager.
    [SerializeField] private GameFreeze gF; // Hold the EnterGameFreeze.

	public UnityEvent OnReset = new UnityEvent(); // Reset the buildings from true to false and start second section.
	public UnityEvent NumberChange = new UnityEvent(); // Change in number notification.
	public UnityEvent StartEndless = new UnityEvent(); // Start of endless section.

	public static SoundController Instance; // Instance the script.

    [SerializeField] private GameObject aICompanion; // Gameobject that holds Hex.

	public bool[] buildingsCharged = new bool[3] { false, false, false }; // bool checks for if buildings are charged.

	bool survivalRunning; // Check if survivalRunning.

	// Location sections of music clips in SoundManager [0] = Start song. || [4] = First drop. || [5] = Start mid section. || [13] = Second drop. || [15] = Start end loop.

    /// <summary>
    /// Instance this script and call the FreezeGame.
    /// </summary>
	void Awake()
    {
		Instance = this;
    }
    /// <summary>
    /// Pause the game, play the tutorial and continue.
    /// </summary>
    /// <param name="freeze"></param>
    /// <param name="voiceNumber"></param>
    /// <returns></returns>
    IEnumerator FreezeGame(int freeze, int voiceNumber)
    {
        aICompanion.SetActive(true);
        yield return null;

        Time.timeScale = 0;

        sM.MusicPlayer.PlayDelayed(freeze);
        sM.VoicePlayer.clip = sM.VoiceHolder[voiceNumber];
        sM.VoicePlayer.Play();

        yield return null;

        yield return new WaitUntil(() => !sM.VoicePlayer.isPlaying);

        float time = 0.1f;

        while (time < 1)
        {
            time *= 1.25f;

            Time.timeScale = time;
            yield return null;
        };

        Time.timeScale = 1;

        aICompanion.SetActive(false);
    }
    /// <summary>
    /// Start tutorial and afterwards the corountine.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
		yield return FreezeGame(12, 0);

		StartCoroutine(IntroStart());
	}
    /// <summary>
    /// Set charged buildings to true.
    /// </summary>
    /// <param name="buildingNumber"></param>
	public void CheckBuildingCharge(int buildingNumber)
	{
		buildingsCharged[buildingNumber - 1] = true;
	}
    /// <summary>
    /// Check if buildings 1 to 3 are charged, set and play song and spawn enemies depending on the part in the song.
    /// </summary>
    /// <returns>Return PlayAndAwait with clip</returns>
	IEnumerator IntroStart() {
		for(int i = 0; i < buildingsCharged.Length; i++) // For each buildingCharged do below.
        {
			buildingsCharged[i] = false; // Set chargable buildings to false.
		}

		for(int i = 0; i < 3; i++) {
			yield return PlayAndAwait(i);
		    if (!buildingsCharged[i])
		        BuildingNotCharged();
		    if (i == 1)
		    {
                SwarmSpawner.SpawnEnemy();
                StartCoroutine(FreezeGame(7, 1));
            }else if (i == 2)
		    {
		        SwarmSpawner.SpawnEnemy(); // Spawn a random enemies in the game.
            } else if(i == 3)
		    {
		        SwarmSpawner.SpawnEnemies(2); // Spawn 2 random enemies in the game.
            }
		}

		yield return PlayRange(0, 3); // Play clips from MusicHolder 0 to 3.
		
		OnReset.Invoke(); // Invoke event for buildings.

		InBetweenSection(); // Start the inbetween section of the game.
	}
    /// <summary>
    /// Building was not charged in time player is returned to menu.
    /// </summary>
    /// <returns></returns>
	IEnumerator BuildingNotCharged()
    {
		yield return new WaitForSeconds(2); // Wait a moment so the player is not instantly sent back.
		SceneManager.LoadScene("MainMenu"); // Send player to game over screen.
	}
    /// <summary>
    /// Call courontine of the section in between the intro and mid section.
    /// </summary>
	void InBetweenSection()
    {
		StartCoroutine(InBetween()); // Start Ienumerator of in between section.
	}
    /// <summary>
    /// Play the 2 clips in between the other sections, spawn enemies, freeze for a tutorial point and go to midsection.
    /// </summary>
    /// <returns></returns>
	IEnumerator InBetween()
    {
		yield return PlayRange(3, 5); // Play clips from MusicHolder 3 to 5.
        SwarmSpawner.SpawnEnemies(3); // Spawn 3 random enemies in the game.
        yield return StartCoroutine(FreezeGame(10, 2));
        MidSection(); // Start the mid section of the game.
	}
    /// <summary>
    /// Midsection of the music with checks if the buildings are charged. 
    /// Does a new clip when appropriate building is charged. 
    /// After all 3 are charged then start survival section.
    /// </summary>
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
    /// <summary>
    /// First part of the midsection.
    /// </summary>
    /// <returns></returns>
	IEnumerator MidPartOne()
    {
        SwarmSpawner.SpawnEnemies(2); // Spawn 2 random enemies in the game.
        yield return PlayAndAwait(5); // Play clip 5 from MusicHolder.
        NumberChange.Invoke(); // Call a change in number.
		MidSection(); // Return to midsection.
	}
    /// <summary>
    /// Second part of the midsection.
    /// </summary>
    /// <returns></returns>
	IEnumerator MidPartTwo()
    {
        SwarmSpawner.SpawnEnemies(2);
        yield return PlayAndAwait(6);
        NumberChange.Invoke();
        MidSection();
    }
    /// <summary>
    /// Third part of the midsection.
    /// </summary>
    /// <returns></returns>
	IEnumerator MidPartThree()
    {
        SwarmSpawner.SpawnEnemies(3);
        yield return PlayAndAwait(7);
        NumberChange.Invoke();
        MidSection();
    }
	/// <summary>
    /// Start the survival section of the game run through it once
    /// then continue onwards to the endless survival section.
    /// </summary>
    /// <returns></returns>
	IEnumerator SurvivalStart()
    {
        yield return StartCoroutine(FreezeGame(7, 3));
		StartEndless.Invoke(); // Invoke start of endless survival.
		yield return PlayRange(8, 14); // Play clips from MusicHolder 8 to 14.
		StartCoroutine(SurvivalEndless()); // Start endless survival section.
	}
    /// <summary>
    /// Sets survivalRunning to true and a while to keep the survival section looping.
    /// </summary>
    /// <returns></returns>
    IEnumerator SurvivalEndless()
    {
        survivalRunning = true;

        while (survivalRunning)
        {
            yield return PlayRange(14, 18);
        }
    }
    /// <summary>
    /// Play clip from min index to max index.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    IEnumerator PlayRange(int min, int max)
    {
        for (int i = min; i < max; i++)
        {
            yield return PlayAndAwait(i);
            NumberChange.Invoke();
        }
    }
    /// <summary>
    /// Play the music clip and wait.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator PlayAndAwait(int index)
    {
		sM.MusicPlayer.clip = sM.MusicHolder[index]; // Set clip Musicholder to musicplayer.
		sM.MusicPlayer.Play(); // Play the clip.
		yield return AwaitMusic(); // Wait for clip to finish.
	}
    /// <summary>
    /// Wait for music to be done.
    /// </summary>
    /// <returns></returns>
    IEnumerator AwaitMusic()
    {
        yield return new WaitForSeconds(.2f); // Wait 0.2f for clip.
        yield return new WaitUntil(() => !sM.MusicPlayer.isPlaying); // Wait until current clip is done playing.
    }
}

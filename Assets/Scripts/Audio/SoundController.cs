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

    private int midCount;

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
        sM.VoicePlayer.volume = 1.4f;
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

        Time.timeScale = 0.25f;
        sM.VoicePlayer.clip = sM.VoiceHolder[voiceNumber];

        float volumeBackup = sM.MusicPlayer.volume;

        sM.MusicPlayer.volume = volumeBackup * 0.35f;
        sM.VoicePlayer.Play();

        yield return null;

        yield return new WaitUntil(() => !sM.VoicePlayer.isPlaying);

        float time = Time.timeScale;

        while (time < 1)
        {
            time *= 1.25f;

            Time.timeScale = time;
            yield return null;
        };

        Time.timeScale = 1;
        sM.MusicPlayer.volume = volumeBackup;
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

	public void CheckBuildingDischarge(int buildingNumber) {
		buildingsCharged[buildingNumber - 1] = false;
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
		    
		    if (i == 0)
		    {
                SwarmSpawner.SpawnEnemy();
                StartCoroutine(FreezeGame(7, 1));
            } else if(i >= 1) {
		        SwarmSpawner.SpawnEnemies(i);
				if (!buildingsCharged[i])
					StartCoroutine(BuildingNotCharged());
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
		yield return new WaitForSeconds(1); // Wait a moment so the player is not instantly sent back.
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
        sM.MusicPlayer.clip = sM.MusicHolder[3];
        sM.MusicPlayer.Play();
        yield return FreezeGame(10, 2);
        sM.MusicPlayer.Stop();
        sM.MusicPlayer.clip = sM.MusicHolder[4];
        sM.MusicPlayer.Play();
        SwarmSpawner.SpawnEnemies(3); // Spawn 3 random enemies in the game.
        MidSection(); // Start the mid section of the game.
    }

    /// <summary>
    /// Midsection of the music with checks if the buildings are charged. 
    /// Does a new clip when appropriate building is charged. 
    /// After all 3 are charged then start survival section.
    /// </summary>
	void MidSection()
    {
            if (midCount == 0)
            {
                StartCoroutine(MidPartOne());
                midCount = midCount + 1;
            } else if(midCount == 1)
            {
                StartCoroutine(MidPartTwo());
                midCount = midCount + 1;
            } else if (midCount == 2)
            {
                StartCoroutine(MidPartThree());
                midCount = midCount + 1;
            } else if (midCount == 3)
            {
                StartCoroutine(SurvivalStart());
            }
	}

    /// <summary>
    /// First part of the midsection.
    /// </summary>
    /// <returns></returns>
	IEnumerator MidPartOne()
    {
        yield return PlayAndAwait(5); // Play clip 5 from MusicHolder.
        SwarmSpawner.SpawnEnemies(2); // Spawn 2 random enemies in the game.
        NumberChange.Invoke(); // Call a change in number.
		MidSection(); // Return to midsection.
	}
    /// <summary>
    /// Second part of the midsection.
    /// </summary>
    /// <returns></returns>
	IEnumerator MidPartTwo()
    {
        SwarmSpawner.SpawnEnemy();
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
        SwarmSpawner.SpawnEnemies(2);
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
        SwarmSpawner.SpawnEnemies(2);
		StartEndless.Invoke(); // Invoke start of endless survival.
		yield return PlayRange(8, 11); // Play clips from MusicHolder 8 to 10
	    SwarmSpawner.SpawnEnemies(2);
        yield return PlayRange(11, 14); // Play clips from MusicHolder 11 to 13.
        StartCoroutine(SurvivalEndless()); // Start endless survival section.
	}
    /// <summary>
    /// Sets survivalRunning to true and a while to keep the survival section looping.
    /// </summary>
    /// <returns></returns>
    IEnumerator SurvivalEndless()
    {
        survivalRunning = true;

        int currentEnemies = 3;
        int increaseEnemies = 1;
        while (survivalRunning)
        {
			if(!buildingsCharged[0] || !buildingsCharged[1] || !buildingsCharged[2]) {
				StartCoroutine(BuildingNotCharged());
			}
            yield return PlayRange(14, 16);
            SwarmSpawner.SpawnEnemies(currentEnemies);
            yield return PlayRange(16, 17);
            SwarmSpawner.SpawnEnemies(currentEnemies);
            currentEnemies += increaseEnemies;
        }
    }
    /// <summary>
    /// Play clip from min(inclusive) index to max(exclusive) index.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="callback">Optional callback to call after.</param>
    /// <returns></returns>
    IEnumerator PlayRange(int min, int max, System.Action callback = null)
    {
        for (int i = min; i < max; i++)
        {
            yield return PlayAndAwait(i);
            NumberChange.Invoke();
        }

        if (callback != null)
            callback();
    }
    /// <summary>
    /// Play the music clip and wait.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator PlayAndAwait(int index, System.Action callback = null)
    {
		sM.MusicPlayer.clip = sM.MusicHolder[index]; // Set clip Musicholder to musicplayer.
		sM.MusicPlayer.Play(); // Play the clip.
		yield return AwaitMusic(); // Wait for clip to finish.

        if (callback != null)
            callback();
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

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

	public bool[] buildingsCharged = new bool[3] { false, false, false };

	bool survivalRunning = false;

	// [0] = start song || [5] = start mid section || [13] = start drop || [15] = start end loop.

	void Awake() {
		Instance = this;
	}

	private void Start() {
		StartCoroutine(IntroStart()); // Start Ienumerator intro.
	}

	public void CheckBuildingCharge(int buildingNumber) // Set buildings to true if charged.
	{
		buildingsCharged[buildingNumber - 1] = true;
	}

	IEnumerator IntroStart() {
		for(int i = 0; i < buildingsCharged.Length; i++) {
			buildingsCharged[i] = false;
		}

		yield return PlayRange(0, 3);
		
		OnReset.Invoke(); // Invoke event for buildings.

		InBetweenSection(); // Start the inbetween section of the game.
	}

	IEnumerator BuildingNotCharged() {
		//Game over.
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene("MainMenu"); // Sends player to game over screen.
	}

	void InBetweenSection() {
		StartCoroutine(InBetween()); // Start Ienumerator of in between section.
	}
	IEnumerator InBetween() {
		yield return PlayRange(3, 5);
		MidSection(); // Start the mid section of the game.
	}

	IEnumerator AwaitMusic() {
		yield return new WaitForSeconds(.2f);
		yield return new WaitUntil(() => !SM.MusicPlayer.isPlaying);
	}

	void MidSection() {
		if(!buildingsCharged[0] && !buildingsCharged[1] && !buildingsCharged[2]) {
			StartCoroutine(MidPartOne());
		} else if(buildingsCharged[0] && !buildingsCharged[1] && !buildingsCharged[2]) {
			StartCoroutine(MidPartTwo());
		} else if(buildingsCharged[0] && buildingsCharged[1] && !buildingsCharged[2]) {
			StartCoroutine(MidPartThree());
		} else if(buildingsCharged[0] && buildingsCharged[1] && buildingsCharged[2]) {
			StartCoroutine(SurvivalStart());
		}
	}
	IEnumerator MidPartOne() {
		yield return PlayAndAwait(5);
		NumberChange.Invoke();
		MidSection();
	}
	IEnumerator MidPartTwo() {
		yield return PlayAndAwait(6);
		NumberChange.Invoke();
		MidSection();
	}
	IEnumerator MidPartThree() {
		yield return PlayAndAwait(7);
		NumberChange.Invoke();
		MidSection();
	}
		
	IEnumerator SurvivalStart() {
		StartEndless.Invoke();
		yield return PlayRange(8, 14);
		StartCoroutine(SurvivalEndless());
	}

	IEnumerator PlayAndAwait(int index) {
		SM.MusicPlayer.clip = SM.MusicHolder[index];
		SM.MusicPlayer.Play();
		yield return AwaitMusic();
	}

	/// <summary>
	/// Play songs from (inclusive)min index to (exclusive)max index.
	/// </summary>
	/// <param name="min">Inclusive</param>
	/// <param name="max">Exclusive</param>
	/// <returns></returns>
	IEnumerator PlayRange(int min, int max) {
		for(int i = min; i < max; i++) {
			yield return PlayAndAwait(i);
			NumberChange.Invoke();
		}
	}
	
	IEnumerator SurvivalEndless() {
		survivalRunning = true;
		
		while(survivalRunning) {
			yield return PlayRange(14, 18);
		}
	}
}

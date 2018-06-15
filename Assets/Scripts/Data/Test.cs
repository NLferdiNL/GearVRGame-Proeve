using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Pause();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Pause()
    {
        Time.timeScale = 0;
    }
}

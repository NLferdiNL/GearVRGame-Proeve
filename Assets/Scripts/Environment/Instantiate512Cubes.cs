using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour {
    public GameObject SampleCubePrefab;
    [SerializeField] 
    private GameObject[] sampleCube = new GameObject[512];
    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float amountListeners = 512f;
    [SerializeField]
    private float circleDegree = 360f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < amountListeners; i++)
        {
            GameObject instanceSampleCube = (GameObject)Instantiate(SampleCubePrefab);
            instanceSampleCube.transform.position = this.transform.position;
            instanceSampleCube.transform.parent = this.transform;
            instanceSampleCube.name = "AudioCube " + i; //Renames AudioCube along with the number it is
            this.transform.eulerAngles = new Vector3(0, -(circleDegree / amountListeners) * i, 0); //This places all 512 AudioCubes in a circle
            instanceSampleCube.transform.position = Vector3.forward * 100;
            sampleCube[i] = instanceSampleCube;
        }
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<amountListeners; i++)
        {
            if (sampleCube != null)
            {
                sampleCube[i].transform.localScale = new Vector3(1, (AudioVisualization.Samples[i] * maxScale) +2, 1);  
            }
        }
	}
}

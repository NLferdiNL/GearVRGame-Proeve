using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColor : MonoBehaviour {

	[SerializeField]
	Image image;

	[SerializeField]
	Color on, off;

	public void On() {
		image.color = on;
	}

	public void Off() {
		image.color = off;
	}
}

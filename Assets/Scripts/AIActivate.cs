using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActivate : MonoBehaviour {
	public GameObject AICars;
	public AutoCamControl autoCam;
	public Transform[] playerCar = new Transform[1];
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("AICars")) {
			print ("AICars is not a player pref. Creating now with value of 1");
			PlayerPrefs.SetInt("AICars", 1);
		}

		if (PlayerPrefs.GetInt ("AICars") == 0) {
			print ("Turning off AICars");
			AICars.SetActive (false);
			autoCam.cameraTargets = playerCar;
		}
	}
}

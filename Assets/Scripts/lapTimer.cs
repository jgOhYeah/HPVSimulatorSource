using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lapTimer : MonoBehaviour {
	public Text bestLap;
	public Text currentLap;
	public Text numberOfLaps;
	public HPVControl player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.bestLap != 0) {
			bestLap.text = "Best Lap: " + player.bestLap.ToString("F2") + "s";
		} else {
			bestLap.text = "";
		}
		if (player.notFirstLap) {
			currentLap.text = "Current Lap: " + player.currentLapTime.ToString ("F2") + "s";
			numberOfLaps.text = "Lap Number: " + (player.numberLaps).ToString ();
		} else { 
			numberOfLaps.text = "Welcome to the";
			currentLap.text = "HPV Simulator";
		}
	}
}

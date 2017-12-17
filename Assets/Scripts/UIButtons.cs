using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour {
	public GameObject playButton;
	public GameObject pauseButton;
	void Start(){
		playButton.SetActive (false);
		pauseButton.SetActive (true);
	}


	public void PauseGame(){
		Time.timeScale = 0;
		playButton.SetActive (true);
		pauseButton.SetActive (false);
		print ("Game Paused");
	}
	public void PlayGame(){
		Time.timeScale = 1;
		playButton.SetActive (false);
		pauseButton.SetActive (true);
		print ("Game in Play");
	}
}

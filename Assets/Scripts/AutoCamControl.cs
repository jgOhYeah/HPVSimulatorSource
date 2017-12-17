using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCamControl : MonoBehaviour {
	public float minChangeTime = 3;
	public float maxChangeTime = 10;
	public GameObject[] cameras;
	public Transform[] cameraTargets;

	private float changeTime = 0;
	private int currentCam = 0;
	private HelicopterCam[] cameraHeli;
	private Camera[] cameraCam;
	private bool AutoCamEnabled = true;

	void Start () {
		if (!PlayerPrefs.HasKey ("TVCam")) {
			print ("TVCam is not a player pref. Creating now with value of 0 (on bottom of main screen)");
			PlayerPrefs.SetInt ("TVCam", 0);
		}
		int targetDisp = 0;
		Vector2 position = new Vector2 (0f, 0f);
		Vector2 size = new Vector2 (1f, 1f);

		switch (PlayerPrefs.GetInt ("TVCam")) {
		case 0:
			position = new Vector2 (0.7f, 0f);
			size = new Vector2 (0.3f, 0.3f);
			break;
		case 1:
			targetDisp = 1;
			break;
		default:
			AutoCamEnabled = false;
			break;
		}
		cameraHeli = new HelicopterCam[cameras.Length];
		cameraCam = new Camera[cameras.Length];
		int count = 0;
		foreach (GameObject indiv in cameras) {
			cameraCam [count] = indiv.GetComponent<Camera> ();

			if (AutoCamEnabled) {
				cameraCam [count].rect = new Rect (position, size);
				cameraCam [count].targetDisplay = targetDisp;
			}

			cameraCam [count].enabled = false;

			//HelicopterCam iComponent
			cameraHeli [count] = indiv.GetComponent<HelicopterCam> ();
			if (cameraHeli [count] != null) {
				cameraHeli [count].zoomNow = false;
			}
			count++;
		}
		if (AutoCamEnabled) {
			ChangeCamera ();
		}
	}

	void Update () {
		if (AutoCamEnabled && (Time.time >= changeTime)) {
			ChangeCamera ();
		}
	}

	void ChangeCamera(){
			
		cameraCam [currentCam].enabled = false;
		if (cameraHeli [currentCam] != null) {
			cameraHeli [currentCam].zoomNow = false;
		}
		currentCam++;
		if (currentCam >= cameras.Length) {
			currentCam = 0;
		}
		cameraCam [currentCam].enabled = true;
		float waitTime = Random.Range (minChangeTime, maxChangeTime);
		changeTime = Time.time + waitTime;
		if (cameraHeli [currentCam] != null) {
			cameraHeli [currentCam].target = cameraTargets [Mathf.RoundToInt(Random.Range (0, cameraTargets.Length))];
			cameraHeli [currentCam].zoomNow = true;
			cameraHeli [currentCam].ZoomCamera (waitTime);
		}
		Debug.Log (cameras [currentCam].name + "\t" + waitTime + " Seconds", this);
	}
}

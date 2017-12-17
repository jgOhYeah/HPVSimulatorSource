using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour {
	public Camera camera1;
	public Camera camera2;

	private bool zeroCrossed;
	private int currentCam;
	// Use this for initialization
	void Start () {
		camera1.enabled = true;
		camera2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		float cameraTrigger = Input.GetAxis ("Change Camera");
		if (cameraTrigger < 0.5) {
			zeroCrossed = true;
			//Debug.Log ("Axis is close to 0");
		} else if (zeroCrossed && cameraTrigger >= 0.5) {
				//Button has been pressed
			camera1.enabled = !camera1.enabled;
			camera2.enabled = !camera2.enabled;
			zeroCrossed = false;
		}
	}
}

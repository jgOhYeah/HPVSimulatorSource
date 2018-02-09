using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class eastereggBarrel : MonoBehaviour {
	public Transform target;
	public Transform dropObject;
	public float timeBetweenDrops = 0.5f;
	public GameObject[] existingModels;
	public GameObject replacementModel;
	public GameObject mainLight;
	public GameObject flashLight;
	public float flashDuration = 1f;
	public HPVControl player;
	public AudioSource replacementHorn;

	public AudioSource song;
	public AudioSource intro;

	private string[] cheatCode;
	private int index;
	private bool isNotTriggered = true;
	private float nextTime = 0f;
	private bool isFlashing = false;

	void Start() {
		// Code is "rollingbarrel", user needs to input this in the right order
		cheatCode = new string[] { "r", "o", "l", "l", "i", "n", "g", "b", "a", "r", "r", "e", "l" };
		index = 0;
	}

	void Update() {
		// Check if any key is pressed
		if (Input.anyKeyDown && isNotTriggered) {
			// Check if the next key in the code is pressed
			if (Input.GetKeyDown(cheatCode[index])) {
				// Add 1 to index to check the next key in the code
				index++;
			}
			// Wrong key entered, we reset code typing
			else {
				index = 0;    
			}
		}

		// If index reaches the length of the cheatCode string, 
		// the entire code was correctly entered
		if ((index == cheatCode.Length) && isNotTriggered) {
			isNotTriggered = false;
			// Cheat code successfully inputted!
			// Unlock crazy cheat code stuff
			Resources.LoadAll("ohYeah");
			replacementHorn.Play();
			player.hornSound = replacementHorn;
			print("Oh Yeah!!!");
			intro.Play ();
			song.Play(88200);
			StartCoroutine ("lightFlash");
			foreach (GameObject objectToDisable in existingModels) {
				objectToDisable.SetActive (false);
			}
			replacementModel.GetComponent<MeshRenderer> ().enabled = true;
		}
		if (!isNotTriggered && Time.time >= nextTime) {
			nextTime = Time.time + timeBetweenDrops;
			print ("Object Dropped!!!");
			dropObjects ();
		}
		if (!isNotTriggered) {
			float hornInput = Input.GetAxisRaw ("Horn");
			if (hornInput > 0.5 && !isFlashing) {
				StartCoroutine ("lightFlash");
			}
		}
	}
	private void dropObjects() {
		Instantiate (dropObject, new Vector3(target.position.x,target.position.y,target.position.z - 3), Quaternion.Euler(new Vector3(-90,0,0)),target);
	}
	IEnumerator lightFlash() {
		isFlashing = true;
		for (int i = 0; i < 2; i++) {
			mainLight.SetActive (false);
			flashLight.SetActive (true);
			yield return new WaitForSeconds (flashDuration);
			mainLight.SetActive (true);
			flashLight.SetActive (false);
			yield return new WaitForSeconds (flashDuration);
		}
		isFlashing = false;
	}
}

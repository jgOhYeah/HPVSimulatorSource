using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class eastereggBarrel : MonoBehaviour {
	public Transform target;
	public Transform dropObject;
	public float timeBetweenDrops = 0.5f;
	public GameObject existingModel;
	public GameObject replacementModel;
	private string[] cheatCode;
	private int index;
	private AudioSource song;
	private bool isNotTriggered = true;
	private float nextTime = 0f;

	void Start() {
		// Code is "rollingbarrel", user needs to input this in the right order
		cheatCode = new string[] { "r", "o", "l", "l", "i", "n", "g", "b", "a", "r", "r", "e", "l" };
		index = 0;
		song = GetComponent<AudioSource> ();
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
			print("Oh Yeah!!!");
			song.Play ();
			MeshRenderer[] meshToDisable = existingModel.GetComponentsInChildren<MeshRenderer> ();
			foreach (MeshRenderer mesh in meshToDisable) {
				if (mesh != null) {
					mesh.enabled = false;
				}
			}
			replacementModel.GetComponent<MeshRenderer> ().enabled = true;
		}
		if (!isNotTriggered && Time.time >= nextTime) {
			nextTime = Time.time + timeBetweenDrops;
			print ("Object Dropped!!!");
			dropObjects ();
		}
	}
	private void dropObjects() {
		Instantiate (dropObject, new Vector3(target.position.x,target.position.y,target.position.z - 3), Quaternion.Euler(new Vector3(-90,0,0)),target);
	}
}

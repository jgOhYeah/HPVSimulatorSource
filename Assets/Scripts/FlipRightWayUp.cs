using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipRightWayUp : MonoBehaviour {
	public float sensitivity = 0.1f;
	public float delay = 5.0f;
	public float yHeight = 3f;

	private bool coroutineRunning = false;
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Dot (transform.up, Vector3.down) > sensitivity) {
			//print ("Car is upside down");
			if (!coroutineRunning) { 
				StartCoroutine ("flipTimer");
				print ("Coroutine started");
			}
		} else if (coroutineRunning) {
			StopCoroutine ("flipTimer");
			print ("Coroutine Stopped");
			coroutineRunning = false;
		}
	}
	IEnumerator flipTimer() {
		coroutineRunning = true;
		yield return new WaitForSeconds(delay);
		print ("Car is still upside down");
		transform.rotation = Quaternion.identity;
		transform.position = new Vector3 (transform.position.x, transform.position.y + yHeight, transform.position.z);
		print ("Set position");
		coroutineRunning = false;
	}
}

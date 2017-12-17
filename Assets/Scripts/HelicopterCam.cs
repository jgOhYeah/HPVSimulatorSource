using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterCam : MonoBehaviour {
	public Transform target;
	public float minRandZoom;
	public float maxRandZoom;
	public float maxDistance = 255;
	public float halfViewLength = 10;
	public bool zoomNow = false;

	private Camera cam;
	private float zoomPerSecond = 0;
	private float currentAdjust;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if (zoomNow) {
			transform.LookAt (target);
			float distance = Vector3.Distance (transform.position, target.position);
			//Debug.Log (distance);
			currentAdjust += zoomPerSecond * Time.deltaTime;
			float aTanDegrees = Mathf.Rad2Deg * Mathf.Atan ((halfViewLength + currentAdjust) / distance);
			cam.fieldOfView = aTanDegrees;
		} else {
			currentAdjust = 0;
		}
	}
	public void ZoomCamera(float zoomTime) {
		float zoomDistance = Random.Range (minRandZoom, maxRandZoom);
		zoomPerSecond = zoomDistance / zoomTime;
		Debug.Log (zoomDistance + " Zoom Distance\t" + zoomPerSecond + " Per Second");
	}
}

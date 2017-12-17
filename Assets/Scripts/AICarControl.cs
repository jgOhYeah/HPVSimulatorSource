using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarControl : MonoBehaviour {
	public Transform waypointContainer;
	public float tollerence = 5f;
	public float steeringSensitivity = 0.5f;

	private Transform[] wayPoints;
	private int waypointCount = 1;
	private HPVControl HPVController;
	// Use this for initialization
	void Start () {
		//int numberOfWayPoints = waypointContainer.childCount;
		//print (numberOfWayPoints);
		wayPoints = waypointContainer.GetComponentsInChildren<Transform> ();
		/*foreach (Transform waypoint in wayPoints) {
			//Only Execute for children.
			if (waypoint != waypointContainer) {
				float distance = Vector3.Distance (waypoint.position, transform.position);
				Vector3 localTarget = transform.InverseTransformPoint (waypoint.position);
				float angle = Mathf.Atan2 (localTarget.x, localTarget.z) * Mathf.Rad2Deg;
				print ("Angle to " + waypoint.name + " is " + angle + " degrees\tDistance to " + waypoint.name + " is " + distance);
			}
		}*/
		HPVController = GetComponent<HPVControl> ();
		HPVController.pedalInput = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		PSEUDOCODE OH YEAH!!!
		Select Next waypoint in list
		If distance to waypoint < tollerence then select next waypoint
		Work out angle from HPV to waypoint
		It negative then turn left else turn right;
		might need some proportional calucluation but oh well
		*/

		//Check if distance within zone and move on to next if it is.
		if (Vector3.Distance (wayPoints [waypointCount].position, transform.position) < tollerence) {
			//print ("Reached Waypoint " + wayPoints [waypointCount].name);
			waypointCount++;
			if (waypointCount == wayPoints.Length) {
				//Skip 0 as this is the parent.
				waypointCount = 1;
			}
		}
		Vector3 localTarget = transform.InverseTransformPoint (wayPoints[waypointCount].position);
		float angle = Mathf.Atan2 (localTarget.x, localTarget.z) * Mathf.Rad2Deg;
		if (angle < 0) {
			HPVController.steerInput = Mathf.Max (-1f, angle * steeringSensitivity);
		} else {
			HPVController.steerInput = Mathf.Min (1f, angle * steeringSensitivity);
		}

	}
}

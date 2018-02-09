using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetupTransformRotate : MonoBehaviour {
	public Transform objectToPlace;
	void Start () {
		objectToPlace.position = transform.position;
		objectToPlace.rotation = transform.rotation;
	}
}

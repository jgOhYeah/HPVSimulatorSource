using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SateliteControl : MonoBehaviour {
	public Transform rotationCentre;
	public float rotationSpeed;
	public Vector3 rotationAxis = Vector3.up;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround (rotationCentre.position, rotationAxis, rotationSpeed * Time.deltaTime);
	}
}

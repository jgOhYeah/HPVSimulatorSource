using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHPVControl : MonoBehaviour {
	private HPVControl HPVController;
	// Use this for initialization
	void Start () {
		HPVController = GetComponent<HPVControl> ();
	}
	
	// Update is called once per frame
	void Update () {
		HPVController.gearInput = Input.GetAxisRaw ("Vertical");
		HPVController.hornInput = Input.GetAxisRaw ("Horn");
	}
	void FixedUpdate () {
		HPVController.brakeInput = Input.GetAxis ("Front Brake");
		HPVController.rearBrakeInput = Input.GetAxis ("Back Brake");
		HPVController.pedalInput = Input.GetAxis ("Pedal");
		HPVController.steerInput = Input.GetAxis ("Horizontal");
	}
}

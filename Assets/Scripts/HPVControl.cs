using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HPVControl : MonoBehaviour {
	//Inputs
	public float hornInput;
	public float pedalInput;
	public float steerInput;
	public float brakeInput;
	public float rearBrakeInput;
	public float gearInput;

	//Stuff controlling the car movement
	//If you do not use center of mass the wheels base it off of the colliders
	//By using center of mass you can control where it is.
	public Transform t_CenterOfMass;
	//The max distance a wheel can turn.
	public float maxSteerAngle = 45f;
	//Each wheel needs its own mesh
	public Transform[] wheelMesh = new Transform[3];
	//The physics of the wheels, max 20 axels.
	//WheelCollider[4] 4 is how many wheels we have.
	public WheelCollider[] wheelCollider = new WheelCollider[3];
	//Ridged body accessor.

	//Stuff that can be accessed by other scripts
	public float velocity;
	public float cadence;
	public float power;
	public int gear = 0;

	//Settings and stuff
	public float brakeAdjust = 1;
	public float rearBrakeAdjust = 1;
	public float cadAdjust = 1;
	public float cadDispAdjust = 1;
	public int maxCad = 110;
	public float powerAdjust = 1;
	public float velocityAdjust = 1;
	public float[] gearRatios = new float[10];
	public float[] torqueAtCadance = new float[8];
	//public float[] maxSpeedPerGear = new float[10];

	//Parts
	public Transform pedals;
	public AudioSource hornSound;

	//Private stuff
	private Rigidbody r_Ridgedbody;
	private bool gearZeroCrossed = false;

	public void Start()
	{
		// This sets where the center of mass is, if you look r_Ridgedbody."centerOfMass" is a function of ridged body.
		r_Ridgedbody = GetComponent<Rigidbody>();
		r_Ridgedbody.centerOfMass = t_CenterOfMass.localPosition;
		hornSound.mute = true;
	}

	public void Update()
	{
		//Sets the wheel meshs to match the rotation of the physics WheelCollider.
		UpdateMeshPosition();
		if (gearInput < 0.5 && gearInput > -0.5) {
			gearZeroCrossed = true;
			//Debug.Log ("Axis is close to 0");
		} else if (gearZeroCrossed) {
			if (gearInput >= 0.5 && gear < 9) {
				gear++;
				gearZeroCrossed = false;
			} else if (gearInput <= -0.5 && gear > 0) {
				gear--;
				gearZeroCrossed = false;
			}
		}

		pedals.Rotate (new Vector3 (0, 0, -cadence * 6 * Time.deltaTime * cadDispAdjust));
		if (hornInput > 0.5) {
			if (hornSound.mute) {
				hornSound.mute = false;
			}
		} else if (!hornSound.mute) {
			hornSound.mute = true;
		}
	}

	public void FixedUpdate() {
		velocity = r_Ridgedbody.velocity.magnitude * velocityAdjust;
		if (pedalInput > 0) {
			cadence = velocity * cadAdjust / gearRatios [gear];
		} else {
			cadence = 0;
		}

		float torqueMultiplier = 0;
		if (cadence < 20) {
			torqueMultiplier = torqueAtCadance [0];
		} else if (cadence <= 40) {
			torqueMultiplier = torqueAtCadance [1];
		} else if (cadence <= 60) {
			torqueMultiplier = torqueAtCadance [2];
		} else if (cadence <= 80) {
			torqueMultiplier = torqueAtCadance [3];
		} else if (cadence <= 100) {
			torqueMultiplier = torqueAtCadance [4];
		} else if (cadence <= 110) {
			torqueMultiplier = torqueAtCadance [5];
		} else if (cadence <= 120) {
			torqueMultiplier = torqueAtCadance [6];
		} else if (cadence > 120) {
			torqueMultiplier = torqueAtCadance [7];
		}
		float pedalTorque = pedalInput * torqueMultiplier;
		float torque = pedalTorque / gearRatios[gear];

		float steer = steerInput * maxSteerAngle;

		//Sets which wheels turn, this is the two front wheels.
		wheelCollider[0].steerAngle = steer;
		wheelCollider [1].steerAngle = steer;

		//Sets which wheels move forward or backwards.
		/*for (int i = 0; i < 3; i++)
		{
			wheelCollider[i].motorTorque = torque;
		}*/
		wheelCollider[2].motorTorque = torque;

		float brakeTorque = brakeInput * brakeAdjust;
		/*if (brakeTorque > 0.01) {
			Debug.Log ("Brake On: " + brakeTorque);
			wheelCollider [2].motorTorque = 0f;
			for (int i = 0; i < 2; i++) {
				wheelCollider [i].brakeTorque = brakeTorque;
			}
		} else {
			Debug.Log ("Brake Off: " + brakeTorque);
			wheelCollider [2].motorTorque = torque;
			for (int i = 0; i < 2; i++) {
				wheelCollider [i].brakeTorque = 0f;
			}
		}*/
		wheelCollider [2].motorTorque = torque;
		for (int i = 0; i < 2; i++) {
			wheelCollider [i].brakeTorque = brakeTorque;
		}
		wheelCollider [2].brakeTorque = rearBrakeAdjust * rearBrakeInput;
		power = pedalTorque * powerAdjust;
	}

	//Sets each wheel to move with the physics WheelColliders.
	public void UpdateMeshPosition() {
		for (int i = 0; i < 3; i++) {
			Quaternion quat;
			Vector3 pos;

			//Gets the current position of the physics WheelColliders.
			wheelCollider [i].GetWorldPose (out pos, out quat);

			///Sets the mesh to match the position and rotation of the physics WheelColliders.
			wheelMesh [i].position = pos;
			wheelMesh [i].rotation = quat;
		}
	}
}
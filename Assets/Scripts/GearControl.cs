using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearControl : MonoBehaviour {
	public float velocity;
	public float velocityAdjust;
	public int cadence;
	public float cadenceAdjust;
	public int gear = 0;
	public float gearRatio;

	private float[] gearRatios = { 5f, 4.5f, 4f, 3.5f, 3f, 2.5f, 2f, 1.5f, 1f, 0.5f };
	private Rigidbody carBody;
	private bool gearZeroCrossed = false;
	// Use this for initialization
	void Start () {
		carBody = GetComponent<Rigidbody> ();
	}
	void Update () {
		float verticalInput = Input.GetAxisRaw ("Vertical");
		//Debug.Log (verticalInput);
		if (verticalInput < 0.5 && verticalInput > -0.5) {
			gearZeroCrossed = true;
			//Debug.Log ("Axis is close to 0");
		} else if (gearZeroCrossed) {
			if (verticalInput >= 0.5 && gear < 9) {
				gear++;
				gearZeroCrossed = false;
			} else if (verticalInput <= -0.5 && gear > 0) {
				gear--;
				gearZeroCrossed = false;
			}
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		gearRatio = gearRatios [gear];
		velocity = carBody.velocity.magnitude * velocityAdjust;
		float cadTemp = cadenceAdjust * velocity * gearRatio;
		cadence = (int)cadTemp;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeControl : MonoBehaviour {
	public float degreesPerSpeed = 1;
	public float degreesPerCad = 1;
	public float speedStartPos = 0;
	public float cadStartPos = 0;
	public RectTransform speed;
	public RectTransform cadence;
	public Text gear;
	public HPVControl currentInfo;

	public int speedAverageLength = 5;
	public int cadAverageLength = 5;

	private float[] speedRollingAve;
	private int currentSpeedAve = 0;

	private float[] cadRollingAve;
	private int currentCadAve = 0;

	private float averagedSpeed = 0;
	private float averagedCad = 0;

	void Start() {
		speedRollingAve = new float[speedAverageLength];
		for (int i = 0; i < speedRollingAve.Length; i++) {
			speedRollingAve [i] = 0;
		}
		cadRollingAve = new float[cadAverageLength];
		for (int i = 0; i < cadRollingAve.Length; i++) {
			cadRollingAve [i] = 0;
		}
	}
	void OnGUI () {
		//speed.text = Mathf.RoundToInt (currentInfo.velocity).ToString ();
		//speed.text = ((int)currentInfo.velocity).ToString ();
		//cadence.text = currentInfo.cadence.ToString();

		//cadence. = Mathf.RoundToInt (currentInfo.cadence).ToString ();
		speed.rotation = Quaternion.Euler(new Vector3(0f, 0f, -(speedStartPos + (averagedSpeed * degreesPerSpeed))));
		cadence.rotation = Quaternion.Euler(new Vector3(0f, 0f, -(cadStartPos + (averagedCad * degreesPerCad))));
		int uiGear = currentInfo.gear + 1;
		gear.text = uiGear.ToString();
	}

	void FixedUpdate (){
		currentSpeedAve++;
		if (currentSpeedAve >= speedRollingAve.Length) {
			currentSpeedAve = 0;
		}
		speedRollingAve [currentSpeedAve] = currentInfo.velocity;
		averagedSpeed = AverageArray (speedRollingAve);

		currentCadAve++;
		if (currentCadAve >= cadRollingAve.Length) {
			currentCadAve = 0;
		}
		cadRollingAve [currentCadAve] = currentInfo.cadence;
		averagedCad = AverageArray (cadRollingAve);
	}

	float AverageArray (float[] array){
		float average = 0;
		for (int i = 0; i < array.Length; i++) {
			average += array [i];
		}
		return average / array.Length;
	}
}
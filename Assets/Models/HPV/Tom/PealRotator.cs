using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PealRotator : MonoBehaviour {
	public float adjustment;
	public GearControl currentInfo;

	void Start () {
		//currentInfo = GetComponent<GearControl> ();
	}

	void Update () {
		//Debug.Log(velocity);
		transform.Rotate (new Vector3 (0, 0, -currentInfo.velocity * currentInfo.gearRatio * adjustment * Time.deltaTime));
	}
}

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform player;
	public Vector3 offset = new Vector3(0f, 2.22f, -4.62f);
	private Vector3 origAngle;
	// Use this for initialization
	void Start () {
		//offset = transform.position - player.position;
		origAngle = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//Set to be behind car
		transform.position = player.position + offset;
		transform.rotation = Quaternion.Euler(origAngle); 
		transform.RotateAround (player.position, Vector3.up, player.eulerAngles.y);
	}

	/*float Difference (float num1, float num2) {
		float difference = num1 - num2;
		if (difference < 0) {
			difference *= -1;
		}
		Debug.Log ("Difference: " + difference);
		return difference;
	}*/
}

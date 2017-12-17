using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayControl : MonoBehaviour {
	public Text speed;
	public Text cadence;
	public Text gear;
	public HPVControl currentInfo;

	void OnGUI () {
		//speed.text = Mathf.RoundToInt (currentInfo.velocity).ToString ();
		speed.text = ((int)currentInfo.velocity).ToString ();
		cadence.text = currentInfo.cadence.ToString();
		cadence.text = Mathf.RoundToInt (currentInfo.cadence).ToString ();
		int uiGear = currentInfo.gear + 1;
		gear.text = uiGear.ToString();
	}
}

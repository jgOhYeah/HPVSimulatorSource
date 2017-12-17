using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
	public Dropdown TVCamDropdown;
	public Toggle AICarToggle;
	void Start() {
		TVCamDropdown.value = PlayerPrefs.GetInt ("TVCam");
		print ("Set Dropdown to " + PlayerPrefs.GetInt ("TVCam"));
		int AICarIntValue = PlayerPrefs.GetInt ("AICars");
		bool AICarBoolValue = false;
		if (AICarIntValue == 1) {
			AICarBoolValue = true;
		}
		AICarToggle.isOn = AICarBoolValue;
		print ("Set AICar Toggle to " + AICarBoolValue);

		print ("Listeners created");
		TVCamDropdown.onValueChanged.AddListener(delegate {setTVCamPrefs(); });
		AICarToggle.onValueChanged.AddListener(delegate {setAICarPrefs(); });
	}
	public void setTVCamPrefs(){
		int TVCamValue = TVCamDropdown.value;
		print ("Value Changed to " + TVCamValue);
		PlayerPrefs.SetInt ("TVCam", TVCamValue);
	}
	public void setAICarPrefs(){
		if (AICarToggle.isOn) {
			PlayerPrefs.SetInt ("AICars", 1);
		} else {
			PlayerPrefs.SetInt ("AICars", 0);
		}
		print ("Checkbox Value Changed");
	}
}

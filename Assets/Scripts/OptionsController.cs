using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
	public Dropdown TVCamDropdown;
	public Dropdown ShellDropdown;
	public Toggle AICarToggle;
	public GameObject ResetPane;
	void Start() {
		TVCamDropdown.value = PlayerPrefs.GetInt ("TVCam");
		print ("Set Dropdown to " + PlayerPrefs.GetInt ("TVCam"));
		int AICarIntValue = PlayerPrefs.GetInt ("AICars");
		bool AICarBoolValue = false;
		if (AICarIntValue == 1) {
			AICarBoolValue = true;
		}
		ShellDropdown.value = PlayerPrefs.GetInt ("SetShell");
		print ("Set Dropdown to " + PlayerPrefs.GetInt ("SetShell"));
		AICarToggle.isOn = AICarBoolValue;
		print ("Set AICar Toggle to " + AICarBoolValue);

		print ("Listeners created");
		TVCamDropdown.onValueChanged.AddListener (delegate {
			setTVCamPrefs ();
		});
		ShellDropdown.onValueChanged.AddListener (delegate {
			setShellPrefs ();
		});
		AICarToggle.onValueChanged.AddListener (delegate {
			setAICarPrefs ();
		});
	}
	void setTVCamPrefs(){
		int TVCamValue = TVCamDropdown.value;
		print ("Value Changed to " + TVCamValue);
		PlayerPrefs.SetInt ("TVCam", TVCamValue);
	}
	void setAICarPrefs(){
		if (AICarToggle.isOn) {
			PlayerPrefs.SetInt ("AICars", 1);
		} else {
			PlayerPrefs.SetInt ("AICars", 0);
		}
		print ("Checkbox Value Changed");
	}
	void setShellPrefs(){
		int ShellValue = ShellDropdown.value;
		print ("Value Changed to " + ShellValue);
		PlayerPrefs.SetInt ("SetShell", ShellValue);
	}
	public void resetAll() {
		PlayerPrefs.DeleteAll ();
		ResetPane.SetActive (false);
		print ("All player prefs deleted");
	}
}

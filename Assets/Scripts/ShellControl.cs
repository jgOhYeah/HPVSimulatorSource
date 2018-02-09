using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour {
	public GameObject EVO17;
	public Transform EVO17Pedals;
	public GameObject Hippo;
	public Transform HippoPedals;
	public GameObject[] TrainerTrikeNotBits = new GameObject[1];

	private HPVControl hpvControl;
	// Use this for initialization
	void Start () {
		hpvControl = GetComponent<HPVControl> ();
		switch (PlayerPrefs.GetInt ("SetShell", 0)) {
		case 0:
			Hippo.SetActive (false);
			EVO17.SetActive (true);
			print ("EVO17");
			hpvControl.pedals = EVO17Pedals;
			break;
		case 1:
			Hippo.SetActive (true);
			EVO17.SetActive (false);
			print ("Hippo");
			hpvControl.pedals = HippoPedals;
			break;
		case 2:
			Hippo.SetActive (false);
			EVO17.SetActive (true);
			hpvControl.pedals = EVO17Pedals;
			foreach (GameObject trainerTrike in TrainerTrikeNotBits) {
				trainerTrike.SetActive (false);
			}
			print ("TrainerTrike");
			break;
		}
	}
}

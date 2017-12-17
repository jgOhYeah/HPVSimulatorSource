using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {
	public void GotoLevel(string sceneName){
		Time.timeScale = 1;
		PlayerPrefs.Save ();
		SceneManager.LoadScene (sceneName);
	}
}

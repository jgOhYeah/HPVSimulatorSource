using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour {
	public Sprite[] images;
	public float speed = 1f;
	public Text cannotLeave;
	public string[] messages = {"", "You cannot leave!!!", "Why would you want to anyway?" };

	private Image imageView;
	private float changeTime = 0f;
	private int count = 0;
	private int textCount = 0;
	// Use this for initialization
	void Start () {
		imageView = GetComponent<Image> ();
		cannotLeave.text = messages[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			textCount++;
			if (textCount == messages.Length) {
				textCount = 0;
			}
			cannotLeave.text = messages [textCount];
		}
		if (Time.time >= changeTime) {
			changeTime = Time.time + speed;
			count++;
			if (count == images.Length) {
				count = 0;
			}
			imageView.sprite = images [count];
		}
	}
}

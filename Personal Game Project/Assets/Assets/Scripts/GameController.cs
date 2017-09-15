using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Camera cam;
	public GameObject apple;
	public float timeLeft;
	public Text timerText;
	public GameObject gameOverText;
	public GameObject restartButton;
	public GameObject startText;
	public GameObject startButton;
	public HatController appleController;

	private float maxWidth;
	private bool playing;

	void Start () {

		if (cam == null) {
			cam = Camera.main;
		}
		playing = false;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float ballWidth = apple.GetComponent<Renderer>().bounds.extents.x;
		maxWidth = targetWidth.x - ballWidth;
		UpdateText ();
	}

	public void StartGame () {
		startText.SetActive (false);
		startButton.SetActive (false);
		appleController.ToggleControl (true); 
		StartCoroutine (Spawn ());
	}

	void FixedUpdate() {
		if(playing) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = 0;
			}
			UpdateText ();
		}
	}

	IEnumerator Spawn(){
		yield return new WaitForSeconds (2.0f);
		playing = true;
		while (timeLeft > 0) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-maxWidth, maxWidth), transform.position.y, 0.0f);
			Quaternion spawnRotation = Quaternion.identity;
			Instantiate (apple, spawnPosition, spawnRotation);
			yield return new WaitForSeconds (Random.Range (0.10f, 0.5f));
		}
		yield return new WaitForSeconds (1.0f);
		gameOverText.SetActive (true);
		yield return new WaitForSeconds (1.0f);
		restartButton.SetActive (true);
	}

	void UpdateText() {
		timerText.text = "Time Left:\n" + Mathf.RoundToInt (timeLeft);
	}
}

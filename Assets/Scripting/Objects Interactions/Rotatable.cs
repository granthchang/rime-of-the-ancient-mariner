using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{
	private Transform player;
	private AudioSource audio;

	private bool isRotatble = false;
	private bool isVisible = false;
	[SerializeField] private float triggerSize = 1f;
	[SerializeField] private float rotationSpeed = 10f;

	private void Start() {
		player = PlayerMovement_Location.main.transform;
		audio = this.GetComponent<AudioSource>();
	}

	private void Update() {
		float distFromPlayer = (this.transform.position - player.position).magnitude;

		// When within range of player to be rotatable
		if (isVisible && distFromPlayer < triggerSize && !isRotatble) {
			isRotatble = true;
			UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.rotateObject);
		} else if (isRotatble && (distFromPlayer >= triggerSize || !isVisible)) {
			isRotatble = false;
			UI_ToolTip.main.hideToolTip();
		}

		// Rotate mirrors and hide tooltip
		if (isRotatble && Input.GetKey(KeyCode.Q)) {
			UI_ToolTip.main.hideToolTip();
			this.transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
			playSound();
		} else if (isRotatble && Input.GetKey(KeyCode.E)) {
			UI_ToolTip.main.hideToolTip();
			this.transform.Rotate(new Vector3(0, rotationSpeed, 0) * -Time.deltaTime);
			playSound();
		}

		// Show tooltip when mirrors are no longer rotating
		if (isRotatble && (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))) {
			UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.rotateObject);
			stopSound();
		}
	}


	public void playSound() {
		if (!audio.isPlaying) {
			audio.Play();
		}
	}

	public void stopSound() {
		if (audio.isPlaying) {
			audio.Stop();
		}
	}

	private void OnBecameVisible() { isVisible = true; }
	private void OnBecameInvisible() { isVisible = false; }
}

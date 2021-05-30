using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
	private Transform player;
	private Transform defaultParent;
	private AudioSource audio;

	private bool isMovable = false;
	private bool isVisible = false;
	[SerializeField] private float triggerSize = 1f;

	private void Start() {
		player = PlayerMovement_Location.main.transform;
		defaultParent = this.transform.parent;
		audio = this.GetComponent<AudioSource>();
	}

	private void Update() {
		float distFromPlayer = (this.transform.position - player.position).magnitude;

		// When within range of player to be moved
		if (isVisible && distFromPlayer < triggerSize && !isMovable) {
			isMovable = true;
			UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.moveObject);
			PlayerMovement_Location.main.pushableObject = this.transform;
		} else if (isMovable && (distFromPlayer >= triggerSize || !isVisible)) {
			isMovable = false;
			UI_ToolTip.main.hideToolTip();
			PlayerMovement_Location.main.pushableObject = null;
		}

		// Show/hide tooltip when moving
		if (isMovable && Input.GetMouseButtonUp(0)) {
			UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.moveObject);
			this.transform.SetParent(defaultParent);
		} else if (isMovable && Input.GetMouseButtonDown(0)) {
			UI_ToolTip.main.hideToolTip();
			this.transform.SetParent(player);
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

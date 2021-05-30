using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
    public UnityEvent onCheckpointReached;

	private void Start() {
        if (PlayerPrefs.GetInt("Current Checkpoint") >= checkpointNumber)
            GameObject.Destroy(this.gameObject);
	}

	private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            // Set checkpoint
            PlayerPrefs.SetInt("Current Checkpoint", checkpointNumber);
            this.GetComponent<AudioSource>().Play();

            // Remove this collider
            this.GetComponent<Collider>().enabled = false;
            onCheckpointReached.Invoke();
        }
	}
}

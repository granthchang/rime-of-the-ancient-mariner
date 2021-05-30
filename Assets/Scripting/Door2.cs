using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    private Animator animator;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        audio = this.GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Current Checkpoint") >= 3) {
            animator.Play("Door open", 0, 1f);
		}
    }

    public void openDoor() {
        animator.Play("Door open");
        audio.Play();
        Debug.Log("Door 2 opened");
    }

    public void closeDoor() {
        animator.Play("Door close");
        audio.Play();
	}
}

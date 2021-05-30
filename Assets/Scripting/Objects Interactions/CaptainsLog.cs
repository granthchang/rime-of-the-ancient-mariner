using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainsLog : MonoBehaviour
{
    // Text for captain's log
    [TextArea(0,100)] public string text;
    [SerializeField] private float rangeToInteract = 1f;
    private bool isVisible;

    private AudioSource audio;

	private void Start() {
        audio = this.GetComponent<AudioSource>();
	}

	public string getText() {
        return text;
	}

    // For in-range hitbox
    private bool interactable, interacting;

    // Handles input
    void Update() {

        // Opening log
        if (Input.GetKeyDown(KeyCode.Space) && interactable && !interacting) {
            openLog();
        }

        // Closing log
        else if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) && interacting) {
            closeLog();
		}
    }

    // Show Tooltip if player is within range
    private void FixedUpdate() {
        float distFromPlayer = (PlayerMovement_Location.main.transform.position - this.transform.position).magnitude;

        if (isVisible && distFromPlayer < rangeToInteract && !interactable) {
            interactable = true;
            UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.inspect);
        } else if (interactable && (distFromPlayer > rangeToInteract || !isVisible)) {
            interactable = false;
            UI_ToolTip.main.hideToolTip();
        }
	}

    public void openLog() {
        interacting = true;
        audio.Play();
        UI_CaptainsLog.main.showLog(text);
        UI_ToolTip.main.hideToolTip();
    }

    public void closeLog() {
        interacting = false;
        UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.inspect);
        UI_CaptainsLog.main.hideLog();
        audio.Play();
    }

    private void OnBecameVisible() { isVisible = true; }
    private void OnBecameInvisible() { isVisible = false; }
}

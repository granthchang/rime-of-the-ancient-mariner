using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBottle : MonoBehaviour
{
    [SerializeField] private float distanceToInteract = 1f;
    private bool showingTooltip;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Current Checkpoint") >= 3)
            GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Picking up the bottle
        if (Input.GetKeyDown(KeyCode.Space) && showingTooltip) {
            UI_ToolTip.main.hideToolTip();
            this.GetComponent<AudioSource>().Play();
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(false);
            PlayerCapture.main.currentPhase = PlayerCapture.CapturePhase.hasBottle;
            this.enabled = false;

            PlayerPrefs.SetInt("Current Checkpoint", 3);
            GameObject.Find("Checkpoint 3").GetComponent<AudioSource>().Play();
        }
    }

	private void FixedUpdate() {
        float distToPlayer = Vector3.Distance(this.transform.position, PlayerMovement_Location.main.transform.position);

        // Show and hide tooltip
        if (!showingTooltip && distToPlayer <= distanceToInteract) {
            UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.takeBottle);
            showingTooltip = true;
        } else if (showingTooltip && distToPlayer >= distanceToInteract) {
            UI_ToolTip.main.hideToolTip();
            showingTooltip = false;
        }
	}

    IEnumerator destroyGameobject() {
        yield return new WaitForSeconds(0.25f);
        GameObject.Destroy(this.gameObject);
    }
}

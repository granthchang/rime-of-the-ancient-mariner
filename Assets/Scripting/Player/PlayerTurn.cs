using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    GameObject mast;
    GameObject player;
    PlayerInteractable interactableScript;
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactableScript = player.GetComponent<PlayerInteractable>();
        mast = GameObject.Find("MidMastDevMesh");
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (!interactableScript.canTurnWheel && distFromPlayer < 3)
        {
            interactableScript.canTurnWheel = true;
            UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.rotateObject);
        }
        else if (interactableScript.canTurnWheel && distFromPlayer >= 3)
        {
            interactableScript.canTurnWheel = false;
            UI_ToolTip.main.hideToolTip();
        }
        
        if (interactableScript.canTurnWheel && Input.GetKey(KeyCode.Q)) {
            UI_ToolTip.main.hideToolTip();
            transform.Rotate(Vector3.up * 5 * Time.deltaTime, Space.Self);
            mast.transform.Rotate(Vector3.up * 10 * Time.deltaTime, Space.Self);
            if (!audio.isPlaying) {
                audio.Play();
            }
        } else if (interactableScript.canTurnWheel && Input.GetKey(KeyCode.E)) {
            UI_ToolTip.main.hideToolTip();
            transform.Rotate(Vector3.up * -5 * Time.deltaTime, Space.Self);
            mast.transform.Rotate(Vector3.up * -10 * Time.deltaTime, Space.Self);
            if (!audio.isPlaying) {
                audio.Play();
            }
        } else
            audio.Stop();
    }
}

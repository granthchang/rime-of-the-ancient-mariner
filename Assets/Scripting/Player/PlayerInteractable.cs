using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    GameObject beamStart;
    private bool canLight;
    private bool showingToolTip;
    public bool canTurnWheel;
    public Light candleLight;

    // Start is called before the first frame update
    void Start()
    {
        beamStart = GameObject.FindGameObjectWithTag("BeamStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, beamStart.transform.position) < 3)
            canLight = true;
        else
            canLight = false;

        if (canLight && !showingToolTip)
        {
            UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.lightCandle);
            showingToolTip = true;
        }
        else if (showingToolTip && !canLight)
        {
            UI_ToolTip.main.hideToolTip();
            showingToolTip = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canLight)
        {
            candleLight.enabled = true;
            beamStart.GetComponent<ReflectionScript>().enabled = true;
            beamStart.GetComponent<AudioSource>().Play();
            canLight = false;
            UI_ToolTip.main.hideToolTip();
        }
    }
}

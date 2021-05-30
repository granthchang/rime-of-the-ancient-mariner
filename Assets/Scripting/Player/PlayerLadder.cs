using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadder : MonoBehaviour
{
    GameObject[] ladders;
    GameObject climbableLadder;
    Ladder ladderScript;
    ReflectionScript rfs;
    bool canClimb = false;
    bool showingToolTip = false;

    // Start is called before the first frame update
    void Start()
    {
        ladders = GameObject.FindGameObjectsWithTag("Ladder");
        rfs = GameObject.FindGameObjectWithTag("BeamStart2").GetComponent<ReflectionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!showingToolTip && canClimb) {
            UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.climbLadder);
            showingToolTip = true;
        } else if (showingToolTip && !canClimb) {
            UI_ToolTip.main.hideToolTip();
            showingToolTip = false;
		}

        if (Input.GetKeyDown(KeyCode.Space) && canClimb)
        {
            if(rfs.isLadderUnlocked)
            {
                BlackoutWhiteoutScript.main.blackout();
                ladderScript.GetComponent<AudioSource>().Play();
                StartCoroutine(climbLadder());
            }
            else
            {
                showingToolTip = true;
                UI_ToolTip.main.hideToolTip();
                UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.blockedLadder);
            }
        }
        foreach (GameObject ladder in ladders)
        {
            if(Vector3.Distance(ladder.transform.position, transform.position) < 2)
            {
                climbableLadder = ladder;
                canClimb = true;
                ladderScript = climbableLadder.GetComponent<Ladder>();
            }
            else
            {
                canClimb = false; 
            }
        }
    }

    IEnumerator climbLadder() {
        yield return new WaitForSeconds(1f);

        BlackoutWhiteoutScript.main.fade();

        if (Vector3.Distance(transform.position, ladderScript.waypoints[0].transform.position) > Vector3.Distance(transform.position, ladderScript.waypoints[1].transform.position))
            transform.position = ladderScript.waypoints[0].transform.position;
        else
            transform.position = ladderScript.waypoints[1].transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ToolTip : MonoBehaviour
{
    public static UI_ToolTip main;
    private void Awake() { main = this; }


    private Text textBox;
    public enum ToolTipType {inspect, moveObject, rotateObject, climbLadder, lightCandle, blockedLadder, takeBottle, captureEnemy, throwBottle}

	
    // Sets references at the beginning of the scene
	void Start()
    {
        EventController.main.onPause += hideToolTip;

        textBox = this.GetComponent<Text>();
    }

    // Shows a specific tool tip depending on what's being interacted with
    public void showToolTip(ToolTipType type) {
        switch (type) {
            case ToolTipType.inspect:
                textBox.text = "Press SPACE to inspect";
                break;
            case ToolTipType.moveObject:
                textBox.text = "Hold LMB to push/pull";
                break;
            case ToolTipType.rotateObject:
                textBox.text = "Hold Q or E to rotate";
                break;
            case ToolTipType.climbLadder:
                textBox.text = "Press SPACE to climb";
                break;
            case ToolTipType.blockedLadder:
                textBox.text = "The way out is locked";
                break;
            case ToolTipType.lightCandle:
                textBox.text = "Press SPACE to light candle";
                break;
            case ToolTipType.takeBottle:
                textBox.text = "Press SPACE to take the bottle";
                break;
            case ToolTipType.captureEnemy:
                textBox.text = "Press SPACE to capture the enemy";
                break;
            case ToolTipType.throwBottle:
                textBox.text = "Press SPACE to throw the bottle";
                break;
        }
        textBox.enabled = true;
	}

    // Hides tool tip
    public void hideToolTip() {
		if(textBox.enabled == true) {
            textBox.enabled = false;
        }
	}
}

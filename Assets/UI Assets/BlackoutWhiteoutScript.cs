using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutWhiteoutScript : MonoBehaviour
{
    public static BlackoutWhiteoutScript main;

    void Start()
    {
        main = this;
        try {
            EventController.main.onGameOver += blackout;
            EventController.main.onWin += whiteout;
            EventController.main.onRestart += blackout;
            EventController.main.onMainMenu += blackout;
        } catch (Exception e) { Debug.Log("No EventController object in scene"); }

    }

    public void blackout() {
        this.GetComponent<Image>().color = Color.black;
        this.GetComponent<Animator>().Play("Fade In");
	}

    public void whiteout() {
        this.GetComponent<Image>().color = Color.white;
        this.GetComponent<Animator>().Play("Fade In");
	}

    public void fade() {
        this.GetComponent<Animator>().Play("Fade Out");
    }
}

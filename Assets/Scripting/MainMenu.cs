using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	private GameObject overlay, title, newG, loadG, options, quit;

	private void Start() {
		Time.timeScale = 1f;
		overlay = GameObject.Find("Darkening Overlay");
		title = GameObject.Find("Title");
		newG = GameObject.Find("New Game");
		loadG = GameObject.Find("Load Game");
		options = GameObject.Find("Help and Options");
		quit = GameObject.Find("Quit");
	}

	public void newGame() {
		BlackoutWhiteoutScript.main.blackout();
		PlayerPrefs.DeleteKey("Current Checkpoint");
		StartCoroutine(waitToLoadScene("Scene2 1"));
	}

	public void loadGame() {
		BlackoutWhiteoutScript.main.blackout();
		StartCoroutine(waitToLoadScene("Scene2 1"));
	}

	IEnumerator waitToLoadScene(string s) {
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene(s);
	}

	public void quitGame() {
		Application.Quit();
	}


	public void hideMainMenu() {
		title.SetActive(false);
		newG.SetActive(false);
		loadG.SetActive(false);
		options.SetActive(false);
		quit.SetActive(false);

	}

	public void showMainMenu() {
		title.SetActive(true);
		newG.SetActive(true);
		loadG.SetActive(true);
		options.SetActive(true);
		quit.SetActive(true);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CaptainsLog : MonoBehaviour
{
    public static UI_CaptainsLog main;
	private void Awake() { main = this; }

	private GameObject textObject;

	// Sets references at the beginning of the scene
	private void Start() {
		textObject = this.transform.Find("Log Text").gameObject;
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject child = this.transform.GetChild(i).gameObject;
			child.SetActive(false);
		}
	}

	// Shows a captain's log with the given text
	public void showLog(string str) {
		textObject.GetComponent<Text>().text = str;
		PauseOverlay.main.blurScreen();
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject child = this.transform.GetChild(i).gameObject;
			child.SetActive(true);
			child.GetComponent<Animator>().Play("Fade in");
		}
		StartCoroutine(waitToPause());
/*		Cursor.lockState = CursorLockMode.None;
*/	}

	IEnumerator waitToPause() {
		yield return new WaitForSeconds(0.1f);
		Time.timeScale = 0f;
	}

	// Hides captain's log
	public void hideLog() {
		textObject.GetComponent<Text>().text = "";
		PauseOverlay.main.unblurScreen();
		for (int i = 0; i < this.transform.childCount; i++) {
			GameObject child = this.transform.GetChild(i).gameObject;
			child.GetComponent<Animator>().Play("Fade out");
		}
		Time.timeScale = 1f;
/*		Cursor.lockState = CursorLockMode.Locked;
*/	}
}

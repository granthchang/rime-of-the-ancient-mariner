using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    // Show cursor
    private void Start() {
        StartCoroutine(waitForCursor());
    }

    // Wait 1 second and then show cursor
    private IEnumerator waitForCursor() {
        yield return new WaitForSeconds(1f);
        Cursor.lockState = CursorLockMode.None;
    }

    // Public call to go to main menu
    public void mainMenu() {
        StartCoroutine(waitToMenu());
	}

    // Wait 1 second and then load main menu
    private IEnumerator waitToMenu() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main Menu");
    }

    // Public call to restart checkpoint
    public void restartCheckpoint() {
        StartCoroutine(waitToRestart());
    }

    // Wait 1 second and then load game scene
    private IEnumerator waitToRestart() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scene2 1");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{
    public static EventController main;
    private void Awake() { main = this; }

    [HideInInspector] public bool paused, running;
    public Transform[] checkpoints;

    public GameObject enemy1, enemy2;

	private void Start() {
        running = true;

        // Move player to the specific checkpoint
        if (GameObject.Find("Player") != null) {
            PlayerMovement_Location.main.transform.position = checkpoints[PlayerPrefs.GetInt("Current Checkpoint")].position;
            Debug.Log("Placed player at checkpoint " + PlayerPrefs.GetInt("Current Checkpoint"));

            if (PlayerPrefs.GetInt("Current Checkpoint") == 3) {
                PlayerMovement_Location.main.transform.Rotate(0, 150, 0);
            }
        }
    }

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused)
                pauseGame();
            else
                resumeGame();
        }

        // Enable the proper enemy
        if (PlayerPrefs.GetInt("Current Checkpoint") >= 2) {
            try {
                enemy1.SetActive(false);
                enemy2.SetActive(true);
            } catch (Exception e) { }
        } else {
            try {
                enemy1.SetActive(true);
                enemy2.SetActive(false);
            } catch (Exception e) { }
        }
            
    }

    // Lost the game
    public event Action onGameOver;
    public void gameOver() {
        onGameOver?.Invoke();
        StartCoroutine(waitToLoadScene("Death Screen"));
    }

/*    // When the first door is successfully unlocked
    private bool isDoor1Open;
    public event Action onDoor1Open;
    public void openDoor1() {
        if (!isDoor1Open) {
            onDoor1Open?.Invoke();
        }
    }*/

    // Won the game
    public event Action onWin;
    public void winGame() {
        onWin?.Invoke();
        StartCoroutine(waitToLoadScene("Win Screen"));
    }

    // Pauses the game
    public event Action onPause;
    public void pauseGame() {
        StartCoroutine(waitToPause());
        paused = true;
        onPause?.Invoke();
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator waitToPause() {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
    }

    // Resumes the game
    public event Action onResume;
    public void resumeGame() {
        Time.timeScale = 1f;
        paused = false;
        onResume?.Invoke();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Reloads scene
    public event Action onRestart;
    public void restartCheckpoint() {
        Time.timeScale = 1f;
        StartCoroutine(waitToLoadScene("Scene2 1"));
        onRestart?.Invoke();
    }

    // Redirects to main menu
    public event Action onMainMenu;
    public void mainMenu() {
        Time.timeScale = 1f;
        StartCoroutine(waitToLoadScene("Main Menu"));
        onMainMenu?.Invoke();
    }


    // Waits half a second before loading screen
    // Gives enough time for blackout
    IEnumerator waitToLoadScene(string s) {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(s);
    }

    // Updates volume
    public event Action onUpdateVolume;
    public void updateVolume() {
        onUpdateVolume?.Invoke();
	}


    // Quit the game
    public void quitGame() {
        Application.Quit();
	}
}

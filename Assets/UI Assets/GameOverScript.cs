using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public static GameOverScript main;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
        text = this.GetComponent<Text>();
        text.raycastTarget = false;
    }

    public void showLoseText() {
        text.text = "YOU WERE CAUGHT";
        text.raycastTarget = true;
        this.GetComponent<Animator>().Play("Fade In");
        Cursor.lockState = CursorLockMode.None;
    }

    public void showWinText() {
        text.text = "YOU ESCAPED";
        text.color = Color.black;
        text.raycastTarget = true;
        this.GetComponent<Animator>().Play("Fade In");
        Cursor.lockState = CursorLockMode.None;
    }

    public void restart() {
        this.GetComponent<Animator>().Play("Fade Out");
        StartCoroutine(waitToRestart());
    }

    IEnumerator waitToRestart() {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Scene2 1");
	}
}

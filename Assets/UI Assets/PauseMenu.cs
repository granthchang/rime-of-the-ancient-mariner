using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventController.main.onPause += fadeInPauseMenu;
        EventController.main.onResume += fadeOutPauseMenu;

        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void showPauseMenu() {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void fadeInPauseMenu() {
        for(int i = 0; i < this.transform.childCount; i++) {
            GameObject child = this.transform.GetChild(i).gameObject;
            child.SetActive(true);
            child.GetComponent<Animator>().Play("Fade in");
            child.GetComponent<Button>().interactable = true;
		}
    }

    public void hidePauseMenu() {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void fadeOutPauseMenu() {
        for (int i = 0; i < this.transform.childCount; i++) {
            GameObject child = this.transform.GetChild(i).gameObject;
            child.GetComponent<Animator>().Play("Fade out");
            child.GetComponent<Button>().interactable = false;
        }
    }
}

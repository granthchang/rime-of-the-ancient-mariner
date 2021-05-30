using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOverlay : MonoBehaviour
{
    public static PauseOverlay main;
    private void Awake() { main = this; }

    private Material mat;
    private Image image;
    private float timeToBlur = 0.1f;
    private float timer;
    private bool blurring, unblurring;

    // Start is called before the first frame update
    void Start()
    {
        image = this.GetComponent<Image>();
        mat = Instantiate(image.material);
        mat.SetFloat("_Size", 0);
        image.material = mat;

        timer = 0;
        blurring = unblurring = false;

        EventController.main.onPause += blurScreen;
        EventController.main.onResume += unblurScreen;
    }

    // Update is called once per frame
    void Update()
    {
        // End timer if animatino has finished
        if (timer > timeToBlur) {
            blurring = unblurring = false;
		}

        // Increment timer and increase/decrease blur
        if (blurring) {
            timer += Time.deltaTime;
            float percent = timer / timeToBlur;

            mat.SetFloat("_Size", Mathf.Lerp(0, 2, percent));
            image.material = mat;
            
		} else if (unblurring) {
            timer += Time.deltaTime;
            float percent = timer / timeToBlur;

            mat.SetFloat("_Size", Mathf.Lerp(2, 0, percent));
            image.material = mat;
        }
    }

    public void blurScreen() {
        blurring = true;
        unblurring = false;
        timer = 0;
    }

    public void unblurScreen() {
        unblurring = true;
        blurring = false;
        timer = 0;
    }
}

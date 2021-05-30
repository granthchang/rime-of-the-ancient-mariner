using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DynamicOverlay : MonoBehaviour
{
    public float totalTimeToKill = 5f;
    public float remainingTime = 0;
    float alphaValue = 0;
    private AudioSource audio;
    private Material screenMaterial;
    public GameObject enemy1, enemy2;
    Enemy_LOS enemyLosScript1;
    Enemy_LOS enemyLosScript2;

    // Start is called before the first frame update
    void Start()
    {
        screenMaterial = GetComponent<Image>().material;

        enemyLosScript1 = enemy1.GetComponent<Enemy_LOS>();
        enemyLosScript2 = enemy2.GetComponent<Enemy_LOS>();

        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (enemy1 == null || enemy2 == null) {
            alphaValue = 0;
        }
        else if (enemyLosScript1.isDetectingPlayer || enemyLosScript2.isDetectingPlayer) {
            if (!audio.isPlaying)
                audio.Play();
            remainingTime += Time.deltaTime;
            if (remainingTime > totalTimeToKill) {
                remainingTime = totalTimeToKill;
                EventController.main.gameOver();
            }
            alphaValue = remainingTime / totalTimeToKill;
        }
        else if (!enemyLosScript1.isDetectingPlayer || !enemyLosScript2.isDetectingPlayer) {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
                remainingTime = 0;
            alphaValue = remainingTime / totalTimeToKill;
        }

        Color color = screenMaterial.color;
        color.a = Mathf.Clamp(alphaValue, 0, 1);
        screenMaterial.color = color;
    }
}

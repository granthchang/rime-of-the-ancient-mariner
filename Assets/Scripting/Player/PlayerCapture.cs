using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    public static PlayerCapture main;
	private void Awake() { main = this; }

    public enum CapturePhase { none, hasBottle, hasCaught }
    public CapturePhase currentPhase;
    public Material skyDay;
    private bool showingTooltip;
    private GameObject[] enemies;

    [SerializeField] private float rangeToCapture = 3f;
    [SerializeField] private Material caughtMat;
    private GameObject bottle;

    public Enemy_LOS enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currentPhase = CapturePhase.none;
        bottle = this.transform.Find("Player Camera").Find("Bottle").gameObject;

        if (PlayerPrefs.GetInt("Current Checkpoint") >= 3) {
            currentPhase = CapturePhase.hasBottle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPhase) {
            
            // Bottle phase
            case CapturePhase.hasBottle:

                bottle.SetActive(true);

                float distFromEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
                
                // Tooltip
                if (!showingTooltip && distFromEnemy <= rangeToCapture && !enemy.isDetectingPlayer) {
                    UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.captureEnemy);
                    showingTooltip = true;
                } else if (showingTooltip && (distFromEnemy > rangeToCapture || enemy.isDetectingPlayer)) {
                    UI_ToolTip.main.hideToolTip();
                    showingTooltip = false;
                }

                // Capturing
                if (showingTooltip && Input.GetKeyDown(KeyCode.Space)) {
                    this.GetComponent<Heartbeat>().stopHeartbeat();
                    this.GetComponent<AudioSource>().volume = 0;
                    bottle.GetComponent<AudioSource>().Play();
                    BlackoutWhiteoutScript.main.blackout();
                    StartCoroutine(waitToCapture());
                    StartCoroutine(waitToWin());
                }
                break;

            /*// Caught phase
            case CapturePhase.hasCaught:
                
                // Tooltip
                if (!showingTooltip && Mathf.Abs(this.transform.position.x) > 9) {
                    UI_ToolTip.main.showToolTip(UI_ToolTip.ToolTipType.throwBottle);
                    showingTooltip = true;
                } else if (showingTooltip && Mathf.Abs(this.transform.position.x) <= 9) {
                    UI_ToolTip.main.hideToolTip();
                    showingTooltip = false;
                }

                // Throwing bottle
                if (showingTooltip && Input.GetKeyDown(KeyCode.Space)) {
                    UI_ToolTip.main.hideToolTip();
                    showingTooltip = false;
					StartCoroutine(waitToWin());
					bottle.transform.SetParent(null);
                    bottle.GetComponent<Rigidbody>().isKinematic = false;
                    bottle.GetComponent<BoxCollider>().enabled = true;
                    bottle.GetComponent<Rigidbody>().AddForce(this.transform.forward * 200);
                    
                    UI_ToolTip.main.hideToolTip();
                    showingTooltip = false;
                    currentPhase = CapturePhase.none;
                }
                break;*/
            default:
                break;
        }
    }

	// Things to change during the fade
	IEnumerator waitToCapture() {
        yield return new WaitForSeconds(0.5f);
        foreach (GameObject enemy in enemies) {
            GameObject.Destroy(enemy);
        }
        Destroy(GameObject.Find("Fog"));
        RenderSettings.skybox = skyDay;
        BlackoutWhiteoutScript.main.fade();
        bottle.transform.Find("BottleMesh").GetComponent<MeshRenderer>().material = caughtMat;
        UI_ToolTip.main.hideToolTip();
        showingTooltip = false;
        currentPhase = CapturePhase.hasCaught;
    }

    // Win the game after 1 second
    IEnumerator waitToWin() {
        yield return new WaitForSeconds(5f);
        EventController.main.winGame();
    }
}

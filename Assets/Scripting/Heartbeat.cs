using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    [SerializeField] [Tooltip("When the enemy is closest")] private float beatVolume = 1f;
    [SerializeField] [Tooltip("Default volume (settings)")] private float defaultVolume = 1f;

    [SerializeField] private float minDistanceForAwareness = 2f;
    [SerializeField] [Tooltip("How close the enemy must be for the heart beat to get louder")] private float maxDistanceForAwareness = 15f;

    [SerializeField] private float timeBetweenBeats = 1f;

    private AudioSource audio;
    public Transform enemy1, enemy2;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        StartCoroutine(waitForBeat());

        EventController.main.onGameOver += stopHeartbeat;
        EventController.main.onWin += stopHeartbeat;

        setVolume();
        EventController.main.onUpdateVolume += setVolume;
    }

    // Called at physics ticks (hopefully to increase performance)
    void FixedUpdate()
    {
        float distFromEnemy;
        if (PlayerPrefs.GetInt("Current Checkpoint") < 2) {
            distFromEnemy = Vector3.Distance(this.transform.position, enemy1.position);
        }
        else {
            distFromEnemy = Vector3.Distance(this.transform.position, enemy2.position);
		}
        

        if (distFromEnemy >= maxDistanceForAwareness) {
            audio.volume = 0;
		}
        else if (distFromEnemy <= minDistanceForAwareness)
        {
            audio.volume = beatVolume;
		}
        else
        {
            float t = (distFromEnemy - minDistanceForAwareness) / (maxDistanceForAwareness - minDistanceForAwareness);
            audio.volume = Mathf.Lerp(beatVolume, 0, t);
		}
    }

    // Repeating heartbeat
    IEnumerator waitForBeat() {
        yield return new WaitForSeconds(timeBetweenBeats);
        audio.Play();
        StartCoroutine(waitForBeat());
	}

    // Stops heartbeat from repeating
    public void stopHeartbeat() {
        StopCoroutine(waitForBeat());
        this.enabled = false;
    }

    public void setVolume() {
        switch (PlayerPrefs.GetInt("Sound FX")) {
            case 1:
                beatVolume = 0; break;
            case 2:
                beatVolume= Mathf.Lerp(0, defaultVolume, 0.25f); break;
            case 3:
                beatVolume = Mathf.Lerp(0, defaultVolume, 0.5f); break;
            case 4:
                beatVolume = Mathf.Lerp(0, defaultVolume, 0.75f); break;
            case 5:
                beatVolume = defaultVolume; break;
            default:
                beatVolume = defaultVolume; break;
        }
    }
}

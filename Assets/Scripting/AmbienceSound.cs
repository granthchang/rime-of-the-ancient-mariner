using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public float defaultVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        setVolume();

        EventController.main.onUpdateVolume += setVolume;
    }

    public void setVolume() {
		switch(PlayerPrefs.GetInt("Ambience")) {
            case 1:
                this.GetComponent<AudioSource>().volume = 0;
                break;
            case 2:
                this.GetComponent<AudioSource>().volume = Mathf.Lerp(0, defaultVolume, 0.25f);
                break;
            case 3:
                this.GetComponent<AudioSource>().volume = Mathf.Lerp(0, defaultVolume, 0.5f);
                break;
            case 4:
                this.GetComponent<AudioSource>().volume = Mathf.Lerp(0, defaultVolume, 0.75f);
                break;
            case 5:
                this.GetComponent<AudioSource>().volume = defaultVolume;
                break;
            default:
                this.GetComponent<AudioSource>().volume = defaultVolume;
                break;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private Text[] sensitivity;
    private Text[] soundFX;
    private Text[] ambience;

    public Color active, inactive;

    private void Start() {
        // Set sensitivity buttons
        Transform sensitivityObj = this.transform.Find("Sensitivity");
        sensitivity = new Text[5];
        for (int i = 0; i < 5; i++) {
            sensitivity[i] = sensitivityObj.GetChild(i).GetComponent<Text>();
		}

        // Set sound fx buttons
        Transform soundFXObj = this.transform.Find("Sound Effects");
        soundFX = new Text[5];
        for (int i = 0; i < 5; i++) {
            soundFX[i] = soundFXObj.GetChild(i).GetComponent<Text>();
        }

        // Set ambience buttons
        Transform ambienceObj = this.transform.Find("Ambience");
        ambience = new Text[5];
        for (int i = 0; i < 5; i++) {
            ambience[i] = ambienceObj.GetChild(i).GetComponent<Text>();
        }

        hideSettingsMenu();
        EventController.main.onResume += hideSettingsMenu;

        refreshSettings();
    }

	public void showSettingsMenu() {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
        this.GetComponent<Image>().enabled = true;
    }

    public void hideSettingsMenu() {
        for (int i = 0; i < this.transform.childCount; i++) {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.GetComponent<Image>().enabled = false;
    }

    public void setSensitivity(int n) {
        switch(n) {
            case 1:
                PlayerPrefs.SetInt("Sensitivity", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("Sensitivity", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("Sensitivity", 3);
                break;
            case 4:
                PlayerPrefs.SetInt("Sensitivity", 4);
                break;
            case 5:
                PlayerPrefs.SetInt("Sensitivity", 5);
                break;
            default:
                Debug.LogError("Invalid sensitivity");
                break;
        }
	}

    public void setSoundFX(int n) {
        switch (n) {
            case 1:
                PlayerPrefs.SetInt("Sound FX", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("Sound FX", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("Sound FX", 3);
                break;
            case 4:
                PlayerPrefs.SetInt("Sound FX", 4);
                break;
            case 5:
                PlayerPrefs.SetInt("Sound FX", 5);
                break;
            default:
                Debug.LogError("Invalid sound FX level");
                break;
        }
        EventController.main.updateVolume();
    }

    public void setAmbience(int n) {
        switch (n) {
            case 1:
                PlayerPrefs.SetInt("Ambience", 1);
                break;
            case 2:
                PlayerPrefs.SetInt("Ambience", 2);
                break;
            case 3:
                PlayerPrefs.SetInt("Ambience", 3);
                break;
            case 4:
                PlayerPrefs.SetInt("Ambience", 4);
                break;
            case 5:
                PlayerPrefs.SetInt("Ambience", 5);
                break;
            default:
                Debug.LogError("Invalid ambience level");
                break;
        }
        EventController.main.updateVolume();

    }

    // Color current settings
    public void refreshSettings() {
        // Sensitivity
        if (!PlayerPrefs.HasKey("Sensitivity")) {
            for (int i = 0; i < 5; i++) {
                if (i == 2) {
                    sensitivity[i].color = active;
                } else {
                    sensitivity[i].color = inactive;
				}
            }
        } else {
            for (int i = 0; i < 5; i++) {
                if (i == PlayerPrefs.GetInt("Sensitivity") - 1) {
                    sensitivity[i].color = active;
                } else {
                    sensitivity[i].color = inactive;
				}
            }
        }

        // soundFX
        if (!PlayerPrefs.HasKey("Sound FX")) {
            for (int i = 0; i < 5; i++) {
                if (i == 4) {
                    soundFX[i].color = active;
                } else {
                    soundFX[i].color = inactive;
                }
            }
        } else {
            for (int i = 0; i < 5; i++) {
                if (i == PlayerPrefs.GetInt("Sound FX") - 1) {
                    soundFX[i].color = active;
                } else {
                    soundFX[i].color = inactive;
                }
            }
        }

        // ambience
        if (!PlayerPrefs.HasKey("Ambience")) {
            for (int i = 0; i < 5; i++) {
                if (i == 4) {
                    ambience[i].color = active;
                } else {
                    ambience[i].color = inactive;
                }
            }
        } else {
            for (int i = 0; i < 5; i++) {
                if (i == PlayerPrefs.GetInt("Ambience") - 1) {
                    ambience[i].color = active;
                } else {
                    ambience[i].color = inactive;
                }
            }
        }
    }
}

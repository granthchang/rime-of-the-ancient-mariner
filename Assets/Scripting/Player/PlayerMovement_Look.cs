using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerMovement_Look : MonoBehaviour
{
    public static PlayerMovement_Look main;
    private void Awake() { main = this; }

    private Transform playerTransform;
    private float yRotation;

    [Header("Settings")]
    public float sensitivity = 200f;

	// Sets references at the beginning of the scene
	void Start()
    {
        playerTransform = this.transform.parent.transform;

        // Hides cursor
        Cursor.lockState = CursorLockMode.Locked;

        setSensitivity();
    }

    // Movement input
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
		transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
		playerTransform.Rotate(Vector3.up * mouseX);
    }

    public void setSensitivity() {
        if (PlayerPrefs.HasKey("Sensitivity"))
            sensitivity = PlayerPrefs.GetInt("Sensitivity") * 40;
        else
            sensitivity = 120;
    }
}

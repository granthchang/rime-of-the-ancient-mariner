using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerMovement_Location : MonoBehaviour
{
    // Singleton object
    public static PlayerMovement_Location main;
    private void Awake() { main = this; }

    // References
    private CharacterController charCon;
    private Animator cameraAnimator;
    public Transform pushableObject;

    [HideInInspector] public Vector3 movement;

    // Movement
    [Header("Movement")]
    public float speed = 10f;
    public float crouchSpeedMultiplier = 0.25f;
    public float pushingSpeedMultiplier = 0.25f;

    // Crouching
    [Header("Crouching")]
    public float normalHeight;
    public float crouchHeight;
    private bool isCrouching;

    [HideInInspector] public bool isPushingObject = false;
    private Vector3 pushDirection;

    // Gravity
    [Header("Gravity")]
    public float gravityScale = 1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private Vector3 velocity;
    private bool grounded;
    private float groundDist = 0.4f;

    // Sets references at beginning of scene
    void Start()
    {
        charCon = this.GetComponent<CharacterController>();
        charCon.height = normalHeight;
        cameraAnimator = this.transform.Find("Player Camera").GetComponent<Animator>();
    }

    // Input
    void Update() {
        // Determine whether pushing an object or not
        if (Input.GetMouseButtonDown(0) && pushableObject != null) {
            startPushing();
		} else if (Input.GetMouseButtonUp(0) && pushableObject != null) {
            startFreeMovement();
		}

        // Process movement
        if (!isPushingObject) {
            freeMovement();
        } else {
            pushMovement();
        }
        gravityMovement();
    }

    // Gravity movement
    private void gravityMovement() {
        grounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);

        if (grounded && velocity.y < 0)
            velocity.y = 0f;

        velocity.y += gravityScale * -20 * Time.deltaTime;
        charCon.Move(velocity * Time.deltaTime);
    }

    // Set up variables to start free movement
    private void startFreeMovement() {
        isPushingObject = false;
        PlayerMovement_Look.main.setSensitivity();
        pushableObject.GetComponent<Movable>().stopSound();
    }


    // Set up variables to start pushing
    private void startPushing() {
        isPushingObject = true;
        pushDirection = this.transform.forward;
        pushDirection.y = 0;
        PlayerMovement_Look.main.sensitivity = 0;
	}

    // Free movement not pushing an object
    private void freeMovement() {
        // Main movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        movement = this.transform.right * x + transform.forward * z;
        charCon.Move(movement * speed * Time.deltaTime);

        // Crouching
        if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.LeftShift))) {
            if (!isCrouching) {
                charCon.height = crouchHeight;
                charCon.center = new Vector3(0, -0.425f, 0);
                speed *= crouchSpeedMultiplier;
                cameraAnimator.Play("Player Crouch", 0);
                isCrouching = true;
            } else {
                charCon.height = normalHeight;
                charCon.center = Vector3.zero;
                speed *= (1 / crouchSpeedMultiplier);
                cameraAnimator.Play("Player Stand", 0);
                isCrouching = false;
            }
        }
    }

    // Movement when pushing an object
    private void pushMovement() {
        float z = Input.GetAxis("Vertical"); ;

		movement = pushDirection * z * pushingSpeedMultiplier;
        charCon.Move(movement * speed * Time.deltaTime);
        if (z != 0) {
            pushableObject.GetComponent<Movable>().playSound();
		} else
			pushableObject.GetComponent<Movable>().stopSound();
	}

    // Kicking objects on the ground
    private void OnControllerColliderHit(ControllerColliderHit hit) {

        Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
			return;

        // Kick objects on the ground
		if (body.tag == "Kickable") {
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.1f, hit.moveDirection.z);
			body.velocity = pushDir * 5;
			body.angularVelocity = new Vector3(Random.Range(0, 6), Random.Range(0, 6), Random.Range(0, 6));
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerObj;
    private Rigidbody rb;
    [SerializeField] private Transform gunObject;

    [Header("Slider")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYscale;
    private float startYscale;

    private float scaleFactor = 0.5f;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private bool desliza;

    private void Start() {
        rb = GetComponent<Rigidbody>();

        startYscale = playerObj.localScale.y;
    }

    private void Update() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && !desliza) {
            StartSlide();
        }
    }

    private void FixedUpdate() {
        if (desliza) SlidingMovement();
    }

    private void StartSlide() {
        desliza = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, playerObj.localScale.y * scaleFactor, playerObj.localScale.z);
        gunObject.localScale = new Vector3(gunObject.localScale.x, gunObject.localScale.y / scaleFactor, gunObject.localScale.z);
        gunObject.position = new Vector3(gunObject.position.x, gunObject.position.y - 10f, gunObject.position.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement() {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0) StopSlide();
        
    }

    private void StopSlide() {
        desliza = false;
        
        playerObj.localScale = new Vector3(playerObj.localScale.x, playerObj.localScale.y / scaleFactor, playerObj.localScale.z);
        gunObject.localScale = new Vector3(gunObject.localScale.x, gunObject.localScale.y * scaleFactor, gunObject.localScale.z);
        gunObject.position = new Vector3(gunObject.position.x, gunObject.position.y + 10f, gunObject.position.z);
    }
}

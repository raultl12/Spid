using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] private float speed;

    [SerializeField] private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void HandleInput(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer(){
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * speed, ForceMode.Force);
    }

    private void Start(){
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update(){
        HandleInput();
    }

    private void FixedUpdate(){
        MovePlayer();
    }


}

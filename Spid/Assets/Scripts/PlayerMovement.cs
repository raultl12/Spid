using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float groundDrag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool readyToJump;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool grounded;

    [SerializeField] private Transform orientation;

    private Vector3 moveDirection;

    private Rigidbody rb;

    PlayerInputActions playerActions;

    private void Awake(){
        rb = GetComponent<Rigidbody>();

        //Crear el input
        playerActions = new PlayerInputActions();
        //Activar el mapa de entrada de jugador
        playerActions.Player.Enable();

        //Subscripciones a eventos
        //Subscripcion al evento de saltar
        playerActions.Player.Jump.performed += Jump;
    }

    private void Start(){
        
        rb.freezeRotation = true;
        readyToJump = true;
    }


    private void Update(){
        //Check for ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsGround);

        SpeedControl();

        //Handle drag
        if(grounded){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }
    }

    private void FixedUpdate(){
        MovePlayer();
    }

    //Tomando el valor del input system se aplica la fuerza de movimiento
    private void MovePlayer(){
        Vector2 inputVector = playerActions.Player.Movement.ReadValue<Vector2>();
        moveDirection = orientation.forward * inputVector.y + orientation.right * inputVector.x;

        if(grounded){
            rb.AddForce(moveDirection.normalized * speed * 10.0f, ForceMode.Force);
        }
        else if(!grounded){
            rb.AddForce(moveDirection.normalized * speed * 10.0f * airMultiplier, ForceMode.Force);
        }
    }

    //Limita la velocidad del jugador al valor de speed
    //Para que no pueda ir mas rapido
    //Evita que al saltar se envale el jugador
    private void SpeedControl(){
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        //Limit vel
        if(flatVel.magnitude > speed){
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    //Aplica la fuerza de salto al personaje
    private void Jump(InputAction.CallbackContext context){
        if(readyToJump && grounded){
            readyToJump = false;

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    //Resetea el salto
    private void ResetJump(){
        readyToJump = true;
    }
}

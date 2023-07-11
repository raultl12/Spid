using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //Hay que hacer que la sensibilidad sea diferente para mando y para raton.
    //Para mando --> 60
    //Raton --> 10
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Transform orientation;

    [SerializeField] private float rotationX;
    [SerializeField] private float rotationY;

    private PlayerInputActions playerActions;

    private void Awake(){
        playerActions = new PlayerInputActions();
        playerActions.Player.Enable();
    }

    private void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update(){
        Vector2 inputVector = playerActions.Player.Camara.ReadValue<Vector2>();
        float mouseX = inputVector.x * sensX * Time.fixedDeltaTime;
        float mouseY = inputVector.y * sensY * Time.fixedDeltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);

        orientation.rotation = Quaternion.Euler(0, rotationY, 0);
    }
}

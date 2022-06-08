using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float HorizontalMovement;
    float VerticalMovement;
    float New_X_Position;
    float New_Y_Position;
    float X_Offset;
    float Y_Offset;

    [SerializeField] InputAction movement;
    [SerializeField] float ControlSpeed = 10f;

    void Start()
    {
        // movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Update(){
        // OLD Input Setup
        // HorizontalMovement = Input.GetAxis("Horizontal");
        // VerticalMovement = Input.GetAxis("Vertical");
        // Debug.Log(VerticalMovement);

        // New Input Setup
        HorizontalMovement = movement.ReadValue<Vector2>().x;
        VerticalMovement = movement.ReadValue<Vector2>().y;

        X_Offset = HorizontalMovement * Time.deltaTime * ControlSpeed;
        Y_Offset = VerticalMovement * Time.deltaTime * ControlSpeed;

        New_X_Position = transform.localPosition.x + X_Offset;
        New_Y_Position = transform.localPosition.y + Y_Offset;

        transform.localPosition = new Vector3(New_X_Position, New_Y_Position, transform.localPosition.z);
    }

    private void OnEnable(){
        movement.Enable();
    }

    private void OnDisable(){
        movement.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float HorizontalMovement;
    float VerticalMovement;
    
    float Raw_X_Pos;
    float Raw_Y_Pos;
    float X_Offset;
    float Y_Offset;
    float Clamped_X_Pos;
    float Clamped_Y_Pos;

    float Pitch;
    float Yaw;
    float Roll;

    [SerializeField] InputAction movement;
    [SerializeField] float ControlSpeed = 30f;
    [SerializeField] float Raw_X_Range = 16f;
    [SerializeField] float Raw_Y_Range = 8.5f;
    [SerializeField] float PositionPitchFactor = -2f;
    [SerializeField] float PositionYawFactor = 2f;
    [SerializeField] float ControlPitchFactor = -20f;

    void Start()
    {
        // movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    void Update(){
        PlayerControlSetup();
        PlayerRotation();
    }

    void PlayerRotation(){
        Pitch = (transform.localPosition.y * PositionPitchFactor) + (VerticalMovement * ControlPitchFactor);
        Yaw = transform.localPosition.x * PositionYawFactor + HorizontalMovement;
        transform.localRotation = Quaternion.Euler(Pitch, Yaw, Roll);
    }

    void PlayerControlSetup(){
        // OLD Input Setup
        // HorizontalMovement = Input.GetAxis("Horizontal");
        // VerticalMovement = Input.GetAxis("Vertical");
        // Debug.Log(VerticalMovement);

        // New Input Setup
        HorizontalMovement = movement.ReadValue<Vector2>().x;
        VerticalMovement = movement.ReadValue<Vector2>().y;

        X_Offset = HorizontalMovement * Time.deltaTime * ControlSpeed;
        Y_Offset = VerticalMovement * Time.deltaTime * ControlSpeed;

        Raw_X_Pos = transform.localPosition.x + X_Offset;
        Raw_Y_Pos = transform.localPosition.y + Y_Offset;

        Clamped_X_Pos = Mathf.Clamp(Raw_X_Pos, -Raw_X_Range, Raw_X_Range);
        Clamped_Y_Pos = Mathf.Clamp(Raw_Y_Pos, -Raw_Y_Range, Raw_Y_Range);

        transform.localPosition = new Vector3(Clamped_X_Pos, Clamped_Y_Pos, transform.localPosition.z);
    }

    private void OnEnable(){
        movement.Enable();
    }

    private void OnDisable(){
        movement.Disable();
    }
}

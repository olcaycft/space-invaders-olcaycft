using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementSystem : MonoBehaviour
{
    private JoystickController playerInput;
    
    private CharacterController _controller;
    private CharacterController controller=>_controller=GetComponent<CharacterController>();
    
    private float playerSpeed = 5f;
    public static event Action playerShooting;
    private void Awake()
    {
        playerInput = new JoystickController();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
    
    


    void Update()
    {
        Vector2 movementInput = playerInput.PlayerMain.SideMovement.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            var rot = gameObject.transform.rotation;
            rot.y = 0;
            gameObject.transform.rotation = rot;
        }

        if (playerInput.PlayerMain.Shooting.triggered)
        {
            playerShooting?.Invoke();
        }
        
    }
}
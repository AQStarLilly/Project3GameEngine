using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, GameInput.IPlayerActions
{
    private GameInput gameInput;

    void Awake()
    {
        gameInput = new GameInput();
        gameInput.Player.Enable();

        gameInput.Player.SetCallbacks(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            Debug.Log("Movement input : " + movement);
            Actions.MoveEvent?.Invoke(movement);
        }
        else if (context.canceled)
        {
            Debug.Log("Movement stopped.");
            Actions.MoveEvent?.Invoke(Vector2.zero);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Actions.SprintEvent?.Invoke(true);
            Debug.Log("Started sprinting.");
        }
        else if (context.canceled)
        {
            Actions.SprintEvent?.Invoke(false);
            Debug.Log("Stopped sprinting.");
        }
    }

    // Clean up on disable or destroy
    void OnDisable()
    {
        if (gameInput != null)
        {
            gameInput.Player.Disable(); // Disable the Player action map
        }
    }

    void OnDestroy()
    {
        if (gameInput != null)
        {
            gameInput.Player.Disable(); // Extra safety for cleanup
        }
    }

}


public static class Actions
{
    public static Action<Vector2> MoveEvent;
    public static Action<bool> SprintEvent;
}

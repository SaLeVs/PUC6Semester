using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static PlayerControls;

public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> OnShootEvent;
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnAimEvent;

    private PlayerControls playerControls;
    

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Player.SetCallbacks(this);
        }

        playerControls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        OnAimEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        // Logic for Handle shoot, is a interaction on Input action
        if (context.started)
        {
            OnShootEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnShootEvent?.Invoke(false);
        }
    }


    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.Player.Disable();
            playerControls.Player.SetCallbacks(null);
            // playerControls.Dispose();
            playerControls = null;

        }
    }
    
}

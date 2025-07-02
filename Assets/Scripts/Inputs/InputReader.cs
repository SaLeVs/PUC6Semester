using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControls;

public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> OnShootEvent;
    public event Action<Vector3> OnMoveEvent;
    public event Action<Vector3> OnAimEvent;

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

    public void OnAim(InputAction.CallbackContext context)
    {
        OnAimEvent?.Invoke(context.ReadValue<Vector3>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector3>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnShootEvent?.Invoke(true);
        }
        else if(context.canceled)
        {
            OnShootEvent?.Invoke(false);
        }
    }

    private void OnDestroy()
    {
        if (playerControls != null)
        {
            playerControls.Player.Disable();
            playerControls.Player.SetCallbacks(null);
            playerControls = null;
        }
    }
}

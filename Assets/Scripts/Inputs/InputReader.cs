using System;
using UnityEngine;
using UnityEngine.InputSystem;
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

    public void OnAim(InputAction.CallbackContext context)
    {
        OnAimEvent?.Invoke(context.ReadValue<Vector2>());

        if(context.performed)
        {
            Debug.Log("Shoot performed");
            OnShootEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            Debug.Log("Shoot canceled");
            OnShootEvent?.Invoke(false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        OnMoveEvent?.Invoke(context.ReadValue<Vector2>());
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

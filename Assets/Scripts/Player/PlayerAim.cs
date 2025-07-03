using Sortify;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAim : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Rigidbody rb;

    [Space(10)]

    [BetterHeader("Settings")]
    [SerializeField] private float aimSpeed;


    private Vector2 aimInput;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        inputReader.OnAimEvent += PlayerInputs_OnAimEvent;

    }


    private void PlayerInputs_OnAimEvent(Vector2 aimInput)
    {
        this.aimInput = aimInput;

    }

    private void FixedUpdate()
    {
        if (!IsOwner) return; 

        if(aimInput != Vector2.zero)
        {
            Vector3 aimDirection = new Vector3(0f, aimInput.x, 0f);

            float targetYaw = Mathf.Atan2(aimInput.x, aimInput.y) * Mathf.Rad2Deg;
            // Debug.Log($"targetYaw: {targetYaw}, InputX: {previousAimInput.x}, InputY: {previousAimInput.y} MathfRad2Deg: {Mathf.Rad2Deg}");

            Quaternion targetRotation = Quaternion.Euler(0f, targetYaw, 0f);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, aimSpeed * Time.fixedDeltaTime));
        }
    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        inputReader.OnAimEvent -= PlayerInputs_OnAimEvent;

    }

}

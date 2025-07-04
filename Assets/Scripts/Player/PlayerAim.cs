using Sortify;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAim : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private Rigidbody rb;

    [Space(10)]

    [BetterHeader("Settings")]
    [SerializeField] private float aimSpeed;


    private Vector2 aimInput;
    private Vector2 serverAimInput;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        inputReader.OnAimEvent += PlayerInputs_OnAimEvent;

    }


    private void PlayerInputs_OnAimEvent(Vector2 aimInput)
    {
        this.aimInput = aimInput;
        SendInputToServerRpc(this.aimInput);

    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void SendInputToServerRpc(Vector2 aimInput)
    {
        serverAimInput = aimInput;

    }

    private void FixedUpdate()
    {
        if (!IsServer) return; 

        if (serverAimInput != Vector2.zero)
        {
            Vector3 aimDirection = new Vector3(0f, serverAimInput.x, 0f);
            float targetYaw = Mathf.Atan2(serverAimInput.x, serverAimInput.y) * Mathf.Rad2Deg;
            // Debug.Log($"targetYaw: {targetYaw}, InputX: {serverAimInput.x}, InputY: {serverAimInput.y} MathfRad2Deg: {Mathf.Rad2Deg}");

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

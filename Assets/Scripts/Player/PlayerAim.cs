using Sortify;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerAim : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReader playerInputs;

    [Space(10)]

    [BetterHeader("Settings")]
    [SerializeField] private float aimSpeed;


    private Vector2 previousAimInput;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        playerInputs.OnAimEvent += PlayerInputs_OnAimEvent;

    }


    private void PlayerInputs_OnAimEvent(Vector2 aimInput)
    {
        previousAimInput = aimInput;

    }

    private void Update()
    {
        if(previousAimInput != Vector2.zero)
        {
            Vector3 aimDir = new Vector3(0f, previousAimInput.x, 0f);

            float targetYaw = Mathf.Atan2(previousAimInput.x, previousAimInput.y) * Mathf.Rad2Deg;
            // Debug.Log($"targetYaw: {targetYaw}, InputX: {previousAimInput.x}, InputY: {previousAimInput.y} MathfRad2Deg: {Mathf.Rad2Deg}");

            Quaternion targetRotation = Quaternion.Euler(0f, targetYaw, 0f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, aimSpeed * Time.deltaTime);
        }
    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        playerInputs.OnAimEvent -= PlayerInputs_OnAimEvent;

    }

}

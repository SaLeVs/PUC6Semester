using Sortify;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerAim : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReader playerInputs;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;

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

    private void FixedUpdate()
    {

    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        playerInputs.OnAimEvent -= PlayerInputs_OnAimEvent;

    }

}

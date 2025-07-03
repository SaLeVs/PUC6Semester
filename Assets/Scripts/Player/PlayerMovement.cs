using Sortify;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReader playerInputs;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody playerRigidbody;

    [Space(10)]

    [BetterHeader("Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float aimSpeed;


    private Vector2 previousMovementInput;
    private Vector2 previousAimInput;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        playerInputs.OnMoveEvent += PlayerInputs_OnMoveEvent;
        playerInputs.OnAimEvent += PlayerInputs_OnAimEvent;

    }


    private void PlayerInputs_OnMoveEvent(Vector2 movementInput)
    {
        previousMovementInput = movementInput;

        playerTransform.position += new Vector3(previousMovementInput.x, 0f, previousMovementInput.y) * Time.deltaTime;

    }

    private void PlayerInputs_OnAimEvent(Vector2 aimInput)
    {
        previousAimInput = aimInput;

        float zRotation = previousAimInput.x * aimSpeed * Time.deltaTime;
        playerTransform.Rotate(0f, zRotation, 0f) ;

    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        playerInputs.OnMoveEvent -= PlayerInputs_OnMoveEvent;
        playerInputs.OnAimEvent -= PlayerInputs_OnAimEvent;

    }
}

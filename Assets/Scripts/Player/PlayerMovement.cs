using Sortify;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Rigidbody rb;

    [Space(10)]

    [BetterHeader("Settings")]
    [SerializeField] private float movementSpeed;


    private Vector2 movementInput;
    private Vector2 serverMovementInput;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        inputReader.OnMoveEvent += PlayerInputs_OnMoveEvent;

    }


    private void PlayerInputs_OnMoveEvent(Vector2 movementInput)
    {
        this.movementInput = movementInput.normalized;
        SendInputToServerRpc(this.movementInput);

    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void SendInputToServerRpc(Vector2 movementInput)
    {
        serverMovementInput = movementInput;

    }

    private void FixedUpdate()
    {
        if (!IsServer) return;

        Vector3 moveDirection = new Vector3(serverMovementInput.x, 0f, serverMovementInput.y);
        rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * moveDirection);

    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        inputReader.OnMoveEvent -= PlayerInputs_OnMoveEvent;

    }
}

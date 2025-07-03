using Sortify;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [BetterHeader("References")]
    [SerializeField] private InputReader playerInputs;

    [Space(10)]

    [BetterHeader("Settings")]
    [SerializeField] private float movementSpeed;


    private Vector2 previousMovementInput;


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        playerInputs.OnMoveEvent += PlayerInputs_OnMoveEvent;

    }


    private void PlayerInputs_OnMoveEvent(Vector2 movementInput)
    {
        previousMovementInput = movementInput.normalized;
    }


    private void Update()
    {
        Vector3 moveDirection = new Vector3(previousMovementInput.x, 0f, previousMovementInput.y);
        transform.position += movementSpeed * Time.deltaTime * moveDirection;
    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        playerInputs.OnMoveEvent -= PlayerInputs_OnMoveEvent;

    }
}

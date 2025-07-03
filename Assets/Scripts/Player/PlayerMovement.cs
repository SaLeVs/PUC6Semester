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


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        inputReader.OnMoveEvent += PlayerInputs_OnMoveEvent;

    }


    private void PlayerInputs_OnMoveEvent(Vector2 movementInput)
    {
        this.movementInput = movementInput.normalized;
    }


    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y);
        rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * moveDirection);
    }


    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        inputReader.OnMoveEvent -= PlayerInputs_OnMoveEvent;

    }
}

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    private bool isShooting;
    private void Start()
    {
        inputReader.OnMoveEvent += InputReader_Move;
        inputReader.OnShootEvent += InputReader_Shoot;
        inputReader.OnAimEvent += InputReader_Aim;
    }

    private void InputReader_Aim(Vector2 aim)
    {
        Debug.Log($"Aim event: {aim}");
    }

    private void InputReader_Shoot(bool isShoot)
    {
        if (isShoot)
        {
            isShooting = true;
            //Debug.Log("Shooting started");
        }
        else
        {
            isShooting = false;
            //Debug.Log("Shooting stopped");
        }
    }

    private void InputReader_Move(Vector2 movement)
    {
        Debug.Log($"Move event: {movement}");
    }

    private void OnDestroy()
    {
        inputReader.OnMoveEvent -= InputReader_Move;
        inputReader.OnShootEvent -= InputReader_Shoot;
        inputReader.OnAimEvent -= InputReader_Aim;
    }
}

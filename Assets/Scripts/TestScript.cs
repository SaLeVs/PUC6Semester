using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;


    private void Start()
    {
        inputReader.OnMoveEvent += InputReader_Move;
    }

    private void InputReader_Move(Vector2 movement)
    {
        Debug.Log($"Move event triggered with vector: {movement}");
    }

    private void OnDestroy()
    {
        inputReader.OnMoveEvent -= InputReader_Move;
    }
}

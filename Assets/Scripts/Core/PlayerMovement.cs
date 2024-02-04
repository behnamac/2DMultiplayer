using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("Refrences")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform body;
    private Rigidbody2D rb;
    private Vector2 previousMovementInput;


    [Header("Setting")]
    [SerializeField] private float speed = 3f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }
        inputReader.MovementEvent += HandleMovment;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }
        inputReader.MovementEvent -= HandleMovment;
    }


    private void HandleMovment(Vector2 position)
    {

    }




}

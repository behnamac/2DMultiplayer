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
    [SerializeField] private float turnRate = 30f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        TankRotation();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        TankForwardMoving();
    }

    private void TankRotation()
    {
        float zRotation = previousMovementInput.x * -turnRate * Time.deltaTime;
        body.Rotate(0, 0, zRotation);
    }

    private void TankForwardMoving()
    {
        rb.velocity = (Vector2)body.up * previousMovementInput.y * speed;
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


    private void HandleMovment(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }




}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAim : NetworkBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform turrentTransform;
    private Camera cam;

    public override void OnNetworkSpawn()
    {
        inputReader.AimEvent += getAimPosition;
    }

    public override void OnNetworkDespawn()
    {
        inputReader.AimEvent -= getAimPosition;
    }

    private void Start()
    {
        cam = Camera.main;
    }





    private void getAimPosition(Vector2 position)
    {        
            if (!IsOwner) return;
            var aimScreenPosition = cam.ScreenToWorldPoint(position);
            turrentTransform.up = new Vector2(aimScreenPosition.x - turrentTransform.position.x, aimScreenPosition.y - turrentTransform.position.y);        
    }
}

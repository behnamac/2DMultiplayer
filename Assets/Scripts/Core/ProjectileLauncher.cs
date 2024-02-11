using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ProjectileLauncher : NetworkBehaviour
{
    [Header("Refrence")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject serverProjectile;
    [SerializeField] private GameObject clientProjectile;

    [Header("Setting")]
    [SerializeField] private float speed = 5;


    private bool isFire;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        _inputReader.PrimaryFireEvent += HandlePrimaryFire;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;
        _inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }

    private void HandlePrimaryFire(bool isFire)
    {
        this.isFire = isFire;
    }

    private void Update()
    {
        if (!isFire) return;
        if (!IsOwner) return;
        SpawnProjectileServerRpc(spawnPoint.position,spawnPoint.up);
        SpawnDummyProjectile(spawnPoint.position, spawnPoint.up);
    }

    [ServerRpc]
    private void SpawnProjectileServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(serverProjectile, spawnPos, Quaternion.identity);
        projectileInstance.transform.up = direction;
        SpawnProjectileClientRpc(spawnPos, direction);
        Debug.Log("I am Server Projectile");
    }

    [ClientRpc]
    private void SpawnProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {
        if (IsOwner) return;
        SpawnDummyProjectile(spawnPos, direction);
        Debug.Log("I am Client Projectile");

    }

    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(projectile, spawnPos, Quaternion.identity);
        projectileInstance.transform.up = direction;
        Debug.Log("I am Dummy Projectile");

    }

}

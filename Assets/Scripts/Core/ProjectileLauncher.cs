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
    [SerializeField] private GameObject serverProjectile;
    [SerializeField] private GameObject clientProjectile;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private Collider2D playerCollider;

    [Header("Setting")]
    [SerializeField] private float fireSpeed = 5;
    [SerializeField] private float muzzleFlashDuration;
    [SerializeField] private float fireRate;


    private float previousFireTime;
    private bool isFire;
    private float muzzleFlashTimer;

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
        if (muzzleFlashTimer > 0f)
        {
            muzzleFlashTimer -= Time.deltaTime;
            if (muzzleFlashTimer <= 0)
            {
                muzzleFlash.SetActive(false);
            }
        }
        if (!isFire) return;
        if (!IsOwner) return;

        if(Time.time<(1/fireRate)+previousFireTime) { return; }
        previousFireTime = Time.time;   

        SpawnProjectileServerRpc(spawnPoint.position, spawnPoint.up);
        SpawnDummyProjectile(spawnPoint.position, spawnPoint.up);
    }

    [ServerRpc]
    private void SpawnProjectileServerRpc(Vector3 spawnPos, Vector3 direction)
    {
        GameObject projectileInstance = Instantiate(serverProjectile, spawnPos, Quaternion.identity);
        projectileInstance.transform.up = direction;

        if (projectileInstance.TryGetComponent(out DealDamageOnConnect dealDamage))
        {
            dealDamage.SetOwner(OwnerClientId);
        }

        if (projectileInstance.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * fireSpeed;
        }

        SpawnProjectileClientRpc(spawnPos, direction);
    }

    [ClientRpc]
    private void SpawnProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
    {
        if (IsOwner) return;
        SpawnDummyProjectile(spawnPos, direction);

    }

    private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
    {
        muzzleFlash.SetActive(true);
        muzzleFlashTimer = muzzleFlashDuration;
        GameObject projectileInstance = Instantiate(clientProjectile, spawnPos, Quaternion.identity);
        projectileInstance.transform.up = direction;
        Physics2D.IgnoreCollision(playerCollider, projectileInstance.GetComponent<Collider2D>());
        if (projectileInstance.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = rb.transform.up * fireSpeed;
        }

    }

}

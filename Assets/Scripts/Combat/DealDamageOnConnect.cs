using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DealDamageOnConnect : MonoBehaviour
{
    [SerializeField] private int damage = 5;

    private ulong ownerClientID;

    public void SetOwner(ulong ownerClientID)
    {
        this.ownerClientID = ownerClientID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.attachedRigidbody) return;
        if (collision.attachedRigidbody.TryGetComponent(out NetworkObject netObj))
        {
            if (ownerClientID == netObj.OwnerClientId) return;
        }

        if (collision.gameObject.TryGetComponent(out Health enemy))
        {
            enemy.TakeDamage(damage);
        }
    }
}

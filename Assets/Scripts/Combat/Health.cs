using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [field: SerializeField] public int maxHealth { get; private set; } = 100;
    public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>();
    private bool isDead;
    public Action<Health> OnDie;


    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        CurrentHealth.Value = maxHealth;
    }

    public override void OnNetworkDespawn()
    {

    }

    public void TakeDamage(int damage)
    {
        ModifyHealth(-damage);
    }
    private void RestoreHealth(int healValue)
    {
        ModifyHealth(healValue);
    }
    private void ModifyHealth(int health)
    {
        if (isDead) return; 
        CurrentHealth.Value += health;  
        CurrentHealth.Value=Mathf.Clamp(CurrentHealth.Value, 0, maxHealth);
        if (CurrentHealth.Value > 0) return;
        isDead = true;
        OnDie?.Invoke(this);
    }

}

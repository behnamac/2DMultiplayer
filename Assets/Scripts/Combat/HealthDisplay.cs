using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : NetworkBehaviour
{
    [SerializeField] private Image healthBarImg;
    [SerializeField] private Health health;

    public override void OnNetworkSpawn()
    {
        if (!IsClient) return;
        if (!health)
            health = GetComponent<Health>();
        health.CurrentHealth.OnValueChanged += HandleHealthChanged;
        HandleHealthChanged(0, health.CurrentHealth.Value);
    }
    public override void OnNetworkDespawn()
    {
        if (!IsClient) return;
        health.CurrentHealth.OnValueChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(int oldHealth, int newHealth)
    {
        float _health = (float)newHealth / health.maxHealth;
        healthBarImg.fillAmount = _health;
    }




}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinWallet : NetworkBehaviour
{
    public NetworkVariable<int> TotalCoin = new NetworkVariable<int>();


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            var coinValue = coin.Collect();
            if (!IsServer) return;
            TotalCoin.Value += coinValue;
        }
    }

}

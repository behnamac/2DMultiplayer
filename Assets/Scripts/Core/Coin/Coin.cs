using Unity.Netcode;
using UnityEngine;

public abstract class Coin : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer coinRnderer;

    protected int coinValue=10;
    protected bool alreadyCollected;

    public abstract int Collect();

    public void SetValue(int value)
    {
        coinValue = value;  
    }

    protected void Show(bool show)
    {
        coinRnderer.enabled = show;
    }


}

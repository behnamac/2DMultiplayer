using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinServer : MonoBehaviour
{
    [SerializeField] private Button joinBtn;
    [SerializeField] private Button hostBtn;

    private void Start()
    {
        joinBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Join();
        });
        hostBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            Host();
        });
    }

    public void Join()
    {
        NetworkManager.Singleton.StartClient();
        print("join");
    } 
    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        print("host");
    }
}

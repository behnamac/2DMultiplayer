using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controllers;

[CreateAssetMenu(fileName = "NewInput", menuName = "Input", order = 2)]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> PrimaryFireEvent;
    public event Action<Vector2> MovementEvent;

    private Controllers control;


    private void OnEnable()
    {
        if (control == null)
        {
            control = new Controllers();
            control.Player.SetCallbacks(this);
        }
        control.Player.Enable();
    }



    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            PrimaryFireEvent?.Invoke(true);
        else
            PrimaryFireEvent?.Invoke(false);    
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    [SerializeField] private InputReader _inputGetter;

    private void OnEnable()
    {
        _inputGetter.MovementEvent += Move;
        _inputGetter.PrimaryFireEvent += Fire;
    }

    private void Fire(bool obj)
    {
        Debug.Log(obj);
    }

    private void OnDestroy()
    {
        _inputGetter.MovementEvent += Move;

    }

    private void Move(Vector2 value)
    {
        Debug.Log(value);
    }


}

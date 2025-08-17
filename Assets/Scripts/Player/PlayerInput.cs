using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Key Settings")]
    [SerializeField] private KeyCode switchToolKey;
    [SerializeField] private KeyCode useToolKey;

    public bool PressedSwithKey()
    {
        return Input.GetKeyDown(switchToolKey);
    }
    public bool PressedUseKey()
    {
        return Input.GetKeyDown(useToolKey);
    }
}

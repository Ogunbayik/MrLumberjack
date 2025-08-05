using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour, ICarryable
{
    public void Carry()
    {
        Debug.Log("Player is carrying this log");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    private FlatbedController flatbed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<FlatbedController>(out FlatbedController flatbed))
        {
            this.flatbed = flatbed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlatbedController>())
            flatbed = null;
    }
    public FlatbedController GetFlatbed()
    {
        return flatbed;
    }
}

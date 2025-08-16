using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IMineable>(out IMineable mineable))
        {
            mineable.Mine(Consts.PlayerToolAmount.HIT_AMOUNT);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetFlatbedArrivedAnimation(bool isArrived)
    {
        animator.SetBool(Consts.StandAnimationParameter.FLATBED_ARRIVED, isArrived);
    }
}

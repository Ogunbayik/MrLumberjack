using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerUnlockAnimation()
    {
        animator.SetTrigger(Consts.BuildingAnimationParameter.UNLOCKED);
    }
}

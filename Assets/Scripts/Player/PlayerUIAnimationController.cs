using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        FlatbedItemHolder.Instance.OnDeliveredItem += Instance_OnDeliveredItem;
    }

    private void Instance_OnDeliveredItem(FlatbedItemHolder flatbed)
    {
        PlayReceiveAnimation();
    }

    public void PlayReceiveAnimation()
    {
        animator.SetTrigger(Consts.PlayerAnimationParameter.RECEIVE_ITEM);
    }
}

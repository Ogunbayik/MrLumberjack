using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        FlatbedItemHolder.Instance.OnFlatbedLoaded += Instance_OnFlatbedLoaded;
    }

    private void Instance_OnFlatbedLoaded()
    {
        PlayAddMoneyAnimation();
    }

    public void PlayAddMoneyAnimation()
    {
        animator.SetTrigger(Consts.MoneyAnimationParameter.ADD_MONEY);
    }
    public void PlaySpendMoneyAnimation()
    {
        animator.SetTrigger(Consts.MoneyAnimationParameter.SPEND_MONEY);
    }
}

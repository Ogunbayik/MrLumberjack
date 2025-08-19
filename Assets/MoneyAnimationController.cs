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
        AddMoneyAnimation();
    }

    public void AddMoneyAnimation()
    {
        animator.SetTrigger("AddMoney");
    }
    public void SpendMoneyAnimation()
    {
        animator.SetTrigger("SpendMoney");
    }
}

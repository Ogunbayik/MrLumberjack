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

    public void SpendMoneyAnimation()
    {
        animator.SetTrigger("SpendMoney");
    }
}

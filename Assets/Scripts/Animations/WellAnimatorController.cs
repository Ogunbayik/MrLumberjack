using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellAnimatorController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}

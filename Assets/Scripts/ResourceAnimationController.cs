using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceAnimationController : MonoBehaviour
{
    public event Action OnDeadAnimationComplete;
    public event Action OnRespawnAnimationComplete;

    private GatherableResource gatherableResource;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gatherableResource = GetComponent<GatherableResource>();
    }
    private void OnEnable()
    {
        gatherableResource.OnHit += GatherableResource_OnHit;
        gatherableResource.OnDead += GatherableResource_OnDead;
        gatherableResource.OnRespawn += GatherableResource_OnRespawn;
    }
    private void OnDisable()
    {
        gatherableResource.OnHit -= GatherableResource_OnHit;
        gatherableResource.OnDead -= GatherableResource_OnDead;
        gatherableResource.OnRespawn -= GatherableResource_OnRespawn;
    }
    private void GatherableResource_OnRespawn()
    {
        PlayRespawnAnimation();
    }
    private void GatherableResource_OnHit()
    {
        PlayHitAnimation();
    }
    private void GatherableResource_OnDead()
    {
        PlayDeadAnimation();
    }
    private void PlayRespawnAnimation()
    {
        animator.SetTrigger(Consts.ResourceAnimationParameter.RESPAWN);
    }
    private void PlayHitAnimation()
    {
        animator.SetTrigger(Consts.ResourceAnimationParameter.HIT);
    }
    private void PlayDeadAnimation()
    {
        animator.SetTrigger(Consts.ResourceAnimationParameter.DEAD);
    }
    public void CompleteRespawnAnimation()
    {
        OnRespawnAnimationComplete?.Invoke();
    }
    public void CompleteDeadAnimation()
    {
        OnDeadAnimationComplete?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatbedUnloadedState : IFlatbedState
{
    private FlatbedItemHolder itemHolder;

    private float cooldownTimer;
    public void EnterState(FlatbedController flatbed)
    {
        itemHolder = flatbed.GetComponent<FlatbedItemHolder>();
        cooldownTimer = flatbed.movementCooldown;

        flatbed.transform.position = flatbed.spawnPosition.position;
        flatbed.SetMovementPosition(flatbed.standPosition);
        
        itemHolder.InitializeItem();
    }

    public void ExitState(FlatbedController flatbed)
    {

    }

    public void UpdateState(FlatbedController flatbed)
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
            flatbed.SetState(new FlatbedAcceleratingState());
    }

}

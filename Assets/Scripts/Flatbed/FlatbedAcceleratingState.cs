using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatbedAcceleratingState : IFlatbedState
{
    public void EnterState(FlatbedController flatbed)
    {

    }

    public void ExitState(FlatbedController flatbed)
    {

    }

    public void UpdateState(FlatbedController flatbed)
    {
        flatbed.currentSpeed += Time.deltaTime;
        if (flatbed.currentSpeed >= flatbed.maximumSpeed)
            flatbed.currentSpeed = flatbed.maximumSpeed;

        var distanceBetweenMovementPosition = Vector3.Distance(flatbed.transform.position, flatbed.movementPosition.position);
        if (distanceBetweenMovementPosition <= flatbed.deceleratingDistance)
            flatbed.SetState(new FlatbedDeceleratingState());

        flatbed.Movement();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatbedDeceleratingState : IFlatbedState
{
    public void EnterState(FlatbedController flatbed)
    {

    }

    public void ExitState(FlatbedController flatbed)
    {

    }

    public void UpdateState(FlatbedController flatbed)
    {
        var deceleratingMultiply = 2f;
        flatbed.currentSpeed -= deceleratingMultiply * Time.deltaTime;

        if(flatbed.currentSpeed <= 0)
        {
            flatbed.currentSpeed = 0;
            flatbed.SetState(new FlatbedLoadingState());
        }

        flatbed.Movement();
    }
}

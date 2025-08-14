using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatbedLoadingState : IFlatbedState
{
    public void EnterState(FlatbedController flatbed)
    {
        
    }

    public void ExitState(FlatbedController flatbed)
    {

    }

    public void UpdateState(FlatbedController flatbed)
    {
        if (flatbed.isDelivered)
            flatbed.SetState(new FlatbedAcceleratingState());
    }
}

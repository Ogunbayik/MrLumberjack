using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatbedLoadedState : IFlatbedState
{
    private FlatbedItemHolder itemHolder;
    public void EnterState(FlatbedController flatbed)
    {
        itemHolder = flatbed.GetComponent<FlatbedItemHolder>();
    }

    public void ExitState(FlatbedController flatbed)
    {

    }

    public void UpdateState(FlatbedController flatbed)
    {
        if (itemHolder.IsLoaded())
            flatbed.SetState(new FlatbedAcceleratingState());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlatbedState
{
    public void EnterState(FlatbedController flatbed);
    public void UpdateState(FlatbedController flatbed);
    public void ExitState(FlatbedController flatbed);
}

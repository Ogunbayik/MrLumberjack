using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : GatherableResource, IChopable
{
    public void Chop(int damageAmount)
    {
        base.OnHitResource(damageAmount);
    }
}

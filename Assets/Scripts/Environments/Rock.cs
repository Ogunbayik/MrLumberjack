using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : GatherableResource, IMineable
{
    public void Mine(int damageAmount)
    {
        base.OnHitResource(damageAmount);
    }

}

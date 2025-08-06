using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour, ICarryable
{
    public void Carry(PlayerCarryController player, Transform carryPosition)
    {
        var carryList = player.GetCarryList();
        var distanceBetweenLog = 5f;
        var maximumCount = player.GetMaximumCarryCount();

        if (carryList.Count < maximumCount)
        {
            carryList.Add(this.gameObject);
            transform.SetParent(carryPosition);
            var desiredPosition = (float)carryList.Count / distanceBetweenLog;

            transform.position = new Vector3(carryPosition.position.x, carryPosition.position.y + desiredPosition, carryPosition.position.z);
            transform.rotation = carryPosition.transform.rotation;
        }
    }

}

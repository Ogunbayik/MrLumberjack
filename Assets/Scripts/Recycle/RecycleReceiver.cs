using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleReceiver : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float maxRecycleTime;

    private float recycleTimer;

    void Start()
    {
        recycleTimer = maxRecycleTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerCarryController>(out PlayerCarryController controller))
        {
            RecyclePlayerItems(controller);
        }
    }
    private void RecyclePlayerItems(PlayerCarryController controller)
    {
        recycleTimer -= Time.deltaTime;

        if (recycleTimer <= 0)
        {
            controller.DestroyLastListObject();
            controller.UpdateCarryingStatus();
            controller.ResetCarriedObjectName();
            ResetRecycleTimer();
        }
    }
    private void ResetRecycleTimer()
    {
        recycleTimer = maxRecycleTime;
    }
}

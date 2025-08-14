using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatbedController : MonoBehaviour
{
    public Dictionary<string, int> requiredItemList = new Dictionary<string, int>();

    private IFlatbedState currentState;
    [Header("Visual Settings")]
    public GameObject cargoVisual;
    public GameObject[] strapsVisual;
    [Header("Item Settings")]
    public string requiredItemName;
    public int minRequiredItemCount;
    public int maxRequiredItemCount;
    public int itemMoney;
    [Header("Distance Settings")]
    public float deceleratingDistance;
    [Header("Movement Settings")]
    public float maximumSpeed;
    [Header("UI Settings")]
    public Sprite materialSprite;

    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public bool isLoaded;
    [HideInInspector]
    public int requiredCountItem;
    [HideInInspector]
    public Transform standPosition;
    [HideInInspector]
    public Transform exitPosition;
    [HideInInspector]
    public Transform movementPosition;
    [HideInInspector]
    public bool isDelivered = false;
    [HideInInspector]
    public int currentCount;
    private void Awake()
    {
        standPosition = GameObject.Find(Consts.FlatbedMovementPositions.STAND_POSITION).transform;
        exitPosition = GameObject.Find(Consts.FlatbedMovementPositions.EXIT_POSITION).transform;
    }
    private void Start()
    {
        InitializeFlatbed();
    }
    private void InitializeFlatbed()
    {
        movementPosition = standPosition;

        currentState = new FlatbedAcceleratingState();
        currentState.EnterState(this);

        requiredCountItem = Random.Range(minRequiredItemCount, maxRequiredItemCount);
        currentCount = requiredCountItem;

        if (!requiredItemList.ContainsKey(requiredItemName))
            requiredItemList.Add(requiredItemName, requiredCountItem);
        
        HasTruckDelivered();
    }

    public void SetState(IFlatbedState newState)
    {
        if(currentState != null)
        {
            currentState.ExitState(this);
        }

        currentState = newState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }
    public void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movementPosition.position, currentSpeed * Time.deltaTime);
    }
    private void ActivateLoadedVisual(bool isLoaded)
    {
        cargoVisual.SetActive(isLoaded);

        foreach (var strap in strapsVisual)
        {
            strap.SetActive(isLoaded);
        }
    }
    public bool CanBeTaken(PlayerCarryController player)
    {
        if (player.IsCarrying() && requiredItemList.ContainsKey(player.GetCarriedObjectName()))
            return true;
        else
            return false;
    }

    public void DecreaseRequiredItemCount()
    {
        if (requiredCountItem > 0)
        {
            currentCount--;
            requiredItemList[requiredItemName] = currentCount;
        }
        else
        {
            currentCount = 0;
            requiredItemList.Clear();
        }
    }

    public bool HasTruckDelivered()
    {
        if (currentCount == requiredCountItem)
        {
            Debug.Log("Kamyona hiç eþya yüklenmedi");
            ActivateLoadedVisual(false);
            isDelivered = false;
        }
        else if(currentCount == 0)
        {
            Debug.Log("Kamyona tüm eþyalar yüklenmiþtir.");
            isDelivered = true;
        }
        else
        {
            Debug.Log("Kamyona eþyalar yüklenmiþtir ancak hepsi deðil");
            ActivateLoadedVisual(true);
            isDelivered = false;
        }

        return isDelivered;
    }

    public void SetMovementPosition(Transform position)
    {
        movementPosition = position;
    }

}

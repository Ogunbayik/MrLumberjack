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
    public int requiredItemCount;
    [HideInInspector]
    public Transform standPosition;
    [HideInInspector]
    public Transform exitPosition;
    [HideInInspector]
    public Transform movementPosition;

    private int requiredStartCount;
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

        requiredItemCount = Random.Range(minRequiredItemCount, maxRequiredItemCount);
        requiredStartCount = requiredItemCount;

        if (!requiredItemList.ContainsKey(requiredItemName))
            requiredItemList.Add(requiredItemName, requiredItemCount);
        
        HasTruckLoaded();
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
    public bool CanBeGiven(PlayerCarryController player)
    {
        if (player.IsCarrying() && requiredItemList.ContainsKey(player.GetCarriedObjectName()))
            return true;
        else
            return false;
    }

    public void DecreaseRequiredItemCount()
    {
        if (requiredItemCount > 0)
        {
            requiredItemCount--;
            requiredItemList[requiredItemName] = requiredItemCount;
        }
    }

    public bool HasTruckLoaded()
    {
        if (requiredStartCount == requiredItemCount)
        {
            Debug.Log("Kamyona hiç eþya yüklenmedi");
            ActivateLoadedVisual(false);
            return false;
        }
        else if( requiredItemCount == 0)
        {
            Debug.Log("Kamyona tüm eþyalar yüklenmiþtir.");
            return true;
        }
        else
        {
            Debug.Log("Kamyona eþyalar yüklenmiþtir ancak hepsi deðil");
            ActivateLoadedVisual(true);
            return false;
        }
    }

    public void SetMovementPosition(Transform position)
    {
        movementPosition = position;
    }

}

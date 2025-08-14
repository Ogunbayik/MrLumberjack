using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stand : MonoBehaviour
{
    private StandReciever standReciever;

    [Header("UI Settings")]
    [SerializeField] private Image standImage;
    [SerializeField] private TextMeshProUGUI countText;

    private FlatbedController flatbed;
    private void Awake()
    {
        standReciever = GetComponentInChildren<StandReciever>();
        ShowItemCounterUI(false);
    }
    private void OnEnable()
    {
        standReciever.OnItemDelivered += StandReciever_OnItemDelivered;
    }
    private void OnDisable()
    {
        standReciever.OnItemDelivered -= StandReciever_OnItemDelivered;
    }

    private void StandReciever_OnItemDelivered(FlatbedController flatbed)
    {
        flatbed.DecreaseRequiredItemCount();
        flatbed.HasTruckDelivered();

        if (flatbed.currentCount == 0)
        {
            flatbed.SetMovementPosition(flatbed.exitPosition); 
            
            var money = flatbed.itemMoney * flatbed.requiredCountItem;
            MoneyManager.Instance.AddMoney(money);
            MoneyManager.Instance.UpdateMoneyUI();
        }

        UpdateRequiredCount(flatbed.currentCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<FlatbedController>(out FlatbedController flatbed))
        {
            this.flatbed = flatbed;
            standImage.sprite = flatbed.materialSprite;
            countText.text = flatbed.requiredCountItem.ToString();

            ShowItemCounterUI(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlatbedController>())
        {
            standImage.sprite = null;
            flatbed = null;
            ShowItemCounterUI(false);
        }
    }

    public void ShowItemCounterUI(bool isActive)
    {
        standImage.gameObject.SetActive(isActive);
        countText.gameObject.SetActive(isActive);
    }
    public void UpdateRequiredCount(int count)
    {
        countText.text = count.ToString();
    }
    public FlatbedController GetFlatbed()
    {
        return flatbed;
    }
}

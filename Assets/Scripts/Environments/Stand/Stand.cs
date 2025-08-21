using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stand : MonoBehaviour
{
    private StandUI standUI;
    private StandAnimationController standAnimator;

    private FlatbedController flatbedController;
    private FlatbedItemHolder flatbedItemHolder;

    private void Awake()
    {
        standUI = GetComponent<StandUI>();
        standAnimator = GetComponent<StandAnimationController>();
    }
    private void OnEnable()
    {
        FlatbedItemHolder.Instance.OnDeliveredItem += Instance_OnDeliveredItem;
    }
    private void OnDisable()
    {
        FlatbedItemHolder.Instance.OnDeliveredItem -= Instance_OnDeliveredItem;
    }
    private void Instance_OnDeliveredItem(FlatbedItemHolder flatbedItemHolder)
    {
        flatbedItemHolder.DecreaseRequiredItemCount();

        if (flatbedItemHolder.GetRequiredItemCount() == 0)
        {
            flatbedController.SetMovementPosition(flatbedController.exitPosition);

            var money = flatbedItemHolder.GetInitialRequiredItemCount() * flatbedItemHolder.GetRequiredItemSO().sellPrice;
            MoneyManager.Instance.AddMoney(money);
            MoneyManager.Instance.UpdatePriceText(money);
            MoneyManager.Instance.UpdateMoneyUI();
            standUI.ShowItemCounterUI(false);
            standUI.ShowNotWorkingUI(true);
            standAnimator.SetFlatbedArrivedAnimation(false);
        }

        standUI.UpdateRequiredCountText(flatbedItemHolder.GetRequiredItemCount());
        flatbedItemHolder.UpdateLoadedStatus();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<FlatbedItemHolder>(out FlatbedItemHolder itemHolder))
        {
            flatbedItemHolder = itemHolder;
            flatbedController = itemHolder.GetComponent<FlatbedController>();
            standUI.SetImageMaterial(itemHolder.GetRequiredItemSO().itemSprite);
            standUI.UpdateRequiredCountText(itemHolder.GetRequiredItemCount());

            standAnimator.SetFlatbedArrivedAnimation(true);
            standUI.ShowItemCounterUI(true);
            standUI.ShowNotWorkingUI(false);
        }
    }
    public FlatbedItemHolder GetFlatbedItemHolder()
    {
        return flatbedItemHolder;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlatbedController>())
        {
            standUI.SetImageMaterial(null);
            flatbedController = null;
            flatbedItemHolder = null;
        }
    }
}

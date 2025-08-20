using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stand : MonoBehaviour
{
    private StandReciever standReciever;
    private StandAnimationController standAnimator;

    [Header("UI Settings")]
    [SerializeField] private Image imageReceiveMaterial;
    [SerializeField] private Image imageActivate;
    [SerializeField] private Sprite notWorkingSprite;
    [SerializeField] private TextMeshProUGUI materialCountText;

    private FlatbedController flatbedController;
    private FlatbedItemHolder flatbedItemHolder;
    private void Awake()
    {
        standReciever = GetComponentInChildren<StandReciever>();
        standAnimator = GetComponent<StandAnimationController>();
    }
    private void Start()
    {
        imageActivate.sprite = notWorkingSprite;
        ShowItemCounterUI(false);
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

            var money = flatbedItemHolder.GetInitialRequiredItemCount() * flatbedItemHolder.GetRequiredItemSO().itemCost;
            MoneyManager.Instance.AddMoney(money);
            MoneyManager.Instance.UpdateAddMoneyText(money);
            MoneyManager.Instance.UpdateMoneyUI();
            ShowItemCounterUI(false);
            ShowNotWorkingUI(true);
            standAnimator.SetFlatbedArrivedAnimation(false);
        }

        UpdateRequiredCountText(flatbedItemHolder.GetRequiredItemCount());
        flatbedItemHolder.UpdateLoadedStatus();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<FlatbedItemHolder>(out FlatbedItemHolder itemHolder))
        {
            flatbedItemHolder = itemHolder;
            flatbedController = itemHolder.GetComponent<FlatbedController>();
            imageReceiveMaterial.sprite = itemHolder.GetRequiredItemSO().itemSprite;
            materialCountText.text = itemHolder.GetRequiredItemCount().ToString();

            standAnimator.SetFlatbedArrivedAnimation(true);
            ShowItemCounterUI(true);
            ShowNotWorkingUI(false);
        }
    }
    public void ShowItemCounterUI(bool isActive)
    {
        imageReceiveMaterial.gameObject.SetActive(isActive);
        materialCountText.gameObject.SetActive(isActive);
    }
    public void ShowNotWorkingUI(bool isActive)
    {
        imageActivate.gameObject.SetActive(isActive);
    }
    public void UpdateRequiredCountText(int count)
    {
        materialCountText.text = count.ToString();
    }
    public FlatbedController GetFlatbedController()
    {
        return flatbedController;
    }
    public FlatbedItemHolder GetFlatbedItemHolder()
    {
        return flatbedItemHolder;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlatbedController>())
        {
            imageReceiveMaterial.sprite = null;
            flatbedController = null;
            flatbedItemHolder = null;
        }
    }
}

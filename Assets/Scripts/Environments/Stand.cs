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

    private FlatbedController flatbed;
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
            ShowItemCounterUI(false);
            ShowNotWorkingUI(true);
            standAnimator.SetFlatbedArrivedAnimation(false);
        }

        UpdateRequiredCountText(flatbed.currentCount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<FlatbedController>(out FlatbedController flatbed))
        {
            this.flatbed = flatbed;
            imageReceiveMaterial.sprite = flatbed.materialSprite;
            materialCountText.text = flatbed.requiredCountItem.ToString();

            standAnimator.SetFlatbedArrivedAnimation(true);
            ShowItemCounterUI(true);
            ShowNotWorkingUI(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlatbedController>())
        {
            imageReceiveMaterial.sprite = null;
            flatbed = null;
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
    public FlatbedController GetFlatbed()
    {
        return flatbed;
    }
}

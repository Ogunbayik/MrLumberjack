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
        ActivateUI(false);
    }
    private void OnEnable()
    {
        standReciever.OnAllItemDelivered += StandReciever_OnAllItemDelivered;
    }

    private void StandReciever_OnAllItemDelivered(FlatbedController flatbed)
    {
        flatbed.SetMovementPosition(flatbed.exitPosition);
        flatbed.SetState(new FlatbedAcceleratingState());
        ActivateUI(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<FlatbedController>(out FlatbedController flatbed))
        {
            this.flatbed = flatbed;
            standImage.sprite = flatbed.materialSprite;
            countText.text = flatbed.requiredItemCount.ToString();

            ActivateUI(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<FlatbedController>())
        {
            standImage.sprite = null;
            flatbed = null;
        }
    }

    public void ActivateUI(bool isActive)
    {
        standImage.gameObject.SetActive(isActive);
        countText.gameObject.SetActive(isActive);
    }
    public void UpdateUI(int count)
    {
        countText.text = count.ToString();
    }
    public FlatbedController GetFlatbed()
    {
        return flatbed;
    }
}

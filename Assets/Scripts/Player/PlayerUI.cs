using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Sprite Settings")]
    [SerializeField] private Sprite noneSprite;
    [SerializeField] private Sprite axeSprite;
    [SerializeField] private Sprite pickaxeSprite;
    [Header("Image Settings")]
    [SerializeField] private Image toolImage;
    [SerializeField] private Image receiveItemImage;
    private void Start()
    {
        toolImage.sprite = noneSprite;
        receiveItemImage.gameObject.SetActive(false);
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
        UpdateReceiveItemImage(flatbedItemHolder.GetRequiredItemSO().itemSprite);
    }

    public void UpdateToolImage(Sprite sprite)
    {
        toolImage.sprite = sprite;
    }
    public void UpdateReceiveItemImage(Sprite sprite)
    {
        receiveItemImage.sprite = sprite;
    }
    public Sprite GetNoneSprite()
    {
        return noneSprite;
    }
    public Sprite GetAxeSprite()
    {
        return axeSprite;
    }
    public Sprite GetPickaxeSprite()
    {
        return pickaxeSprite;
    }
}

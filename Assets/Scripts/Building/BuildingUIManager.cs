using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUIManager : MonoBehaviour
{
    private Building building;

    [Header("UI Settings")]
    [SerializeField] private Image imageBuilding;
    [SerializeField] private Image imageItemReciever;
    [SerializeField] private Sprite spriteCoin;
    [SerializeField] private Vector3 imageRotation;
    private void Awake()
    {
        building = GetComponent<Building>();
    }
    void Start()
    {
        InitializeBuilding();
    }
    private void InitializeBuilding()
    {
        ToggleBuildingPanel(false);
        ToggleReceiverPanel(false);
        UpdateBuildingUI();
    }
    public void ToggleBuildingPanel(bool isActive)
    {
        imageBuilding.gameObject.SetActive(isActive);
    }
    public void ToggleReceiverPanel(bool isActive)
    {
        imageItemReciever.gameObject.SetActive(isActive);
    }
    private void UpdateBuildingUI()
    {
        imageBuilding.sprite = building.GetRequiredItemSO().itemSprite;
        imageBuilding.transform.rotation = Quaternion.Euler(imageRotation);
        imageItemReciever.sprite = spriteCoin;
        imageItemReciever.transform.rotation = Quaternion.Euler(imageRotation);
    }
}

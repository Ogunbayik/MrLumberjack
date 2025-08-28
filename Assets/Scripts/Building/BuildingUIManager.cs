using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUIManager : MonoBehaviour
{
    private Building building;

    [Header("Image Settings")]
    [SerializeField] private Image requiredItemImage;
    [SerializeField] private Image coinImage;
    [SerializeField] private Image produceItemImage;
    [SerializeField] private Image fillProduceImage;
    [SerializeField] private Vector3 imageRotation;
    [Header("Visual Settings")]
    [SerializeField] private GameObject workingUI;
    [Header("Sprite Settings")]
    [SerializeField] private Sprite spriteCoin;
    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI buildCostText;
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
        SetImagesSprite();
        SetImagesRotation();

        ToggleMaterialImage(false);
        ToggleMoneyImage(false);
        ToggleCostText(false);
    }
    private void Update()
    {
        ActivateDeactivateWorkingUI();
    }
    private void ActivateDeactivateWorkingUI()
    {
        if (building.IsProduce())
            ToggleWorkingUI(true);
        else
            ToggleWorkingUI(false);
    }
    public void ToggleMaterialImage(bool isActive)
    {
        requiredItemImage.gameObject.SetActive(isActive);
    }
    public void ToggleMoneyImage(bool isActive)
    {
        coinImage.gameObject.SetActive(isActive);
    }
    public void ToggleCostText(bool isActive)
    {
        buildCostText.gameObject.SetActive(isActive);
    }
    public void ToggleWorkingUI(bool isActive)
    {
        workingUI.SetActive(isActive);
    }
    public void SetRemainCost(int buildingCost, int playerMoney)
    {
        var remainCost = buildingCost - playerMoney;
        buildCostText.text = remainCost.ToString();
    }
    public void SetFillAmount(float maximumTime, float currentTime)
    {
        fillProduceImage.fillAmount = (float)currentTime / maximumTime;
    }
    private void SetImagesSprite()
    {
        requiredItemImage.sprite = building.GetRequiredItemSO().itemSprite;
        produceItemImage.sprite = building.GetProduceItemSO().itemSprite;
        coinImage.sprite = spriteCoin;
    }
    private void SetImagesRotation()
    {
        requiredItemImage.transform.rotation = Quaternion.Euler(imageRotation);
        produceItemImage.transform.rotation = Quaternion.Euler(imageRotation);
        coinImage.transform.rotation = Quaternion.Euler(imageRotation);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class BuildingUIManager : MonoBehaviour
{
    private Building building;

    [Header("UI Settings")]
    [SerializeField] private GameObject workingUI;
    [SerializeField] private Image requiredItemImage;
    [SerializeField] private Image coinImage;
    [SerializeField] private Image produceItemImage;
    [SerializeField] private Image fillProduceImage;
    [SerializeField] private Vector3 imageRotation;
    [Header("Sprite Settings")]
    [SerializeField] private Sprite spriteCoin;
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
    public void ToggleWorkingUI(bool isActive)
    {
        workingUI.SetActive(isActive);
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

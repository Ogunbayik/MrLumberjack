using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private MoneyAnimationController animationController;

    [Header("Image Settings")]
    [SerializeField] private Image coinImage;
    [Header("Sprite Settings")]
    [SerializeField] private Sprite coinSprite;
    [Header("Text Settings")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI priceText;

    public int currentMoney;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        animationController = GetComponent<MoneyAnimationController>();
    }
    private void Start()
    {
        InitializeMoneyUI();
    }
    public void InitializeMoneyUI()
    {
        coinImage.sprite = coinSprite;
        coinText.text = currentMoney.ToString();
        costText.gameObject.SetActive(false);
    }
    public bool TryToSpendMoney(int cost)
    {
        if (currentMoney >= cost)
        {
            UpdateCostText(cost);
            SpendMoney(cost);
            UpdateMoneyUI();
            animationController.PlaySpendMoneyAnimation();

            return true;
        }
        else
            return false;
    }
    public void UpdateMoneyUI()
    {
        coinText.text = currentMoney.ToString();
    }
    public void UpdatePriceText(int money)
    {
        priceText.text = "+" + money.ToString();
    }
    public void UpdateCostText(int money)
    {
        costText.text =  "-" + money.ToString();
    }
    public void AddMoney(int money)
    {
        currentMoney += money;
    }
    private void SpendMoney(int money)
    {
        currentMoney -= money;
    }
    public int GetCurrentMoney()
    {
        return currentMoney;
    }

}

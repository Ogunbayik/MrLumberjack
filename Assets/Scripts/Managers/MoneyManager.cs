using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("UI Settings")]
    [SerializeField] private Image coinImage;
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private TextMeshProUGUI coinText;
    
    private int currentMoney;
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
    }

    private void Start()
    {
        InitializeMoneyUI();
    }
    public void InitializeMoneyUI()
    {
        coinImage.sprite = coinSprite;
        currentMoney = 10;
        coinText.text = currentMoney.ToString();
    }
    public void UpdateMoneyUI()
    {
        coinText.text = currentMoney.ToString();
    }
    public void AddMoney(int money)
    {
        currentMoney += money;
    }
    public void SpendMoney(int money)
    {
        currentMoney -= money;
    }
    public int GetCurrentMoney()
    {
        return currentMoney;
    }

}

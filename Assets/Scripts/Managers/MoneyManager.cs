using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField] private Image coinImage;
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private TextMeshProUGUI coinText;
    
    private int playerMoney;
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
    }

    public void UpdateUI()
    {
        coinText.text = playerMoney.ToString();
    }

    public void AddMoney(int money)
    {
        playerMoney += money;
    }

    public void SpendMoney(int money)
    {
        playerMoney -= money;
    }

}

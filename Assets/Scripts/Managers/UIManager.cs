using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panel Settings")]
    [SerializeField] private GameObject menuUIPanel;
    [SerializeField] private GameObject moneyPanel;
    [SerializeField] private GameObject toolPanel;
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
    void Start()
    {
        InitializeUI();
        GameManager.Instance.OnClickStartButton += Instance_OnClickStartButton;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnClickStartButton -= Instance_OnClickStartButton;
        GameManager.Instance.OnGameStart -= Instance_OnGameStart;
    }
    private void InitializeUI()
    {
        ToggleMenuPanel(true);
        ToggleMoneyPanel(false);
        ToggleToolPanel(false);
    }
    private void Instance_OnClickStartButton()
    {
        ToggleMenuPanel(false);
    }
    private void Instance_OnGameStart()
    {
        ToggleMoneyPanel(true);
        ToggleToolPanel(true);
    }
    private void ToggleMenuPanel(bool isActive)
    {
        menuUIPanel.SetActive(isActive);
    }
    private void ToggleToolPanel(bool isActive)
    {
        toolPanel.SetActive(isActive);
    }
    private void ToggleMoneyPanel(bool isActive)
    {
        moneyPanel.SetActive(isActive);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnClickStartButton;

    public static GameManager Instance;
    public enum States
    {
        StartMenu,
        InGame,
        Pause
    }

    private States currentState;

    [SerializeField] private GameObject menuUIVisual;
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
        InitializeGameSettings();
    }
    private void InitializeGameSettings()
    {
        currentState = States.StartMenu;
        ShowMenuVisual(true);
    }
    private void SetStates(States newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;
    }
    public void StartGame()
    {
        OnClickStartButton?.Invoke();
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }
    private void ShowMenuVisual(bool isActive)
    {
        menuUIVisual.SetActive(isActive);
    }
}

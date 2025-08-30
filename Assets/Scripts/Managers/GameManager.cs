using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action OnClickStartButton;
    public event Action OnGameStart;

    public static GameManager Instance;

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
    private void OnEnable()
    {
        OnClickStartButton += GameManager_OnClickStartButton;
    }

    private void GameManager_OnClickStartButton()
    {
        StartCoroutine(nameof(StartGameSequence));
    }

    private IEnumerator StartGameSequence()
    {
        var cinematicDelayTimer = 3f;
        yield return new WaitForSeconds(cinematicDelayTimer);
        OnGameStart?.Invoke();
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
}

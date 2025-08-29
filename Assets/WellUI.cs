using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image recycleImage;
    [SerializeField] private Sprite recycleSprite;
    void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        recycleImage.sprite = recycleSprite;
    }
}

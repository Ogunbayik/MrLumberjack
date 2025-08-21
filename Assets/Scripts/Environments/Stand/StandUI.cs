using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StandUI : MonoBehaviour
{
    [Header("UI Settings")]
    [SerializeField] private Image imageReceiveMaterial;
    [SerializeField] private Image imageActivate;
    [SerializeField] private Sprite notWorkingSprite;
    [SerializeField] private TextMeshProUGUI materialCountText;
    void Start()
    {
        imageActivate.sprite = notWorkingSprite;
        ShowItemCounterUI(false);
    }
    public void ShowItemCounterUI(bool isActive)
    {
        imageReceiveMaterial.gameObject.SetActive(isActive);
        materialCountText.gameObject.SetActive(isActive);
    }
    public void ShowNotWorkingUI(bool isActive)
    {
        imageActivate.gameObject.SetActive(isActive);
    }
    public void UpdateRequiredCountText(int count)
    {
        materialCountText.text = count.ToString();
    }
    public void SetImageMaterial(Sprite sprite)
    {
        imageReceiveMaterial.sprite = sprite;
    }
}

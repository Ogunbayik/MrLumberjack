using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Scriptable Object/Item")]
public class ItemDataSO : ScriptableObject
{
    [Header("Item Settings")]
    public GameObject itemPrefab;
    public string itemName;
    public float intervalHorizontal;
    public float intervalVertical;
    public int sellPrice;
    [Header("UI Settings")]
    public Sprite itemSprite;
}

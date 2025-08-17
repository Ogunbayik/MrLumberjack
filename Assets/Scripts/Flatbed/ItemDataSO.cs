using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Scriptable Object/Item")]
public class ItemDataSO : ScriptableObject
{
    [Header("Item Settings")]
    public GameObject itemPrefab;
    public string itemName;
    public float itemBetweenSpace;
    public int itemCost;
    [Header("UI Settings")]
    public Sprite itemSprite;
}

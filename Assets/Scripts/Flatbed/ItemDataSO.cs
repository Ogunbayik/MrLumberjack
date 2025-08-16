using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Scriptable Object/Item")]
public class ItemDataSO : ScriptableObject
{
    [Header("Item Settings")]
    public string itemName;
    public GameObject itemPrefab;
    public float itemBetweenSpace;
    [Header("UI Settings")]
    public Sprite itemSprite;
}

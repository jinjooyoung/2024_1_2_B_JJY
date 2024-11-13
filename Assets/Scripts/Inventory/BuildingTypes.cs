using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingTypes
{
    CraftingTable,          // ���۴�
    Furnace,                // �뱤��
    Kitchen,                // �ֹ�
    Storage                 // â��
}

[System.Serializable]
public class CraftingRecipe
{
    public string itemName;             // ������ ������ �̸�
    public ItemType resultItem;         // �����
    public int resultAmount = 1;        // ����� ����
    public ItemType[] requiredItems;    // �ʿ��� ����
    public int[] requiredAmounts;       // �ʿ��� ��� ����
}
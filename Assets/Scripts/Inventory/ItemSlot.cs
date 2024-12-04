using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;        // ������ �̸�
    public TextMeshProUGUI countText;           // ������ ����
    public Button useButton;              // ��� ��ư

    private ItemType itemType;
    private int itemCount;

    public void Setup(ItemType type, int count)
    {
        itemType = type;
        itemCount = count;

        itemNameText.text = GetItemDisplayName(type);
        countText.text = count.ToString();

        useButton.onClick.AddListener(UseItem);
    }

    private string GetItemDisplayName(ItemType type)
    {
        switch(type)
        {
            case ItemType.VegetableStew: return "��ä ��Ʃ";
            case ItemType.FruitSalad: return "���� ������";
            case ItemType.RepairKit: return "���� ŰƮ";
            default: return type.ToString();
        }
    }

    private void UseItem()
    {
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        SurvivalStats stats = FindObjectOfType<SurvivalStats>();

        switch (itemType)
        {
            case ItemType.VegetableStew:
                if (inventory.RemoveItem(itemType, 1))
                {
                    stats.EatFood(40f);
                    InventoryUIManager.Instance.RefreshInventory();
                }
                break;
            case ItemType.FruitSalad:
                if (inventory.RemoveItem(itemType, 1))
                {
                    stats.EatFood(50f);
                    InventoryUIManager.Instance.RefreshInventory();
                }
                break;
            case ItemType.RepairKit:
                if (inventory.RemoveItem(itemType, 1))
                {
                    stats.RepairSuit(25f);
                    InventoryUIManager.Instance.RefreshInventory();
                }
                break;
        }
    }
}
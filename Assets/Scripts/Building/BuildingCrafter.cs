using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCrafter : MonoBehaviour
{
    public BuildingTypes buildingTypes;         // �ǹ� Ÿ��
    public CraftingRecipe[] recipes;            // ��� ������ ������ �迭
    private SurvivalStats survivalStats;        // ���� ���� ����
    private ConstructibleBuilding building;     // �ǹ� ���� ����

    void Start()
    {
        survivalStats = FindObjectOfType<SurvivalStats>();
        building = GetComponent<ConstructibleBuilding>();

        switch(buildingTypes)                               // �ǹ� Ÿ�Կ� ���� ������ ����
        {
            case BuildingTypes.Kitchen:
                recipes = RecipeList.KitchenRecipes;
                break;
            case BuildingTypes.CraftingTable:
                recipes = RecipeList.WorkbenchRecipes;
                break;
        }
    }

    public void TryCraft(CraftingRecipe recipe, PlayerInventory inventory)      // ������ ���� �õ�
    {
        if (!building.isConstructed)        // �Ǽ��� �Ϸ���� �ʾҴٸ� ���� �Ұ�
        {
            FloatingTextManager.instance?.Show("�Ǽ��� �Ϸ���� �ʾҽ��ϴ�!", transform.position + Vector3.up);
            return;
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)       // ��� üũ
        {
            if (inventory.GetItemCount(recipe.requiredItems[i]) < recipe.requiredAmounts[i])
            {
                FloatingTextManager.instance?.Show("��ᰡ �����մϴ�!", transform.position + Vector3.up);
                return;
            }
        }

        for (int i = 0; i < recipe.requiredItems.Length; i++)       // ��� �Һ�
        {
            inventory.RemoveItem(recipe.requiredItems[i], recipe.requiredAmounts[i]);
        }

        survivalStats.DamageOnCrafting();                   // ���ֺ� ������ ����

        inventory.AddItem(recipe.resultItem, recipe.resultAmount);      // ������ ����
        FloatingTextManager.instance?.Show($"{recipe.itemName} ���� �Ϸ�!", transform.position + Vector3.up);
    }
}

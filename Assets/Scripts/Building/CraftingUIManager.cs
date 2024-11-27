using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingUIManager : MonoBehaviour
{
    public static CraftingUIManager Instance { get; private set; }  // �̱��� �ν��Ͻ�

    [Header("UI References")]
    public GameObject craftingPanel;            // ���� UI �г�
    public TextMeshProUGUI buildingNameText;    // �ǹ� �̸� �ؽ�Ʈ
    public Transform recipeContainer;           // ������ ��ư���� �� �����̳�
    public Button closeButton;                  // �ݱ� ��ư
    public GameObject recipeButtonPrefabs;      // ������ ��ư ������

    private BuildingCrafter currentCrafter;     // ���� ���õ� �ǹ��� ���� �ý���

    private void Awake()
    {
        if (Instance == null) Instance = this;      // �̱��� ����
        else Destroy(gameObject);

        craftingPanel.SetActive(false);             // ���۽� UI �����
    }

    private void RefreshRecipeList()
    {
        // ���� ������ ��ư�� ����
        foreach (Transform child in recipeContainer)
        {
            Destroy(child.gameObject);
        }

        // �� ������ ��ư�� ����
        if (currentCrafter != null && currentCrafter.recipes != null)
        {
            foreach (CraftingRecipe recipe in currentCrafter.recipes)
            {
                GameObject buttonobj = Instantiate(recipeButtonPrefabs, recipeContainer);
                RecipeButton recipeButton = buttonobj.GetComponent<RecipeButton>();
                recipeButton.Setup(recipe, currentCrafter);
            }
        }
    }

    public void ShowUI(BuildingCrafter crafter)
    {
        currentCrafter = crafter;
        craftingPanel.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (crafter != null)
        {
            buildingNameText.text = crafter.GetComponent<ConstructibleBuilding>().buildingName;
            RefreshRecipeList();
        }
    }

    public void HideUI()
    {
        craftingPanel.SetActive(false);
        currentCrafter = null;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        closeButton.onClick.AddListener(() => HideUI());
    }
}

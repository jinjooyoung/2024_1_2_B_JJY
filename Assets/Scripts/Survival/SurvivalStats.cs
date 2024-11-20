using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalStats : MonoBehaviour
{
    [Header("Hunger Settings")]
    public float maxHunger = 100;               // �ִ� ��ⷮ
    public float currentHunger;                 // ���� ��ⷮ
    public float hungerDecreaseRate = 1;        // �ʴ� ��� ���ҷ�

    [Header("Space Suit Settings")]
    public float maxSuitDurability = 100;       // �ִ� ���ֺ� ������
    public float currentSuitDurability;         // ���� ���ֺ� ������
    public float harvestingDamage = 5.0f;       // ������ ���ֺ� ������
    public float craftingDamage = 3.0f;         // ���۽� ���ֺ� ������

    private bool isGameover = false;            // ���� ���� ����
    private bool isPaused = false;              // �Ͻ����� ����
    private float hungerTimer = 0;              // ��� Ÿ�̸�

    // Start is called before the first frame update
    void Start()
    {
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover || isPaused) return;

        hungerTimer += Time.deltaTime;

        if (hungerTimer >= 1.0f)
        {
            currentHunger = Mathf.Max(0, currentHunger - hungerDecreaseRate);
            hungerTimer = 0;

            CheckDeath();
        }
    }

    // ������ ������ ���ֺ� ������
    public void DamageOnHarvesting()
    {
        if (isGameover || isPaused) return;                                                     // ���� ���ᳪ �ߴ� ���¿����� �������� �ʰ�

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - harvestingDamage);         // 0�� ���Ϸ� �������� �ʱ� ����
        CheckDeath();
    }

    // ������ ���۽� ���ֺ� ������
    public void DamageOnCrafting()
    {
        if (isGameover || isPaused) return;                                                     // ���� ���ᳪ �ߴ� ���¿����� �������� �ʰ�

        currentSuitDurability = Mathf.Max(0, currentSuitDurability - craftingDamage);         // 0�� ���Ϸ� �������� �ʱ� ����
        CheckDeath();
    }

    // ���� ����� ��� ȸ��
    public void EatFood(float amount)
    {
        if (isGameover || isPaused) return;                                                     // ���� ���ᳪ �ߴ� ���¿����� �������� �ʰ�

        currentHunger = Mathf.Min(maxHunger, currentHunger + amount);                           // max ���� �ѱ��� �ʱ� ����

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"��� ȸ�� + {amount}", transform.position + Vector3.up);
        }
    }

    // ���ֺ� ���� (ũ����Ż�� ������ ���� ŰƮ ���)
    public void RepairSuit(float amount)
    {
        if (isGameover || isPaused) return;                                                     // ���� ���ᳪ �ߴ� ���¿����� �������� �ʰ�

        currentSuitDurability = Mathf.Min(maxSuitDurability, currentSuitDurability + amount);   // max ���� �ѱ��� �ʱ� ����

        if (FloatingTextManager.instance != null)
        {
            FloatingTextManager.instance.Show($"���ֺ� ���� + {amount}", transform.position + Vector3.up);
        }
    }

    private void CheckDeath()               // �÷��̾� ��� ó�� üũ �Լ�
    {
        if(currentHunger <= 0 || currentSuitDurability <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()              // �÷��̾� ��� �Լ�
    {
        isGameover = true;
        Debug.Log("�÷��̾� ���");
        //TODO : ��� ó�� �߰� (���ӿ��� UI, ������ ���)
    }

    public float GetHungerPercentage()              // ����� % ���� �Լ�
    {
        return (currentHunger / maxHunger) * 100;
    }

    public float GetSuitDurabilityPercentage()      // ��Ʈ % ���� �Լ�
    {
        return (currentSuitDurability / maxSuitDurability) * 100;
    }

    public bool IsGameOver()        // ���� ���� Ȯ��
    {
        return isGameover;
    }

    public void ResetStats()        // ���� �Լ� �ۼ� (������ �ʱ�ȭ �뵵)
    {
        isGameover = false;
        isPaused = false;
        currentHunger = maxHunger;
        currentSuitDurability = maxSuitDurability;
        hungerTimer = 0;
    }
}

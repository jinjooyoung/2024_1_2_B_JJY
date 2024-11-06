using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public ItemType itemType;               // ������ ���� (�� : ũ����Ż, �Ĺ�, ��Ǯ, ����)
    public string itemName;                 // ������ �̸�
    public float respawnTime = 30.0f;       // ������ �ð� (�������� �ٽ� ������ �� ������ ��� �ð�)
    public bool canCollect = true;          // ���� ���� ���� (������ �� �ִ��� ���θ� ��Ÿ��)

    // �������� �����ϴ� �޼���, PlayerInventory�� ���� �κ��丮�� �߰�
    public void CollectItem(PlayerInventory inventory)
    {
        // ���� ���� ���θ� üũ
        if (!canCollect) return;

        inventory.AddItem(itemType);            // �������� �κ��丮�� �߰�
        Debug.Log($"{itemName} ���� �Ϸ�");     // ������ ���� �Ϸ� �޼��� ���
        StartCoroutine(RespawnRoutine());
    }

    // ������ �������� ó���ϴ� �ڷ�ƾ
    private IEnumerator RespawnRoutine()
    {
        canCollect = false;         // ���� �Ұ��� ���·� ����
        GetComponent<MeshRenderer>().enabled = false;           // �������� �޽��������� ���� ������ �ʰ� ��

        // ������ ������ �ð���ŭ ���
        yield return new WaitForSeconds(respawnTime);

        GetComponent<MeshRenderer>().enabled = true;            // �������� �ٽ� ���̰� ��
        canCollect = true;                                      // ���� ���� ���·� ����
    }
}

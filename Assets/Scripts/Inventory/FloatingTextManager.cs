using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager instance;         // �̱���
    public GameObject textPrefab;                       // UI �ؽ�Ʈ ������

    private void Awake()
    {
        instance = this;                                // �̱��� ���
    }

    public void Show(string text, Vector3 worldPos)
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        GameObject textOBJ = Instantiate(textPrefab, transform);
        textOBJ.transform.position = screenPos;

        TextMeshProUGUI temp = textOBJ.GetComponent<TextMeshProUGUI>();
        if(temp != null )
        {
            temp.text = text;

            StartCoroutine(AnimateText(textOBJ));
        }
    }

    private IEnumerator AnimateText(GameObject textOBJ)
    {
        float duration = 1f;                                                            // ���� �ð�
        float timer = 0;                                                                // ����� Ÿ�̸�

        Vector3 startPos = textOBJ.transform.position;
        TextMeshProUGUI temp = textOBJ.GetComponent<TextMeshProUGUI>();                 // �޾ƿ� ������Ʈ���� TMP ��Ʈ ����

        while (timer < duration)                                                        // Ÿ�̸Ӱ� 1�ʰ� �ɶ�����
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            textOBJ.transform.position = startPos + Vector3.up * (progress * 50f);      // ��Ʈ�� ���� �ø��� ȿ���� �ش�

            if (temp != null )
            {
                temp.alpha = 1 - progress;
            }

            yield return null;
        }

        Destroy( textOBJ );
    }
}

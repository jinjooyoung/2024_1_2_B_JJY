using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour
{
    public float speed = 10f;

    // ���� �Լ� : �̵�
    public virtual void Move()
    {
        // ������ �ش� �ӵ���ŭ �����δ�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // �߻� �Լ� : ����
    public abstract void Horn();     // ���� �Լ��� ���� �Ѵ�.
}

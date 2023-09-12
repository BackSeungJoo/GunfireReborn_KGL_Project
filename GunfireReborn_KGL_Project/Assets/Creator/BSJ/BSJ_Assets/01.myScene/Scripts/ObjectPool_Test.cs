using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Test : MonoBehaviour
{
    public GameObject prefab; // ������ ������
    public int poolSize = 20; // Ǯ ũ��
    private List<GameObject> objectPool; // ������Ʈ Ǯ ����Ʈ

    private void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        objectPool = new List<GameObject>();

        // Ǯ ������ ��ŭ �ݺ�
        for(int i = 0; i < poolSize; i++)
        {
            // ������ ���� (�ڽĿ�����Ʈ�� ����)
            GameObject obj = Instantiate(prefab, this.transform);
            // ������ ��Ȱ��ȭ
            obj.SetActive(false);
            // ������Ʈ Ǯ ����Ʈ�� ������ �߰�
            objectPool.Add(obj);
        }
    }

    // ������Ʈ�� �������� �Լ�
    public GameObject GetObjectFromPool()
    {
        // ������Ʈ Ǯ ����Ʈ���� GameObject ��Ҹ� ��� ����
        foreach(GameObject obj in objectPool)
        {
            // Ǯ�ȿ� �ִ� ������ ������Ʈ�� Ȱ��ȭ �Ǿ����� �ʴٸ�
            if(obj.activeInHierarchy == false)
            {
                // �������� Ȱ��ȭ ���ְ� ��ȯ�Ѵ�.
                obj.SetActive(true);
                return obj;
            }
        }

        // Ǯ�� ��� ������ ������Ʈ�� ���� ��� ���� ����
        GameObject newObj = Instantiate(prefab);
        objectPool.Add(newObj); // ����Ʈ�� ���ο� ������ �߰�
        newObj.SetActive(true); // ���ο� ������ Ȱ��ȭ
        return newObj;          // ���ο� ������ ��ȯ
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}

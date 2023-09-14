using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

// ȣ���ϴ� ������Ʈ�� Ÿ���� �����ϱ����� �����ϴ� enum
public enum PoolObjType { BULLET, HELLBULLET, PISTOLBULLET }


// �ܺ� �ν�����â���� Ŭ���� ������ �����Ҽ� �ְ� ���ִ� [Serializable]
[Serializable]
public class PoolInfo
{
    // �ν�����â���� ������ ������
    public PoolObjType Type;    // ������Ʈ �̸� (Ÿ��)
    public int objAmount = 0;   // ������ Ǯ�� ������Ʈ ����
    public GameObject prefab;   // ������ Ǯ�� ������Ʈ ������
    public GameObject container;    // ������ Ǯ��������Ʈ�� ���� �����̳� ( ���� ������ƮǮ������ ���� �����ϱ� )
    public Stack<GameObject> poolObj = new Stack<GameObject>();

}

// �ν�����â���� Ŭ���� ���ΰ� ���� ������ ���� [Serializable]
public class ObjectPoolManager : MonoBehaviour
{ 

    public static ObjectPoolManager instance;

    private void Awake()
    {
        instance = this;
    }


    // ��ܿ� ������ PoolInfo Ŭ������ �ν�����â���� �����ϱ����� [Serialzifield]
    // �ν�����â���� ������ ������ŭ PoolInfo�� Ŭ������ ���� List�� ���� �� �� 
    // == �� ����Ʈ�� �����ϴ� �ε��� �ȿ� ������ Stack�� ������ �� 
    [SerializeField]
    List<PoolInfo> poolList;

    private Vector3 startPos = new Vector3 (0, 0, 0);

    void Start()
    {
        // ������ ������Ʈ Ǯ ������ŭ ( ��ܿ� ������ ����Ʈ ) �ݺ�
        for(int i = 0; i < poolList.Count; i++)
        {
            // PoolInfo Ŭ������ ��Ƶ� ������ �� poolLsit�� ��´�.
            FillPool(poolList[i]);
        }
    }


    // PoolInfo Ŭ���� ���ð� ( �ν�����â���� �����ϴ� �� ) ������� Ǯ��������Ʈ �����ϱ�
    void FillPool(PoolInfo poolInfo)
    {
        // PoolInfo Ŭ�������� ������ objAmount (������ ������Ʈ�� ����) ��ŭ �ݺ�
        for(int i = 0; i < poolInfo.objAmount ; i++)
        {
            // Ǯ���� ������Ʈ�� ���� ���� �ʱ�ȭ??
            GameObject tempObj = null;

            // �ν��Ͻ�ȭ ��Ų ������Ʈ ��Ƴֱ�
            tempObj = Instantiate(poolInfo.prefab, poolInfo.container.transform);

            // ������ ������Ʈ ��Ȱ��ȭ, ��ġ �ʱ�ȭ, �޸� �Ҵ��ϱ�
            tempObj.SetActive(false);
            tempObj.transform.position = poolInfo.container.transform.position;
            poolInfo.poolObj.Push(tempObj);
        }

    }

    // ������ Ǯ��������Ʈ�� ȣ���� �޼ҵ� 
    public GameObject GetPoolObj(PoolObjType type)
    {
        // GetPoolByType() �޼ҵ�� �����ϰ� ��ȯ���� type���� PoolInfo Ŭ������ �����ϱ�. 
        PoolInfo select = GetPoolByType(type);

        // �ش��ϴ� Ÿ���� ���� 
        Stack<GameObject> pool = select.poolObj;

        // ��Ƶ� ���ӿ�����Ʈ �ʱ�ȭ
        GameObject objInstance = null;

        // ȣ���ϴ� ������Ʈ ���� �����ص� ������Ʈ�� ����ϴٸ�
        if(pool.Count > 0)
        {
            // �ش� ������Ʈ�� ���ӿ�����Ʈ�� ���
            objInstance = pool.Peek();
            // Stack �޸𸮿��� ���ش�.
            pool.Pop();
        }

        // ȣ���ϴ� ������Ʈ ���� ���ð����� ���ٸ� 
        else
        {
            // Ǯ��������Ʈ�� ���� �������ش�.
            objInstance = Instantiate(select.prefab, select.container.transform);
        }

        // ��� ������Ʈ ��ȯ
        return objInstance;
    }

    // ȣ��� Ǯ��������Ʈ�� Ǯ�� �ٽ� ��ȯ�ϴ� �޼ҵ�
    public void CoolObj(GameObject obj, PoolObjType type)
    {
        // 
        PoolInfo select = GetPoolByType(type);

        obj.SetActive(false);
        obj.transform.position = select.container.transform.position;
        Stack<GameObject> pool = select.poolObj;

        if(pool.Contains(obj) == false)
        {
            pool.Push(obj);
        }
    }

    // 
    private PoolInfo GetPoolByType(PoolObjType type)
    {
        // ȣ���ϴ� ������Ʈ ������ �����ϴ� �ݺ���?
        // ������Ʈ Ǯ ������ŭ �ݺ����� ����
        for(int i = 0; i < poolList.Count; i++)
        {
            // ȣ���ϴ� ������Ʈ�� Ÿ�԰� ��ġ�Ѵٸ� 
            if(type == poolList[i].Type)
            {
                // �ش��ϴ� ������ƮǮ�� �ε����� ��ȯ�Ѵ�. (���� ����);
                return poolList[i];
            }
        }

        // �޼ҵ尡 �����Ǳ� ���� == �ݺ��� ���ο� ��ġ�ϴ� Ÿ���� ������Ʈ�� ���ٸ� null���� ��ȯ
        return null;
    }

}

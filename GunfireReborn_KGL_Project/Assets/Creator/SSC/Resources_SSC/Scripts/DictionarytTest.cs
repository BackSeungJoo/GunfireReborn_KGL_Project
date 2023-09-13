using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

[Serializable]
public class Test
{
    public int testAmount = 0;
    public GameObject prefab;
}

// �ν�����â���� Ŭ���� ���ΰ� ���� ������ ���� [Serializable]
public class DictionarytTest : MonoBehaviour
{
    // ��ܿ� ������ Test Ŭ������ �ν�����â���� �����ϱ����� ����Ʈ 
    [SerializeField]
    List<Test> test;

    public GameObject[] prefabs;

    Stack<GameObject> stack = new Stack<GameObject>();
    Stack<GameObject> floatStack = new Stack<GameObject>();
    Stack<GameObject> stringStack = new Stack<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        Dictionary<string, Stack<GameObject>> dic = new Dictionary<string, Stack<GameObject>>();

        for(int i  = 0; i < 10; i++)
        {
            stack.Push(prefabs[0]);
            floatStack.Push(prefabs[1]);
            stringStack.Push(prefabs[2]);
        }   

        dic.Add("Cube", stack);
        dic.Add("Sphere", floatStack);
        dic.Add("Capsule", stringStack);

        Debug.Log("��ųʸ��� ī��Ʈ�� :" + dic["Capsule"].Count);

        Debug.Log("===== �ν��Ͻ�ȭ �� =====");

        for(int i = 0; i < 10; i++)
        {
            Instantiate(prefabs[2], transform.position, transform.rotation);
            Debug.Log("��ųʸ���" + i + " ��° ī��Ʈ �� : " + dic["Capsule"].Count);
            dic["Capsule"].Pop();
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

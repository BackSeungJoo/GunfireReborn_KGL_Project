using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ������ ���� Ŭ����
/// </summary>
public class ItemData
{
    public int index;    // ������ ��ȣ
    public string name;  // ������ �̸�
    public string info;  // ������ ����
    public int count;    // ������ ����
    public int price;    // ������ ����
}

public class ItemDataManager : MonoBehaviour
{
    public List<ItemData> ItemList { get; private set; } = new List<ItemData>();  // ���� ������ ������ 

    #region ItemList
    private List<string> itemNameList = new List<string> { "�ű��� ����", "ź�� ����", "Ư��ź", "����ź", "�Ϲ�ź" };
    private List<string> itemInfoList = new List<string>
    {"�ű��� ����. �ӿ� ���� ��������� ����, ������ 30%�� ������� ȸ��",
     "��� ź���� �����Ͽ� �� ä���",
     "Ư�� ź���� �����Ͽ� �� ä���",
     "���� ź���� �����Ͽ� �� ä���",
     "�Ϲ� ź���� �����Ͽ� �� ä���"};

    /// <summary>
    /// ItemNameList ������Ƽ
    /// </summary>
    public List<string> ItemNameList { get { return itemNameList; } }
    /// <summary>
    /// ItemInfoList ������Ƽ
    /// </summary>
    public List<string> ItemInfoList { get { return itemInfoList; } }
    #endregion

    public Sprite[] itemSprites;  // ������ �̹���

    // TODO: ��� ITEMCOUNTMAX �� �߰��Ѵ�. 5�� �ʱ�ȭ���ش�.
    private const int ITEMCOUNTMAX = 5;

    private void Awake()
    {
        // TODO: ITEMCOUNTMAX ��ŭ �ݺ��ؼ� itemList�� �߰��Ѵ�.
        for (int i = 0; i < ITEMCOUNTMAX; i++)
        {
            ItemData data = new ItemData();

            // TODO: �������� ��ȣ, �̸�, ����, ����, ������ �������ش�.
            data.index = i;
            data.name = itemNameList[i];
            data.info = ItemInfoList[i];
            data.count = 1;
            data.price = Random.Range(1, 100);

            ItemList.Add(data);

        }
    }
}

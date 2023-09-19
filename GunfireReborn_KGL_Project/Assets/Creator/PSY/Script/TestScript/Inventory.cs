using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private ItemDataManager itemDataManager;

    private WeaponBox[] weaponBoxes;
    private InventoryBox[] inventoryBoxes;

    private void Start()
    {
        itemDataManager = GameObject.Find("@Managers").GetComponent<ItemDataManager>();

        #region F ����
        weaponBoxes = GetComponentsInChildren<WeaponBox>();  // ���� ��ũ��Ʈ�� ���� ������Ʈ�� ��� �ڽ� ������Ʈ �߿���
                                                             // WeaponBox Ŭ������ ���� �ڽĵ��� ��� �������� �Լ��̴�.

        /* ������ ���� ������ */
        weaponBoxes[0].SetData(new WeaponData("�������� +2", 20, 40, 60, "Ư��ź", "���� �����Դϴ�.", "��� ��"));
        weaponBoxes[1].SetData(new WeaponData("������ +4", 30, 60, 90, "Ư��ź2", "���� �����Դϴ�!", ""));
        #endregion

        inventoryBoxes = GetComponentsInChildren<InventoryBox>();

        //for ( int i = 0; i < itemDataManager.ItemList.Count; i++ )
        //{
        //    inventoryBoxes[i].SetItemData(itemDataManager.ItemList[i]);
        //}
    }

    private void Update()
    {
        #region F ����
        if ( Input.GetKeyDown(KeyCode.F) )
        {
            Swap( weaponBoxes[0],  weaponBoxes[1]);
        }
        #endregion
    }

    #region F ����
    /// <summary>
    /// ������ ���� �Լ�
    /// </summary>
    public void Swap( WeaponBox a ,  WeaponBox b)
    {
        WeaponData tempData = new WeaponData();

        tempData = a.data;
        
        a.SetData(b.data);
        b.SetData(tempData);
    }
    #endregion
}

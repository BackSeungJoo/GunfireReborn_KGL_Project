using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private ShopManager shopManager;
    private ItemDataManager itemDataManager;

    private WeaponBox[] weaponBoxes;
    private List<InventoryBox> inventoryBoxes;

    #region ������Ƽ
    public List<InventoryBox> InventoryBoxes { get { return inventoryBoxes; } }  // InventoryBoxes List ������Ƽ
    public ItemDataManager ItemDataManager { get { return itemDataManager; } }  // ItemDataManager ������Ƽ
    #endregion

    private void Start()
    {
        #region F ����
        weaponBoxes = GetComponentsInChildren<WeaponBox>();  // ���� ��ũ��Ʈ�� ���� ������Ʈ�� ��� �ڽ� ������Ʈ �߿���
                                                             // WeaponBox Ŭ������ ���� �ڽĵ��� ��� �������� �Լ��̴�.

        /* ������ ���� ������ */
        weaponBoxes[0].SetData(new WeaponData("�������� +2", 20, 40, 60, "Ư��ź", "���� �����Դϴ�.", "��� ��"));
        weaponBoxes[1].SetData(new WeaponData("������ +4", 30, 60, 90, "Ư��ź2", "���� �����Դϴ�!", ""));
        #endregion

        itemDataManager = GameObject.Find("@Managers").GetComponent<ItemDataManager>();
        shopManager = GameObject.Find("@Managers").GetComponent<ShopManager>();

        // ToList() : �迭�� ����Ʈ�� �ٲ��ش�.
        inventoryBoxes = GameObject.Find("ItemInventoryFrame/ItemBoxGroup").GetComponentsInChildren<InventoryBox>().ToList();
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

    /// <summary>
    /// ���� ó�� �Լ�
    /// </summary>
    /// <param name="buyItem">������ ������</param>
    public void Trade( ItemData buyItem )
    {
        bool isSame = false;

        for ( int i = 0; i < inventoryBoxes.Count; i++ )
        {
            if (InventoryBoxes[i].ItemData == buyItem )
            {  // ������ �������� �κ��丮�� �̹� �ִ� �������̶��
                inventoryBoxes[i].SetItemCount(buyItem.count);
                isSame = true;
                break;
            }
        }

        if ( !isSame )
        {
            for (int i = 0; i < inventoryBoxes.Count; i++)
            {
                if (inventoryBoxes[i].ItemData == null)
                {
                    inventoryBoxes[i].SetItemData(buyItem);         // ������ �������� ����ִ� �κ��丮�� �ִ´�.
                    inventoryBoxes[i].SetItemCount(buyItem.count);  // ������ �������� ������ŭ �κ��丮�� �������� �ø���.
                    inventoryBoxes[i].SetItemImage();               // ������ �������� �̹����� �κ��丮�� �ִ´�.
                    break;
                }
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryBox : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UIManager uiManager;

    private Inventory inventory;
    private ItemData itemData;
    private Image itemImage;
    private TextMeshProUGUI itemCountText;
    private Outline inventoryBoxOutLine;
    private Transform inventoryBoxSize;


    private int itemCount = 0;

    public ItemData ItemData { get { return itemData; } }  // ItemData ������Ƽ

    private void Start()
    {
        uiManager = GameObject.Find("@Managers").GetComponent<UIManager>();

        inventory = GameObject.Find("InventoryCanvas").GetComponent<Inventory>();
        itemImage = transform.Find("ItemImage").GetComponent<Image>();
        itemCountText = transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
        inventoryBoxOutLine = GetComponent<Outline>();
        inventoryBoxSize = GetComponent<Transform>();

        inventoryBoxOutLine.enabled = false;
        inventoryBoxSize.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        itemImage.color = new Color(0f, 0f, 0f, 0f);
        itemCountText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemData == null)
        {
            return;
        }

        SetItemCount(-1);  // �κ��丮���� �ش� �������� Ŭ���ϸ� ������ �����Ѵ�.
        SetItemImage();

        if ( itemCount <= 0 )
        {
            itemData = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryBoxOutLine.enabled = true;
        inventoryBoxSize.localScale = new Vector3(1.2f , 1.2f , 1.0f);

        if (itemData != null)
        {
            uiManager.SetActiveToolTip(true);
            uiManager.SetToolTipText(itemData);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryBoxOutLine.enabled = false;
        inventoryBoxSize.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        uiManager.SetActiveToolTip(false);
    }

    #region ����
    /// <summary>
    /// ������ ������ ���� �Լ�
    /// </summary>
    /// <param name="itemData">������ ����</param>
    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
    }

    /// <summary>
    /// ������ �̹��� ���� �Լ�
    /// </summary>
    public void SetItemImage()
    {
        itemImage.sprite = inventory.ItemDataManager.itemSprites[itemData.index];
        itemImage.color = Color.white;

        if (itemCount <= 0)
        {
            itemImage.gameObject.SetActive(false);
        }
        else
        {
            itemImage.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ������ ���� ���� �Լ�
    /// </summary>
    public void SetItemCount(int AddCount)
    {
        itemCount += AddCount;  // �߰��� ������ ������ŭ ���� ������ ������Ų��.
        itemCountText.text = $"{itemCount}";  // UI�� ǥ��

        // TODO: 0������ ��, �ؽ�Ʈ�� ����������  ������ 0���� ��ȯ���Ѿ���
        if (itemCount <= 0)
        {
            itemCountText.text = "";
            itemCount = 0;
        }
    }
    #endregion
}

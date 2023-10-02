using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region ����
    private GameObject shopPopup;        // ���� �˾�
    private ShopManager shopManager;
    #endregion

    private GameObject invenCanvas;      // ���� UI

    private GameObject blackSmithCanvas; // �������� UI

    private GameObject mainCanvas;       // ���� UI

    private TextMeshProUGUI text_Reroll; // ���ΰ�ħ �ؽ�Ʈ

    private GameObject toolTip;          // �κ��丮 ����
    private TextMeshProUGUI toolTipTitleText;
    private TextMeshProUGUI toolTipInfoText;

    #region Exit ����
    private Button bt_Exit;              // ESC ��ư
    private TextMeshProUGUI text_Exit;   // ESC �ؽ�Ʈ
    #endregion

    private void Start()
    {
        shopPopup = GameObject.Find("ShopPopupCanvas");
        shopManager = GameObject.Find("@Managers").GetComponent<ShopManager>();

        invenCanvas = GameObject.Find("InventoryCanvas");

        blackSmithCanvas = GameObject.Find("BlackSmithCanvas");

        mainCanvas = GameObject.Find("MainCanvas");

        text_Reroll = GameObject.Find("RerollText").GetComponent<TextMeshProUGUI>();
        
        bt_Exit = GameObject.Find("ExitButton").GetComponent<Button>();
        text_Exit = GameObject.Find("ExitText").GetComponent <TextMeshProUGUI>();

        toolTip = GameObject.Find("ToolTip");

        toolTipTitleText = toolTip.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        toolTipInfoText = toolTip.transform.Find("ItemInfo").GetComponent<TextMeshProUGUI>();



        SetActiveMainCanvas(true);
        SetActiveInven(false);
        //SetActiveShopPopup(false);
        SetActiveBlackSmith(false);
        SetActiveToolTip(false);

    }



    /// <summary>
    /// ShopPopup Ȱ��ȭ üũ �Լ� 
    /// </summary>
    public bool CheckActiveShopPopup()
    {
        if (shopPopup.activeInHierarchy)
        {
            return true;
        }
        return false;
    }


    #region �̺�Ʈ �Լ�
    /// <summary>
    /// ExitButton �̺�Ʈ �Լ�
    /// </summary>
    public void OnExitButton()
    {  // ESC ��ư Ŭ�� �� ���� / ���� �˾��� �ݴ´�.
        shopPopup.SetActive(false);
        invenCanvas.SetActive(false);
        blackSmithCanvas.SetActive(false);
    }

    /// <summary>
    /// RerollButton �̺�Ʈ �Լ�
    /// </summary>
    public void OnRerollButton()
    {  // ���ΰ�ħ ��ư Ŭ�� �� ���ΰ�ħ Ƚ���� �����Ǵ� �Լ� ����
        SetRerollText();
    }
    #endregion


    #region ���� �Լ�
    /// <summary>
    /// RerollText ���� �Լ�
    /// </summary>
    /// <param name="count">���ΰ�ħ Ƚ��</param>
    public void SetRerollText()
    {  // ���ΰ�ħ ��ư Ŭ�� �� Ŭ�� Ƚ���� ������ �ؽ�Ʈ ��� �Լ�
        if ( shopManager.Count > 0 )
        {
            shopManager.Count--;
            text_Reroll.text = $"���� ��ħ <i> {shopManager.Count} / 3 <i> ";
        }
    }

    /// <summary>
    /// ShopPopup Ȱ��ȭ ���� �Լ�
    /// </summary>
    /// <param name="isActive">Ȱ��ȭ ����</param>
    public void SetActiveShopPopup(bool isActive)
    {  // ���� �˾� Ȱ��ȭ / ��Ȱ��ȭ �Լ�
        shopPopup?.SetActive(isActive);
        SetActiveMainCanvas(!isActive);  // ���� �˾��� �ݴ�� ����
    }

    /// <summary>
    /// BlackSmith Ȱ��ȭ ���� �Լ�
    /// </summary>
    /// <param name="isActive">Ȱ��ȭ ����</param>
    public void SetActiveBlackSmith(bool isActive)
    {  // �������� UI Ȱ��ȭ / ��Ȱ��ȭ �Լ�
        blackSmithCanvas?.SetActive(isActive);
        SetActiveMainCanvas(!isActive);  // �������� �˾��� �ݴ�� ����
    }

    /// <summary>
    /// Inventory Ȱ��ȭ ���� �Լ�
    /// </summary>
    /// <param name="isActive">Ȱ��ȭ ����</param>
    public void SetActiveInven(bool isActive)
    {  // ���� UI Ȱ��ȭ / ��Ȱ��ȭ �Լ�
        invenCanvas?.SetActive(isActive);
        SetActiveMainCanvas(!isActive);  // ���� �˾��� �ݴ�� ����
    }

    /// <summary>
    /// MainCanvas Ȱ��ȭ ���� �Լ�
    /// </summary>
    /// <param name="isActive">Ȱ��ȭ ����</param>
    public void SetActiveMainCanvas(bool isActive)
    {  // ���� UI Ȱ��ȭ / ��Ȱ��ȭ �Լ�
        mainCanvas?.SetActive(isActive);
    }

    /// <summary>
    /// ToolTip Ȱ��ȭ ���� �Լ�
    /// </summary>
    /// <param name="isActive">Ȱ��ȭ ����</param>
    public void SetActiveToolTip(bool isActive)
    {
        toolTip.SetActive(isActive);
    }

    /// <summary>
    /// ToolTip Ȱ��ȭ ���� ���� �Լ�
    /// </summary>
    /// <param name="itemData">Ȱ��ȭ �� ������</param>
    public void SetToolTipText(ItemData itemData)
    {
        toolTipTitleText.text = itemData.name;
        toolTipInfoText.text = itemData.info;
    }
    #endregion


}

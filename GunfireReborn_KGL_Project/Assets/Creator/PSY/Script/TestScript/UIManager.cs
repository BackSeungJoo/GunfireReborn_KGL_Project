using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject shopPopup;        // ���� �˾�
    private ShopManager shopManager;

    private TextMeshProUGUI text_Reroll; // ���ΰ�ħ �ؽ�Ʈ

    #region Exit ����
    private Button bt_Exit;              // ESC ��ư
    private TextMeshProUGUI text_Exit;   // ESC �ؽ�Ʈ
    #endregion

    private void Start()
    {
        shopPopup = GameObject.Find("ShopPopupCanvas");
        shopManager = GameObject.Find("@Managers").GetComponent<ShopManager>();

        text_Reroll = GameObject.Find("RerollText").GetComponent<TextMeshProUGUI>();
        
        bt_Exit = GameObject.Find("ExitButton").GetComponent<Button>();
        text_Exit = GameObject.Find("ExitText").GetComponent <TextMeshProUGUI>();
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
    {  // ESC ��ư Ŭ�� �� ���� �˾��� �ݴ´�.
        shopPopup.SetActive(false);
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
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject shopPopup;

    private void Start()
    {
        shopPopup = GameObject.Find("ShopPopupCanvas");
    }

    /// <summary>
    /// ShopPopup Ȱ��ȭ ���� �Լ�
    /// </summary>
    /// <param name="isActive">Ȱ��ȭ ����</param>
    public void SetActiveShopPopup( bool isActive )
    {
        shopPopup?.SetActive( isActive ); 
    }

    /// <summary>
    /// ShopPopup Ȱ��ȭ üũ �Լ� 
    /// </summary>
    public bool CheckActiveShopPopup()
    {
        if ( shopPopup.activeInHierarchy )
        {
            return true;
        }
        return false;
    }
}

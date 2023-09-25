using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Exit Popup Class
public class CheckExitButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler , IPointerClickHandler
{
    public GameObject popupExit;

    public Image bt;

    private Color baseColor;   // �⺻ �÷�
    private Color changeColor; // �ٲ�� �÷�

    public string checkType;   // ��ư Ÿ��

    private void Start()
    {
        popupExit = GameObject.Find("PopupExit");

        bt = GetComponent<Image>();
        baseColor = bt.color;  // �⺻ �÷��� �����Ѵ�.
    }

    #region �̺�Ʈ �Լ�
    /// <summary>
    /// ���콺�� �ش� ���� ���� �ִٸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    { 
        ColorUtility.TryParseHtmlString("#FFFFFFF", out changeColor);
        bt.color = changeColor;  // �÷��� �ٲٰ� �ٲ� �÷��� �����Ѵ�.
    }

    /// <summary>
    /// ���콺�� �ش� �����ּ� ���� ���ٸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        bt.color = baseColor;  // �⺻ �÷��� �ٽ� ����
    }

    /// <summary>
    /// ���콺�� �ش� ������ Ŭ���ϸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if ( checkType == "Yes" )  // ��ư Ÿ���� "Yes" ��� if�� ����
        {
            Application.Quit();  // ������ �����Ѵ�,
        }
        else
        {
            popupExit?.SetActive(false);  // ��ư Ÿ���� "Yes"�� �ƴ϶�� ���� Ȯ�� �˾��� �ݴ´�.
        }
    }
    #endregion
}

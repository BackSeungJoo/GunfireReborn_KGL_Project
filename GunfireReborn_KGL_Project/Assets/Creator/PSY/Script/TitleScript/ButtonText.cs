using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonText : MonoBehaviour ,IPointerEnterHandler , IPointerExitHandler
{
    public TextMeshProUGUI Text; 
    private Color baseColor;    // �⺻ �÷�
    private Color changeColor;  // �ٲ�� �÷�

    private void Start()
    {
        baseColor = Text.color;  // �⺻ �÷��� ����
    }

    #region �̺�Ʈ �Լ�
    /// <summary>
    /// ���콺�� �ش� ���� ���� �ִٸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)  // ���콺�� �ش� ���� ���� �ִٸ� �ش� �Լ��� ����
    {
        ColorUtility.TryParseHtmlString("#C29024FF", out changeColor);  // �ٲ� �÷��� changeColor�� ����
        Text.color = changeColor;  // �ٲ�� �÷��� ���� text.color �� �����Ѵ�.
        Text.fontSize = 60;  // ���� ��Ʈ ����� 60���� ����
    }

    /// <summary>
    /// ���콺�� �ش� �����ּ� ���� ���ٸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)  // ���콺�� �ش� �������� ���� ���ٸ� �ش� �Լ��� ����
    {
        Text.color = baseColor;  // �⺻ �÷��� ���� text.color �� �����Ѵ�.
        Text.fontSize = 50;      // ���� ��Ʈ ����� 50���� ����
    }
    #endregion
}

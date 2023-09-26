using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Title Start/End Button Class
public class TitleButton : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerClickHandler
{
    private Image bt; // ��ư �̹���
    public string buttonType;  // ��ư Ÿ��

    private GameObject popupExit;  // ���� Ȯ�� �˾�

    private void Start()
    {
        bt = GetComponent<Image>();  // ��ư �̹����� ������Ʈ�� �����´�.

        bt.fillAmount = 0;  // ������ �� ��ư�� ����� 0���� �Ѵ�.

        popupExit = GameObject.Find("PopupExit");  // ���� Ȯ�� �˾��� �����ͼ� ������ �ִ´�.
        popupExit?.SetActive(false);  // ��Ȱ��ȭ ��Ų��.
    }

    #region �̺�Ʈ �Լ�
    /// <summary>
    /// ���콺�� �ش� ���� ���� �ִٸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData) 
    {
        StartCoroutine(OpenButton());  // �ڷ�ƾ �Լ� ����
    }

    /// <summary>
    /// ���콺�� �ش� �����ּ� ���� ���ٸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)  // ���콺�� �ش� �������� ���� ���ٸ� �ش� �Լ��� ����
    {
        StopAllCoroutines();  // ��� �ڷ�ƾ�� �����Ų��.
        bt.fillAmount = 0;    // ��ư�� ����� 0���� �Ѵ�.
    }  

    /// <summary>
    /// ���콺�� �ش� ������ Ŭ���ϸ� ����Ǵ� �̺�Ʈ �Լ�
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if ( buttonType == "����" )
        {
            popupExit?.SetActive(true);  // ���� Ȯ�� �˾��� Ȱ��ȭ ��Ų��.
        } 
        else
        {
            SceneManager.LoadScene("Main_LoadingScene");  
        }
    }
    #endregion

    /// <summary>
    /// ���콺�� �ش� ���� ���� ���� �� �����ϴ� �ڷ�ƾ �Լ�
    /// </summary>
    IEnumerator OpenButton()  // �ڷ�ƾ : ���� ������ ���� �ְ� ���� �� ���� �Լ�
    {
        while (bt.fillAmount < 1)  // ��ư�� ����� 1���� ���� ������ ����
        {
            bt.fillAmount += 0.02f;  // ��ư�� �̹����� �ش� ����ŭ�� ���ذ���.
            yield return new WaitForSeconds(Time.deltaTime);  // �ݺ��ϴ� �����̸� Time.deltaTime ��ŭ�� �ش�.
        }

        yield break;  // ���� �ݺ����� ����� �ش� �ڷ�ƾ ����
    }
}

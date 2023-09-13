using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public void OnPointerEnter(PointerEventData eventData)  // ���콺�� �ش� ���� ���� �ִٸ� �ش� �Լ��� ����
    {
        StartCoroutine(OpenButton());  // �ڷ�ƾ �Լ� ����
    }

    public void OnPointerExit(PointerEventData eventData)  // ���콺�� �ش� �������� ���� ���ٸ� �ش� �Լ��� ����
    {
        StopAllCoroutines();  // ��� �ڷ�ƾ�� �����Ų��.
        bt.fillAmount = 0;    // ��ư�� ����� 0���� �Ѵ�.
    }  

    IEnumerator OpenButton()  // �ڷ�ƾ : ���� ������ ���� �ְ� ���� �� ���� �Լ�
    {
        while ( bt.fillAmount < 1 )  // ��ư�� ����� 1���� ���� ������ ����
        {
            bt.fillAmount += 0.02f;  // ��ư�� �̹����� �ش� ����ŭ�� ���ذ���.
            yield return new WaitForSeconds(Time.deltaTime);  // �ݺ��ϴ� �����̸� Time.deltaTime ��ŭ�� �ش�.
        }

        yield break;  // ���� �ݺ����� ����� �ش� �ڷ�ƾ ���� ( �ʼ� )
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ( buttonType == "����" )
        {
            popupExit?.SetActive(true);  // ���� Ȯ�� �˾��� Ȱ��ȭ ��Ų��.
        } 
        else
        {
            SceneManager.LoadScene("LoadingScene_PSY");
        }
    }
}

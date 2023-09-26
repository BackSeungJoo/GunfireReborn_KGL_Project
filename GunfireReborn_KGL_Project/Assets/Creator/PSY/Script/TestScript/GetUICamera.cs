using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetUICamera : MonoBehaviour
{
    // UI ī�޶� ������ ����
    private Camera uiCamera;
    private Canvas minimapCanvas;

    private void Awake()
    {
        // Canvas ������Ʈ ã��
        minimapCanvas = GetComponent<Canvas>();
    }
 
    void Start()
    {
        // ���̾��Ű â���� UI ī�޶� ã��
        uiCamera = GameObject.Find("UI Camera").GetComponent<Camera>();

        // UI ī�޶� ���� ���, �ش� ī�޶��� ������Ʈ�� ������ ����� �� ����
        if (uiCamera != null)
        {
            // Canvas ������Ʈ�� �̺�Ʈ ī�޶� UI ī�޶� �Ҵ�
            minimapCanvas.worldCamera = uiCamera;
        }
    }
}

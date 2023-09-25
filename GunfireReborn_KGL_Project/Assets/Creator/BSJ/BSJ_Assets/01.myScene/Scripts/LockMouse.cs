using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMouse : MonoBehaviour
{
    private bool isMouseLocked = true; // ���콺 ��� ���¸� ��Ÿ���� ����

    private void Start()
    {
        LockMouseCursor();
    }

    private void Update()
    {
        // ESC Ű�� ������ ���콺 ��� ���¸� ����մϴ�.
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isMouseLocked = !isMouseLocked;
            LockMouseCursor();
        }
    }

    private void LockMouseCursor()
    {
        if (isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
            Cursor.visible = false; // ���콺 Ŀ�� ����
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // ���콺 ��� ����
            Cursor.visible = true; // ���콺 Ŀ�� ǥ��
        }
    }
}

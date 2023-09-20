using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class MouseLook : MonoBehaviour
{
    public static MouseLook instance;

    [Header("����")]
    public Vector2 clampInDegrees = new Vector2(360, 180);
    public bool lockCursor = true;
    [Space]
    private Vector2 sensitivity = new Vector2(2, 2);
    [Space]
    public Vector2 smoothing = new Vector2(3, 3);

    [Header("����Ī ����")]
    public GameObject characterBody;

    private Vector2 targetDirection;
    private Vector2 targetCharacterDirection;

    private Vector2 _mouseAbsolute;
    private Vector2 _smoothMouse;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool scoped;

    void Start()
    {
        
        instance = this;

        // ī�޶��� �ʱ� �������� ��ǥ ���� ����
        targetDirection = transform.localRotation.eulerAngles;

        // ĳ���� ��ü�� �ʱ� ���·� ��ǥ ���� ����
        if (characterBody)
            targetCharacterDirection = characterBody.transform.localRotation.eulerAngles;

        if (lockCursor)
            LockCursor();
        
    }

    public void LockCursor()
    {
        // Ŀ���� ����� ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        // ���ϴ� ��ǥ���� ������� ��ũ��Ʈ�� ������ ������ �� �ְ� ��
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

        // �� �ΰ��� ���콺���� �� ����� �б� ���� ���� ���콺 �Է� ������
        mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // �ΰ��� ������ ���� �Է��� �����ϸ��ϰ� �װ��� ������ ���� ����
        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

        // ������ ��Ÿ�� �����Ͽ� �ð� ����� ���� ���콺 �̵� ����
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

        // ���� ���콺 �̵� ��(0����������) ã��
        _mouseAbsolute += _smoothMouse;

        // ���� x ���� ���� �����ϰ� �����Ͽ� ���� ��ȯ�� ������ ���� �ʰ� ��
        if (clampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

        // �׷� ���� �۷ι� y ���� �����ϰ� ����
        if (clampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);

        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;

        // ī�޶��� �θ� ������ �ϴ� ĳ���� ��ü�� �ִ� ���
        if (characterBody)
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
            characterBody.transform.localRotation = yRotation * targetCharacterOrientation;
        }
        else
        {
            var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
            transform.localRotation *= yRotation;
        }

        
    }
}
*/
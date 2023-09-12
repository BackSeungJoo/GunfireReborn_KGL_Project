using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FakeBoom : MonoBehaviour
{
    public float scaleFactor = 1f;          // ���� ������
    public float duration = 0.8f;           // ���濡 �ɸ� �ð�
    private Vector3 startScale;             // �ʱ� ������
    private float startTime;                // ���� �ð�

    private void Start()
    {
        startScale = transform.localScale;  // �ʱ� ������ ����
        startTime = Time.time;              // ���� �ð� ����
    }

    private void Update()
    {
        // ���� �ð����� ���� �ð��� ���� ����� �ð� ���
        float elapsed = Time.time - startTime;

        // �������� õõ�� �����ϴ� ���� ��� (0���� 1����)
        float t = Mathf.Clamp01(elapsed / duration);

        // ������ ���� �ʱ� �����Ͽ��� ���� �����Ϸ� �����Ͽ� ����
        transform.localScale = Vector3.Lerp(startScale, Vector3.one * scaleFactor, t);

        // ������ �Ϸ� �Ǹ� ������ ���� �ߴ�
        if(t >= 1.0f)
        {
            transform.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        startTime = Time.time;
    }


    // �ʱ� �����Ϸ� ����
    private void OnDisable()
    {
        transform.localScale = startScale;
    }
}

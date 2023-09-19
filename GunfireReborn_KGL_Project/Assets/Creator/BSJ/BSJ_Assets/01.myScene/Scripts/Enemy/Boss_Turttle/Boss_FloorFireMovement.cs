using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FloorFireMovement : MonoBehaviour
{
    public Vector3 targetPos;       // �̵��� ��ǥ ��ġ
    public float duration = 5.0f;   // �̵��� �ɸ� �ð�

    Vector3 initialPosition;    // �̵� ���� ��ġ
    float startTime;            // �̵� ���� �ð�

    bool isMoving = false;      // �̵� ������ üũ


    private void Awake()
    {
        // ���� ��ġ�� ���� ��ġ�� ����
        initialPosition = transform.position;
    }

    private void OnEnable()
    {
        // ��ġ�� �ٽ� ����
        transform.position = initialPosition;
    }

    void Update()
    {
        if(!isMoving)
        {
            StartCoroutine(MoveOverTime());
        }
    }

    IEnumerator MoveOverTime()
    {
        isMoving = true;
        startTime = Time.time;

        // ������ ����Ͽ� ���� ������Ʈ�� �ε巴�� �̵���Ű�µ� ���
        while(Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            // ���� ��ġ���� ��ǥ ��ġ���� �����ؼ� �̵�
            transform.position = Vector3.Lerp(initialPosition, targetPos, t);

            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        gameObject.SetActive(false);
    }
}

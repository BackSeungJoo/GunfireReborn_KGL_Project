using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FloorFireMovement : MonoBehaviour
{
    public GameObject startPos;        // �̵� ���� ��ġ

    private float moveSpeed = 15f;     // �̵� �ӵ�
    private float lifeTime = 2f;      // ����Ʈ Ȱ��ȭ �ð�
    private float timer = 0f;         // ��Ȱ��ȭ���� ������ �ð�

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * moveSpeed;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifeTime)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
    }
    private void OnDisable()
    {
        // ��ġ�� �ٽ� ����
        gameObject.transform.localPosition = Vector3.zero;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("�÷��̾ ��Ҵ�.");
            // �÷��̾�� ������ �ִ� ����
        }
    }
}

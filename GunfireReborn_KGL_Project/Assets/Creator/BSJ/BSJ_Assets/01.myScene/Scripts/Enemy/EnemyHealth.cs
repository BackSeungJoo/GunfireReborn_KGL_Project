 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemyHealth : MonoBehaviourPun
{
    public int maxHealth;       // �ִ� Hp
    public int curHealth;       // ���� Hp

    public Image hpbar;         // HP�� �̹���

    private void Awake()
    {
        // ���� Hp�� �ִ� Hp�� �ʱ�ȭ
        curHealth = maxHealth;

        // HP�� �ʱ�ȭ
        hpbar.fillAmount = (float)curHealth / (float)maxHealth;
    }

    // Enemy�� ������ �޾��� �� ���� ���ν��� �� ���� �Լ�
    [PunRPC]
    public void EnemyTakeDamage(int _damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            curHealth -= _damage;

            hpbar.fillAmount = (float)curHealth / (float)maxHealth;

            if (curHealth <= 0)
            {
                // �켱 ��Ȱ��ȭ�� �س�����.
                transform.gameObject.SetActive(false);

                // PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    public void EnemyHpDown(int _damage)
    {
        curHealth -= _damage;

        hpbar.fillAmount = (float)curHealth / (float)maxHealth;

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

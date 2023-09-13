 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemyHealth : MonoBehaviour
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


    // Enemy �����ΰ� ������ �޾��� �� ���� ���ν��� �� ���� �Լ�
    [PunRPC]
    public void EnemyTakeDamage(int _damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            curHealth -= _damage;

            hpbar.fillAmount = (float)curHealth / (float)maxHealth;

            if (curHealth <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
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

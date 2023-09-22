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


    [PunRPC]
    public void ApplyHealthUpdate(int newhealth, float newHpbar)
    {
        curHealth = newhealth;
        hpbar.fillAmount = newHpbar;

        if (curHealth <= 0)
        {
            // �켱 ��Ȱ��ȭ�� �س�����.
            transform.gameObject.SetActive(false);

            // PhotonNetwork.Destroy(gameObject);
        }
    }

    
    // Enemy�� ������ �޾��� �� ���� ���ν��� �� ���� �Լ�
    public void EnemyTakeDamage(int _damage)
    {
        // ������Ŭ���̾�Ʈ���� �Է¹��� �������� ���� ������ �����ϰ�
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("�Է¹��� ������ : " +  _damage);
            curHealth -= _damage;
            hpbar.fillAmount = (float)curHealth / (float)maxHealth;

            Debug.Log("���� ü�� : " + curHealth);

            // ����� ���ҵ� ü�°� ü�¹� ���� �Ű������� ApplyHealthUpdate() �޼��忡 ������ 
            // �ش� �۾��� ������ Ŭ���̾�Ʈ������ �����ϰ� �Ѵ�.
            photonView.RPC("ApplyHealthUpdate", RpcTarget.Others, curHealth, hpbar.fillAmount);

            // ���������� ������ Ŭ���̾�Ʈ������ �������� �޴� �޼ҵ带 ���� ��Ų��.
            // �ϴܿ� ����ü���� 0���ϰ� �ɽ� ���ӿ�����Ʈ�� ��Ȱ��ȭ��Ű�� �ൿ�� �����ֱ� ����
            //photonView.RPC("EnemyTakeDamage", RpcTarget.Others, _damage);

        }

        if (curHealth <= 0)
        {
            // �켱 ��Ȱ��ȭ�� �س�����.
            transform.gameObject.SetActive(false);

            // PhotonNetwork.Destroy(gameObject);
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

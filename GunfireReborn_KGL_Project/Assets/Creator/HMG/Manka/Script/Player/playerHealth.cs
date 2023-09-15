using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviourPun
{
    public int maxHealth;       //�ִ�HP
    public int curHealth;       //����HP

    public int ammo;            //��üźâ
    public int remainAmmo;      //����źâ

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //��Ȱ�� ����� HP
        //����HP�� MAXHP/2�� �ʱ�ȭ
        curHealth = maxHealth/2;
    }

    private void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //ó�� ���� �� HP 
        //����HP�� MAXHP�� �ʱ�ȭ
        curHealth = maxHealth;
    }
    //player�� ���ݹ޾����� ���� ���ν��� �� �Լ�
    [PunRPC]
    public void playerTakeDamage(int _damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            curHealth -= _damage;

            if (curHealth <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

}

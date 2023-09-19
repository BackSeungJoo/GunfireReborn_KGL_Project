
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHp : MonoBehaviour
{
    public int maxHealth;       //�ִ�HP
    public int curHealth;       //����HP

    public int ammo;            //��üźâ
    public int remainAmmo;      //����źâ

    private void Awake()
    {

        //��Ȱ�� ����� HP
        //����HP�� MAXHP/2�� �ʱ�ȭ
        curHealth = maxHealth/2;
    }

    private void Start()
    {
        //ó�� ���� �� HP 
        //����HP�� MAXHP�� �ʱ�ȭ
        curHealth = maxHealth;
    }
    //player�� ���ݹ޾����� ���� ���ν��� �� �Լ�
    public void playerTakeDamage(int _damage)
    {
        curHealth -= _damage;

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}

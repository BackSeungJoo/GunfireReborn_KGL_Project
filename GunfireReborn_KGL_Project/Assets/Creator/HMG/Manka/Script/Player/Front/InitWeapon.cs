using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class InitWeapon : MonoBehaviourPun
{
    private WeaponManager player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<WeaponManager>();
        // Front_weapons �迭�� �ڽ� ���� ������Ʈ���� �ֽ��ϴ�.
        for (int i = 0; i < player.Front_weapons.Length; i++)
        {
            Transform child = transform.GetChild(2).GetChild(i); // �ڽ� ���� ������Ʈ ��������
            player.Front_weapons[i] = child.gameObject; // �迭�� �ֱ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

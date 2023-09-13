using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HitBoxActive : MonoBehaviour
{
    public GameObject HitBox;

    private float ticDamage = 0.3f;     // 0.3�ʸ��� �������� ����
    private float timer = 0f;           // ������ Ÿ�̸�

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer < ticDamage)
        {
            HitBox.SetActive(true);
        }

        if (timer > ticDamage)
        {
            HitBox.SetActive(false);
            timer = 0f;
        }
    }
}

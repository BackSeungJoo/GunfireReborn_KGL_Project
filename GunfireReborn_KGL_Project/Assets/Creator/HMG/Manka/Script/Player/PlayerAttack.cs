using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviourPun
{
    public bool isShoot = false;
    public bool useSkill = false;
    public bool isShootPistol = false;

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //Ŭ���� �ʵ忡�ִ� �ٸ����⿡�� �߻簡�Ǵ¹��� , �ʵ�� �÷��̾ �������ִ� ���Ⱑ �ٸ��� ���������.
        if (Input.GetMouseButton(0))
        {
            isShoot = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShoot = false;
            //isShootPistol = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            isShootPistol = true;
        }
        // } ���콺 ��Ŭ��


        // { ���콺 ��Ŭ��
        if (Input.GetMouseButtonDown(1))
        {
            useSkill = true;
        }
        else
        {
            useSkill = false;
        }
    }
}

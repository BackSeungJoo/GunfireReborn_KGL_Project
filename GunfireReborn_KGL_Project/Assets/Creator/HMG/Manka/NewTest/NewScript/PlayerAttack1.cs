
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<<< HEAD:GunfireReborn_KGL_Project/Assets/Creator/HMG/Manka/NewTest/NewScript/PlayerAttack.cs
public class PlayerAttack : MonoBehaviour
========
public class PlayerAttack1 : MonoBehaviour
>>>>>>>> origin/SSC:GunfireReborn_KGL_Project/Assets/Creator/HMG/Manka/NewTest/NewScript/PlayerAttack1.cs
{
    public bool isShoot = false;
    public bool useSkill = false;
    public bool isShootPistol = false;

    // Update is called once per frame
    void Update()
    {
<<<<<<<< HEAD:GunfireReborn_KGL_Project/Assets/Creator/HMG/Manka/NewTest/NewScript/PlayerAttack.cs

========
>>>>>>>> origin/SSC:GunfireReborn_KGL_Project/Assets/Creator/HMG/Manka/NewTest/NewScript/PlayerAttack1.cs
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

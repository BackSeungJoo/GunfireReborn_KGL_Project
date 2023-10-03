using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FrontIK1 : MonoBehaviour
{
    //���� ������
    public Transform FrontweaponPosition;
    //������ �ڽĵ��������ҹ迭
    public GameObject[] FrontWeaponChilds;
    //���� �����ʼ��� ����ġ
    public Transform p_HandLeft;
    public Transform p_HandRight;
    public Animator IKAnimator;


    // Start is called before the first frame update
    void Start()
    {
            FrontWeaponChilds = new GameObject[3];
            IKAnimator = gameObject.GetComponent<Animator>();
            //��������  weaponchilds�� �����ؾ���
            FrontWeaponChilds = new GameObject[FrontweaponPosition.childCount];
            for (int i = 0; i < FrontweaponPosition.childCount; i++)
            {
                FrontWeaponChilds[i] = FrontweaponPosition.GetChild(i).gameObject;
            }
            ChangeIK("Pistol");
    }

    private void OnAnimatorIK(int layerIndex)
    {
            //�޼� �����ϴ��ڵ�
            IKAnimator.SetIKPosition(AvatarIKGoal.LeftHand, p_HandLeft.position);
            IKAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);

            //������ �����ϴ��ڵ�
            IKAnimator.SetIKPosition(AvatarIKGoal.RightHand, p_HandRight.position);
            IKAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
    }

    public void ChangeIK(string weaponName)
    {
        //for ���� ������ ������ �̸��� weaponName�� �����Ͱ�
        //ex) weaponName�� Pistol�ϰ�� Pistol ����������Ʈ���ִ� leftGrap, rightGrap�� p_handLeft �� p_handRight�� ����.
        for (int i = 0; i < FrontWeaponChilds.Length; i++)
        {
            if (FrontWeaponChilds[i].name == weaponName)
            {
                // IK�� ���ߴ� �ڵ� �߰�
                p_HandLeft = FrontWeaponChilds[i].GetComponent<weapon>().leftGrap;
                p_HandRight = FrontWeaponChilds[i].GetComponent<weapon>().rightGrap;
            }
        }
    }

  
}

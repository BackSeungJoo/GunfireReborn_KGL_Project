using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IK1 : MonoBehaviour

{
    //���� ������
    public Transform weaponPosition;
    //������ �ڽĵ��������ҹ迭
    private GameObject[] weaponChilds;
    //���� �����ʼ��� ����ġ
    public Transform p_HandLeft;
    public Transform p_HandRight;
    //���� �ٶ� ������Ʈ
    public Transform targetObj;
    private Animator IKAnimator;

    [Range(0, 1)]
    public float HandIKHandler = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        IKAnimator = GetComponent<Animator>();
        //Todo  ������ �ڽĵ���  weaponchilds�� �����ؾ���
        weaponChilds = new GameObject[weaponPosition.childCount];
        for (int i = 0; i < weaponPosition.childCount; i++)
        {
            weaponChilds[i] = weaponPosition.GetChild(i).gameObject;
        }
        ChangeIK("Pistol");
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnAnimatorIK(int layerIndex)
    {
        //���� �����ٶ󺸰� ������ڵ�
        IKAnimator.SetLookAtWeight(1);
        IKAnimator.SetLookAtPosition(targetObj.position);
        
        //�޼� �����ϴ��ڵ�
        IKAnimator.SetIKPosition(AvatarIKGoal.LeftHand, p_HandLeft.position);
        IKAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, HandIKHandler);
        //IKAnimator.SetIKRotation(AvatarIKGoal.LeftHand, p_HandLeft.rotation);
        //IKAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, HandIKHandler);
        
        //������ �����ϴ��ڵ�
        IKAnimator.SetIKPosition(AvatarIKGoal.RightHand, p_HandRight.position);
        IKAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, HandIKHandler);
        //IKAnimator.SetIKRotation(AvatarIKGoal.RightHand, p_HandRight.rotation);
        //IKAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, HandIKHandler);

    }

    public void ChangeIK(string weaponName)
    {
        //Todo : for ���� ������ weaponposition�� �ڽ���  �̸��� weaponName�� ���� �༮�� ik�� ���ߴ� �ڵ带 �ۼ��ؾ��Ѵ�.
        for (int i = 0; i < weaponChilds.Length; i++)
        {
            if (weaponChilds[i].name == weaponName)
            {
                // IK�� ���ߴ� �ڵ� �߰�
                p_HandLeft = weaponChilds[i].GetComponent<weapon>().leftGrap;
                p_HandRight = weaponChilds[i].GetComponent<weapon>().rightGrap;
            }
        }
    }
}

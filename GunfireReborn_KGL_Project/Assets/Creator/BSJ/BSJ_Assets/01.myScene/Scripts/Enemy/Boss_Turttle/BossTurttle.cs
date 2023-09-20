using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTurttle : MonoBehaviour
{
    // ���� 1
    public GameObject breath;
    int[] pattern01_first_lineAttackTrueOrFalse = new int[5];
    int[] pattern01_second_lineAttackTrueOrFalse = new int[5];
    int[] pattern01_third_lineAttackTrueOrFalse = new int[5];
    int[] pattern01_fourth_lineAttackTrueOrFalse = new int[5];
    int[] pattern01_fifth_lineAttackTrueOrFalse = new int[5];
    public GameObject[] pattern01_warning01;
    public GameObject[] pattern01_warning02;
    public GameObject[] pattern01_warning03;
    public GameObject[] pattern01_warning04;
    public GameObject[] pattern01_warning05;
    public GameObject[] pattern01_fire01;
    public GameObject[] pattern01_fire02;
    public GameObject[] pattern01_fire03;
    public GameObject[] pattern01_fire04;
    public GameObject[] pattern01_fire05;


    // ü��
    // ���� 1 �Ҳ� ������
    // ���� 2 �ٴ����(�Ͻ� ����)
    // ���� 3 �÷��̾� ���� ���� (�Ͻ� ����) || �������� �� �÷��̾�� �������� �ݻ� (�Ѿ� �ݻ�� ����Ʈ�� ó��)

    #region ���� 1

    // ���� 1 _ �극�� Ȱ��ȭ
    public void Pattern01_ActiveBreath()
    {
        breath.SetActive(true);
    }

    // ���� 1 _ �극�� ��Ȱ��ȭ
    public void Pattern01_InActiveBreath()
    {
        breath.SetActive(false);
    }

    // ���� 1 _ 1�� ���� ���� ��� ǥ�� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_ActiveFirst_AttackWarningMark()
    {
        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for(int i = 0; i < pattern01_first_lineAttackTrueOrFalse.Length; i++)
        {
            // ������ ���� ������ ���Ѵ�.
            pattern01_first_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if(pattern01_first_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning01[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 1�� ���� ���� ��� ǥ�� ���� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_InActiveFirst_AttackWarningMark()
    {
        foreach(GameObject warning in pattern01_warning01)
        {
            warning.SetActive(false);
        }
    }

    // ���� 1 _ 1�� ���� ����
    public void Pattern01_Do_FirstAttack()
    {
        for(int i = 0; i < pattern01_first_lineAttackTrueOrFalse.Length; i++)
        {
            if (pattern01_first_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_fire01[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 2�� ���� ���� ��� ǥ�� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_ActiveSecond_AttackWarningMark()
    {
        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_second_lineAttackTrueOrFalse.Length; i++)
        {
            // ������ ���� ������ ���Ѵ�.
            pattern01_second_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_second_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning02[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 2�� ���� ���� ��� ǥ�� ���� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_InActiveSecond_AttackWarningMark()
    {
        foreach (GameObject warning in pattern01_warning02)
        {
            warning.SetActive(false);
        }
    }

    // ���� 1 _ 2�� ���� ����
    public void Pattern01_Do_SecondAttack()
    {
        for (int i = 0; i < pattern01_second_lineAttackTrueOrFalse.Length; i++)
        {
            if (pattern01_second_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_fire02[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 3�� ���� ���� ��� ǥ�� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_ActiveThird_AttackWarningMark()
    {
        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_third_lineAttackTrueOrFalse.Length; i++)
        {
            // ������ ���� ������ ���Ѵ�.
            pattern01_third_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_third_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning03[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 3�� ���� ���� ��� ǥ�� ���� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_InActiveThird_AttackWarningMark()
    {
        foreach (GameObject warning in pattern01_warning03)
        {
            warning.SetActive(false);
        }
    }

    // ���� 1 _ 3�� ���� ����
    public void Pattern01_Do_ThirdAttack()
    {
        for (int i = 0; i < pattern01_third_lineAttackTrueOrFalse.Length; i++)
        {
            if (pattern01_third_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_fire03[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 4�� ���� ���� ��� ǥ�� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_ActiveFourth_AttackWarningMark()
    {
        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_fourth_lineAttackTrueOrFalse.Length; i++)
        {
            // ������ ���� ������ ���Ѵ�.
            pattern01_fourth_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_fourth_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning04[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 4�� ���� ���� ��� ǥ�� ���� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_InActiveFourth_AttackWarningMark()
    {
        foreach (GameObject warning in pattern01_warning04)
        {
            warning.SetActive(false);
        }
    }

    // ���� 1 _ 4�� ���� ����
    public void Pattern01_Do_FourthAttack()
    {
        for (int i = 0; i < pattern01_fourth_lineAttackTrueOrFalse.Length; i++)
        {
            if (pattern01_fourth_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_fire04[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 5�� ���� ���� ��� ǥ�� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_ActiveFifth_AttackWarningMark()
    {
        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_fifth_lineAttackTrueOrFalse.Length; i++)
        {
            // ������ ���� ������ ���Ѵ�.
            pattern01_fifth_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_fifth_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning05[i].SetActive(true);
            }
        }
    }

    // ���� 1 _ 5�� ���� ���� ��� ǥ�� ���� ( �ִϸ��̼� ����Ʈ )
    public void Pattern01_InActiveFifth_AttackWarningMark()
    {
        foreach (GameObject warning in pattern01_warning05)
        {
            warning.SetActive(false);
        }
    }

    // ���� 1 _ 5�� ���� ����
    public void Pattern01_Do_FifthAttack()
    {
        for (int i = 0; i < pattern01_fifth_lineAttackTrueOrFalse.Length; i++)
        {
            if (pattern01_fifth_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_fire05[i].SetActive(true);
            }
        }
    }

    #endregion ���� 1 ��
}

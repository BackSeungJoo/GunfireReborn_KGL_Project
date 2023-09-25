using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossTurttle : MonoBehaviourPun
{
    public bool isIdle;
    public bool isPattern01;
    public bool isPattern02;
    public bool isPattern03;

    public int randomPatternNumber = -1;
    public int setPatternNumber;

    private Animator animator;
    private float patternThinkTime = 5f;
    private float patternThinkTimer = 0f;

    // ���� 1
    public GameObject breath;                                               // ���� 1 �극�� ���� ����
    private int[] pattern01_first_lineAttackTrueOrFalse = new int[5];       // �ش� ���ο��� ������ �� ������ �ƴ��� �Ǵ�
    private int[] pattern01_second_lineAttackTrueOrFalse = new int[5];
    private int[] pattern01_third_lineAttackTrueOrFalse = new int[5];
    private int[] pattern01_fourth_lineAttackTrueOrFalse = new int[5];
    private int[] pattern01_fifth_lineAttackTrueOrFalse = new int[5];

    public GameObject[] pattern01_warning01;                                // ���� 1 ���� �� ��� ǥ��
    public GameObject[] pattern01_warning02;
    public GameObject[] pattern01_warning03;
    public GameObject[] pattern01_warning04;
    public GameObject[] pattern01_warning05;
    public GameObject[] pattern01_fire01;                                   // ���� 1 ����
    public GameObject[] pattern01_fire02;
    public GameObject[] pattern01_fire03;
    public GameObject[] pattern01_fire04;
    public GameObject[] pattern01_fire05;

    // ���� 2
    public List<PhotonView> playerWithTag = new List<PhotonView>();         // ���� �ִ� �÷��̾� ã��
    public GameObject[] pattern02_warnings;                                 // ���� 2 ���� �� ��� ǥ��
    public GameObject[] pattern02_bombs;                                    // ���� 2 ����

    // ���� 3
    public GameObject pattern03_sheild;                                     // ���� 3 ����
    private int[] pattern03_first_AttackTrueOrFalse = new int[5];           // �ش� ���� ��ġ ���� ������ �� ������ �ƴ��� �Ǵ�
    private int[] pattern03_second_AttackTrueOrFalse = new int[5];
    private int[] pattern03_third_AttackTrueOrFalse = new int[5];
    public GameObject[] pattern03_warning01;                                // ���� 3 ���� �� ��� ǥ��
    public GameObject[] pattern03_warning02;
    public GameObject[] pattern03_warning03;
    public GameObject[] pattern03_explosion01;                              // ���� 3 ����
    public GameObject[] pattern03_explosion02;
    public GameObject[] pattern03_explosion03;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
        isIdle = true;
        isPattern01 = false;
        isPattern02 = false;
        isPattern03 = false;
    }

    private void Update()
    {
        if(photonView.IsMine)
        {
            if (isIdle)
            {
                patternThinkTimer += Time.time;

                if (patternThinkTimer > patternThinkTime)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        DecidePattern();
                        photonView.RPC("Client_DecidePattern", RpcTarget.Others, randomPatternNumber);
                    }

                    SetPattern();
                    patternThinkTimer = 0;
                }
            }

            else if (!isIdle)
            {
                if (isPattern01)
                {
                    animator.SetBool("Attack01", true);
                    animator.SetBool("Idle", false);
                }
                if (isPattern02)
                {
                    animator.SetBool("Attack02", true);
                    animator.SetBool("Idle", false);
                }
                if (isPattern03)
                {
                    animator.SetBool("Attack03", true);
                    animator.SetBool("Idle", false);
                }
            }
        }
    }

    public void DecidePattern()
    {
        // ���� ���� (0, 1, 2)
        randomPatternNumber = Random.Range(0, 3);
        setPatternNumber = randomPatternNumber;
    }

    [PunRPC]
    public void Client_DecidePattern(int _randomPatternNumber)
    {
        randomPatternNumber = _randomPatternNumber;
        setPatternNumber = randomPatternNumber;
    }

    public void SetPattern()
    {
        if (setPatternNumber == -1)
        {
            isIdle = true;
            isPattern01 = false;
            isPattern02 = false;
            isPattern03 = false;
        }
        else if (setPatternNumber == 0) { isIdle = false; isPattern01 = true; }
        else if (setPatternNumber == 1) { isIdle = false; isPattern02 = true; }
        else if (setPatternNumber == 2) { isIdle = false; isPattern03 = true; }
    }


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
        // ������ Ŭ���̾�Ʈ�� ���� ��
        int[] master_Pattern01_first_lineAttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_first_lineAttackTrueOrFalse.Length; i++)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern01_first_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern01_Deside_FirstAttack", RpcTarget.All, i, master_Pattern01_first_lineAttackTrueOrFalse[i]);
            }

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if(pattern01_first_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning01[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern01_Deside_FirstAttack(int index, int _master_Pattern01_first_lineAttackTrueOrFalse)
    {
        pattern01_first_lineAttackTrueOrFalse[index] = _master_Pattern01_first_lineAttackTrueOrFalse;
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
        // ������ Ŭ���̾�Ʈ ���� ��
        int[] master_Pattern01_second_lineAttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_second_lineAttackTrueOrFalse.Length; i++)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern01_second_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern01_Deside_SecondAttack", RpcTarget.All, i, master_Pattern01_second_lineAttackTrueOrFalse[i]);
            }

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_second_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning02[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern01_Deside_SecondAttack(int index, int _master_Pattern01_second_lineAttackTrueOrFalse)
    {
        pattern01_second_lineAttackTrueOrFalse[index] = _master_Pattern01_second_lineAttackTrueOrFalse;
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
        // ������ Ŭ���̾�Ʈ ���� ��
        int[] master_Pattern01_third_lineAttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_third_lineAttackTrueOrFalse.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern01_third_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern01_Deside_ThirdAttack", RpcTarget.All, i, master_Pattern01_third_lineAttackTrueOrFalse[i]);
            }  

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_third_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning03[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern01_Deside_ThirdAttack(int index, int _master_Pattern01_third_lineAttackTrueOrFalse)
    {
        pattern01_third_lineAttackTrueOrFalse[index] = _master_Pattern01_third_lineAttackTrueOrFalse;
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
        // ������ Ŭ���̾�Ʈ ���� ��
        int[] master_Pattern01_fourth_lineAttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_fourth_lineAttackTrueOrFalse.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern01_fourth_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern01_Deside_FourthAttack", RpcTarget.All, i, master_Pattern01_fourth_lineAttackTrueOrFalse[i]);
            }

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_fourth_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning04[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern01_Deside_FourthAttack(int index, int _master_Pattern01_fourth_lineAttackTrueOrFalse)
    {
        pattern01_fourth_lineAttackTrueOrFalse[index] = _master_Pattern01_fourth_lineAttackTrueOrFalse;
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
        // ������ Ŭ���̾�Ʈ ���� ��
        int[] master_Pattern01_fifth_lineAttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern01_fifth_lineAttackTrueOrFalse.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern01_fifth_lineAttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern01_Deside_FifthAttack", RpcTarget.All, i, master_Pattern01_fifth_lineAttackTrueOrFalse[i]);
            }

            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern01_fifth_lineAttackTrueOrFalse[i] == 1)
            {
                pattern01_warning05[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern01_Deside_FifthAttack(int index, int _master_Pattern01_fifth_lineAttackTrueOrFalse)
    {
        pattern01_fifth_lineAttackTrueOrFalse[index] = _master_Pattern01_fifth_lineAttackTrueOrFalse;
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

    #region ���� 2
    // ��� �÷��̾��� ��ġ�� ���Ѵ�.
    public void Pattern02_FindAllPlayers()
    {
        PhotonView[] players = FindObjectsOfType<PhotonView>();
        playerWithTag = new List<PhotonView>();

        foreach (PhotonView player in players)
        {
            if (player.gameObject.CompareTag("Player"))
            {
                playerWithTag.Add(player);
            }
        }
    }

    // �װ��� ��ź ��� Ȱ��ȭ�ϰ�,
    public void Pattern02_Active_BoomWarningMark()
    {
        for(int i = 0; i < playerWithTag.Count; i++)
        {
            pattern02_warnings[i].transform.position = new Vector3 (playerWithTag[i].transform.position.x, -5, playerWithTag[i].transform.position.z);
            pattern02_warnings[i].SetActive(true);
        }
    }

    // ��ź ��� ��Ȱ��ȭ�Ѵ�.
    public void Pattern02_InActive_BoomWarningMark()
    {
        foreach(GameObject warning in pattern02_warnings)
        {
            warning.SetActive(false);
        }
    }

    // ��� �ڿ� ���߽�Ų��.
    public void Pattern02_Do_BoomAttack()
    {
        for (int i = 0; i < playerWithTag.Count; i++)
        {
            pattern02_bombs[i].transform.position = pattern02_warnings[i].transform.position;
            pattern02_bombs[i].SetActive(true);
        }
    }

    // ���� ��
    public void Pattern02_AttackEnd()
    {
        foreach (GameObject boom in pattern02_bombs)
        {
            boom.SetActive(false);
        }
    }
    #endregion

    #region ���� 3
    // ������ ���� ���� Ȱ��ȭ
    public void Pattern03_Active_Sheild()
    {
        pattern03_sheild.SetActive(true);
    }

    // ������ ���� ���� ��Ȱ��ȭ
    public void Pattern03_InActive_Sheild()
    {
        pattern03_sheild.SetActive(false);
    }

    // ���� 3 ����01 ǥ��
    public void Pattern03_Active_FirstExplosionWarningMark()
    {
        // ������ Ŭ���̾�Ʈ�� ���� ��
        int[] master_Pattern03_first_AttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern03_first_AttackTrueOrFalse.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern03_first_AttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern03_Deside_FirstAttack", RpcTarget.All, i, master_Pattern03_first_AttackTrueOrFalse[i]);
            }
                
            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern03_first_AttackTrueOrFalse[i] == 1)
            {
                pattern03_warning01[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern03_Deside_FirstAttack(int index, int _master_Pattern01_first_lineAttackTrueOrFalse)
    {
        pattern03_first_AttackTrueOrFalse[index] = _master_Pattern01_first_lineAttackTrueOrFalse;
    }


    // ���� 3 ����01 ǥ�� ��Ȱ��ȭ
    public void Pattern03_InActive_FirstExplosionWarningMark()
    {
        foreach (GameObject warning in pattern03_warning01)
        {
            warning.SetActive(false);
        }
    }

    // ���� 3 ���� 01 ����
    public void Pattern03_Do_FirstAttack()
    {
        for (int i = 0; i < pattern03_first_AttackTrueOrFalse.Length; i++)
        {
            if (pattern03_first_AttackTrueOrFalse[i] == 1)
            {
                pattern03_explosion01[i].SetActive(true);
            }
        }
    }

    // ���� 3 ���� 01 ���� ��
    public void Pattern03_End_FirstAttack()
    {
        foreach(GameObject explosion in pattern03_explosion01)
        {
            explosion.SetActive(false);
        }
    }


    // ���� 3 ����02 ǥ��
    public void Pattern03_Active_SecondExplosionWarningMark()
    {
        // ������ Ŭ���̾�Ʈ�� ���� ��
        int[] master_Pattern03_second_AttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern03_second_AttackTrueOrFalse.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern03_second_AttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern03_Deside_SecondAttack", RpcTarget.All, i, master_Pattern03_second_AttackTrueOrFalse[i]);
            }
                
            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern03_second_AttackTrueOrFalse[i] == 1)
            {
                pattern03_warning02[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern03_Deside_SecondAttack(int index, int _master_Pattern01_second_lineAttackTrueOrFalse)
    {
        pattern03_second_AttackTrueOrFalse[index] = _master_Pattern01_second_lineAttackTrueOrFalse;
    }


    // ���� 3 ����02 ǥ�� ��Ȱ��ȭ
    public void Pattern03_InActive_SecondExplosionWarningMark()
    {
        foreach (GameObject warning in pattern03_warning02)
        {
            warning.SetActive(false);
        }
    }

    // ���� 3 ���� 02 ����
    public void Pattern03_Do_SecondAttack()
    {
        for (int i = 0; i < pattern03_second_AttackTrueOrFalse.Length; i++)
        {
            if (pattern03_second_AttackTrueOrFalse[i] == 1)
            {
                pattern03_explosion02[i].SetActive(true);
            }
        }
    }

    // ���� 3 ���� 02 ���� ��
    public void Pattern03_End_SecondAttack()
    {
        foreach (GameObject explosion in pattern03_explosion02)
        {
            explosion.SetActive(false);
        }
    }

    // ���� 3 ����03 ǥ��
    public void Pattern03_Active_ThirdExplosionWarningMark()
    {
        // ������ Ŭ���̾�Ʈ�� ���� ��
        int[] master_Pattern03_third_AttackTrueOrFalse = new int[5];

        // ������ �� ���� [ 0 == �������� ���� / 1 == ������ ]
        for (int i = 0; i < pattern03_third_AttackTrueOrFalse.Length; i++)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                // ������ ���� ������ ���Ѵ�.
                master_Pattern03_third_AttackTrueOrFalse[i] = Random.Range(0, 2);

                // RPC�� �������� ���Ѱ� ����
                photonView.RPC("Pattern03_Deside_ThirdAttack", RpcTarget.All, i, master_Pattern03_third_AttackTrueOrFalse[i]);
            }
                
            // �����ϴ� ������ �������ٸ� ��� Ȱ��ȭ�Ѵ�.
            if (pattern03_third_AttackTrueOrFalse[i] == 1)
            {
                pattern03_warning03[i].SetActive(true);
            }
        }
    }

    [PunRPC]
    public void Pattern03_Deside_ThirdAttack(int index, int _master_Pattern01_third_lineAttackTrueOrFalse)
    {
        pattern03_third_AttackTrueOrFalse[index] = _master_Pattern01_third_lineAttackTrueOrFalse;
    }

    // ���� 3 ����03 ǥ�� ��Ȱ��ȭ
    public void Pattern03_InActive_ThirdExplosionWarningMark()
    {
        foreach (GameObject warning in pattern03_warning03)
        {
            warning.SetActive(false);
        }
    }

    // ���� 3 ���� 03 ����
    public void Pattern03_Do_ThirdAttack()
    {
        for (int i = 0; i < pattern03_third_AttackTrueOrFalse.Length; i++)
        {
            if (pattern03_third_AttackTrueOrFalse[i] == 1)
            {
                pattern03_explosion03[i].SetActive(true);
            }
        }
    }

    // ���� 3 ���� 03 ���� ��
    public void Pattern03_End_ThirdAttack()
    {
        foreach (GameObject explosion in pattern03_explosion03)
        {
            explosion.SetActive(false);
        }
    }
    #endregion

    // ���� ����
    public void Pattern_Exit()
    {
        setPatternNumber = -1;

        isIdle = true;
        isPattern01 = false;
        isPattern02 = false;
        isPattern03 = false;
        animator.SetBool("Idle", true);
        animator.SetBool("Attack01", false);
        animator.SetBool("Attack02", false);
        animator.SetBool("Attack03", false);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class playerHp : MonoBehaviourPun
{
    public int maxHealth;       //�ִ�HP
    public int curHealth;       //����HP
    public int maxShield;       //�ִ�shield;
    public int curShield;       //����shield;

    public bool isturnGroggy;    //�׷α���°��ƴ��� Ȯ���ϴº���
    public bool isturnPlay;      //�÷��̻��°��ƴ��� Ȯ���ϴ� ����

    public Slider hpBar;                        //�÷���hp��
    public Slider shieldBar;                    //�÷��̽����
    private Animator animator;                  //�÷��̾� �ִϸ�����
    private IK1 ik;                             //3��Ī ik�� �޾ƿ��� ����
    private CinemachineVirtualCamera virtualCam;//�� �ó׸ӽ��� �޾ƿ��º���, //���̸� �������� ķ
    private PlayerRoation roation;              //�÷��̾��� ȸ���� ����ϴ½�ũ��Ʈ;
    public Transform weapon;                    //3��Ī�÷��̾��� �����
    private FrontIK1 frontIK;                   //1��Ī ik�� �޾ƿ��� ����
    public Transform frontWeapon;               //1��Ī �÷����� �����

    public GameObject recoveryBarOB;            //ȸ���� false true�� �ٲٰ� �ϱ����ؼ�
    public Image recoveryBar;                   //ȸ���� Slider ������������� ���̰��� ��
    private float dieSpeed = 1f;                 //�׾�¼ӵ�
    private float recoverySpeed = 1;             //ȸ���ӵ�
    private float currentRecovery = 1.0f;       //���� ȸ�����൵
    public RaycastHit hitInfo;                  //���������� ��������
    private float rayDistance = 5f;             //������ �����Ÿ�
    public bool isCure;                         //ȸ�������� üũ�ϴ� ����
    public List<GameObject> otherPlayers;       //�� �ֺ��� �ִ� �÷��̾���� ���� ����
    private GameObject hPlayer;                 //���� ȸ����Ű���ִ� �÷��̾ ���� ����

    public enum State
    {
        play,
        groggy,
        die
    }

    public State state;
    private void Start()
    {
        //�ʱ�ȭ
        roation = GetComponent<PlayerRoation>();                                //ȸ����ũ���� �޾ƿ���
        hpBar = GameObject.Find("HpBgBar").GetComponent<Slider>();              //hp�� �޾ƿ���
        shieldBar = GameObject.Find("ShieldBgBar").GetComponent<Slider>();      //shield�� �޾ƿ���
        virtualCam = FindObjectOfType<CinemachineVirtualCamera>();              //�����ķ �޾ƿ���
        ik = GetComponent<IK1>();                                               //3��Īik �޾ƿ���
        animator = GetComponent<Animator>();                                    //�� �ִϸ����� �޾ƿ���
        frontIK = GetComponent<FrontIK1>();                                     //1��Īik �޾ƿ���
       
        state = State.play;                                                     //���� ���¸� play�� �س���
        curHealth = maxHealth;                                                  //����HP�� MAXHP�� �ʱ�ȭ
        curShield = maxShield;                                                  //���罯�带 MAXShield�� �ʱ�ȭ
        hpBar.value = (float)curHealth / (float)maxHealth;                      //hp���� �ʱ�ȭ
        shieldBar.value = (float)curShield / (float)maxShield;                  //Shield���� �ʱ�ȭ

        otherPlayers = new List<GameObject>();                                  //�ֺ����÷��̾���� ���� ����Ʈ
        recoveryBar.fillAmount = 1;                                             //��Ŀ������ 
        currentRecovery = 0.99f;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (state == State.groggy)
        {
            Debug.Log("����ǰ��̽�?");
            photonView.RPC("GoingDead", RpcTarget.All);
        }
        //RPC�� ��������ϴºκ�
        photonView.RPC("StateUpdate", RpcTarget.All, curHealth, curShield);     //���¸� ������Ʈ�ϴ� �Լ�,hp,shield,�ִϸ������Ķ����,
        //RPC�� ���������ʾƵ��ȴ�, �����ָ�ȵǴ� �κ�
        if (state == State.groggy)                              // �׷α���¶�� 
        {
            animator.SetBool("groggy", true);                   // �ִϸ����� �Ķ���͸� groggy�� ���� groggy�ִϸ��̼��� ����ǰԸ�����
        }
        else                                                    // �÷��̻��¶��
        {
            animator.SetBool("groggy", false);                  // �ִϸ����� �Ķ���͸� groggy�� ���� groggy�ִϸ��̼��� ��������ʰԸ�����
            ShotRayCast();                                      // �÷������̶�� rayCast�� ��Ը�����
        }
        if (state == State.die)                                 // �������¶�� 
        {
            animator.SetTrigger("Dead");                        // ���� �ִϸ��̼��� ����ǰ���
        }
    }
    
    #region �ǰ��Լ�
    //player�� ���ݹ޾����� ���� ���ν��� �� �Լ�
    public void PlayerTakeDamage(int damage)
    {
        Debug.LogFormat("�׾��~");
        //���尡 �����ִٸ� ���尡 ���̰� �ϰ� ���尡 0�̰ų� ���϶�� hp�� ���̰���
        if (curShield <= 0)
        {
            curHealth -= damage;
        }
        else
        {
            curShield -= damage;
        }
        //���� hp�� 0���� ���Եȴٸ� �÷��̾ �׷α���·θ�����
        if (curHealth <= 0)
        {
            state = State.groggy;
            TurnGroggy();
        }
        //�����Ʈ��ũ������ Ŭ���̾�Ʈ��   ����� hp�� �ٸ������� ���� �����Ŵ
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("PlayerHealthUpdated", RpcTarget.Others, curHealth, curShield);
        }
    }
    #endregion
    #region ���¾�����Ʈ�Լ�
    [PunRPC]
    private void StateUpdate(int newHealth, int newShield)
    {
        //hp�ٿ� shield�ٸ� ���������� ��������
        shieldBar.value = (float)curShield / (float)maxShield;
        hpBar.value = (float)curHealth / (float)maxHealth;

        curHealth = newHealth;
        curShield = newShield;

        if (state == State.groggy)                              // �׷α���¶�� 
        {
            Debug.LogFormat("access complete");
            roation.enabled = false;                            // �÷��̾��� �����̼��� ������
            ik.enabled = false;                                 // ik�ִϸ����͸� ������
            recoveryBarOB.SetActive(true);                            // ť�� �ٸ� Ȱ��ȭ��Ŵ
        }
        else                                                    // �׷α���°� �ƴ϶��
        {
            roation.enabled = true;                             // �÷��̾��� �����̼��� �ѹ���
            ik.enabled = true;                                  // ik�ִϸ����͸� ������
            recoveryBarOB.SetActive(false);                           // ť�� �ٸ� Ȱ��ȭ��Ŵ
        }
    }
    #endregion
    #region ���⸦ ��Ȱ��ȭ�ϴ��Լ�
    private void FalseWeapons()
    {
        for (int i = 0; i < weapon.childCount; i++)
        {
            weapon.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void FalseFrontWeapons()
    {
        for (int i =0; i < frontWeapon.childCount; i++)
        {
            frontWeapon.GetChild(i).gameObject.SetActive(false);
        }
    }
    #endregion
    #region �׷α���·� ���ϴ� �Լ���, �÷��̻��·� ���ϴ� �Լ�
    private void TurnGroggy()
    {
        isturnGroggy = true;
        virtualCam.Follow = gameObject.transform;           // �����ī�޶��� Follow�� LookAt�� ���� ��������
        virtualCam.LookAt = gameObject.transform;           // �̰� ������ ������ �ֳĸ� StateUpdate�� Rpc�� �ֱ������� ����ϴµ� �ٸ������� ���� �����ī�޶� ���⶧���̴�.

        virtualCam.transform.parent = null;

        virtualCam.AddCinemachineComponent<CinemachineOrbitalTransposer>();
        CinemachineOrbitalTransposer v = virtualCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();

        v.m_FollowOffset.y = 1f;                            // ī�޶� ����
        v.m_FollowOffset.x = 1f;
        v.m_FollowOffset.z = 1.5f;
        v.m_XAxis.m_MaxSpeed = 1000;

        roation.enabled = false;                            // ȸ����ũ��Ʈ�� false�� �����.

        FalseWeapons();                                     // 3��Ī ���� ��� false�� �����
        ik.enabled = false;                                 // 3��Ī ik false�� �����
        frontIK.enabled = false;                            // 1��Ī ik false�� �����
        FalseFrontWeapons();                                // 1��Ī ���� ��� false�� �����
    }

    public void TurnPlay()
    {
        virtualCam.Follow = null;                           // �����ī�޶��� Follow�� LookAt�� ���� ��������
        virtualCam.LookAt = null;                           // �̰� ������ ������ �ֳĸ� StateUpdate�� Rpc�� �ֱ������� ����ϴµ� �ٸ������� ���� �����ī�޶� ���⶧���̴�.
        virtualCam.transform.parent = transform.transform;  // ī�޶� �ٽ� ĳ������ ���� ������Ʈ�� �ִ´�.
        virtualCam.DestroyCinemachineComponent<CinemachineOrbitalTransposer>();
        roation.enabled = true;
        virtualCam.transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.8f, transform.localPosition.z + 0.4f);
        isturnPlay = true;
    }
    #endregion
    #region �׾���Լ�
    [PunRPC]
    private void GoingDead()
    {
        if (isCure == false)
        {
            
            currentRecovery -= dieSpeed * Time.deltaTime;
            if (currentRecovery < 0)
            {
                currentRecovery = 0;
            }
            recoveryBar.fillAmount = currentRecovery/1;
            Debug.LogFormat("{0}", currentRecovery);
        }
    }
    #endregion
    #region Ʈ���� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            otherPlayers.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            otherPlayers.Remove(other.gameObject);
        }
    }
    #endregion
    #region �������ִ� �÷��̾ �ִ��� �Ǻ��ϴ� �Լ�
    private int SearchPlayer()
    {
        int peopleNum = 0;
        for (int i = 0; i < otherPlayers.Count; i++)
        {
            if (otherPlayers[i] != null)
            {
                peopleNum++;
            }
        }
        if (peopleNum == 0)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
    #endregion
    #region ġ���ϴ� �Լ�
    [PunRPC]
    private void CureOther()
    {
        //���� ġ���� ���θ� ��Ÿ���� ������ true�� ������ 
        playerHp otherHP = hPlayer.GetComponent<playerHp>();
        isCure = true;
        otherHP.currentRecovery += otherHP.recoverySpeed * Time.deltaTime;
        otherHP.recoveryBar.fillAmount = otherHP.currentRecovery;
        if (otherHP.currentRecovery >= 1.0f)
        {
            // ȸ���� �Ϸ�Ǹ� ��Ȱ ������ ȣ���ϰų� �ٸ� �ʿ��� �۾��� �����մϴ�.
            otherHP.curHealth = otherHP.maxHealth / 2;
            isCure = false;
            hPlayer = null;
            //�÷��̾��� ������Ʈ�� play���Ǹ� playerCure�� �����Ƿ� �ݵ�� ���� �������� �ξ���Ѵ�.
            otherHP.state = playerHp.State.play;
            otherHP.TurnPlay();
        }
    }
    #endregion
    #region �߰��� ġ�Ḧ ���⶧ �Լ�
    [PunRPC]

    private void StopCure()
    {
        isCure = false;
    }
    #endregion
    #region ���̸� ����Լ�
    private void ShotRayCast()
    {
        if (SearchPlayer() == 2)
        {//�ֺ��� �÷��̾ �������
         //���⼭ ����ĳ��Ʈ�� �߻��ؼ� �׷α� ������ �÷��̾��ϰ�� cureOther()�� ����ϰԸ�����\
            if (Physics.Raycast(virtualCam.transform.position, virtualCam.transform.forward, out hitInfo, rayDistance))
            {   //���̰� �浹�Ѱ�� hitInfo�� �浹 ������ �޾ƿ´�.
                Debug.DrawRay(virtualCam.transform.position, virtualCam.transform.forward * rayDistance, Color.blue);
                if (hitInfo.transform.CompareTag("Player") && hitInfo.collider.gameObject.GetComponent<playerHp>().state == playerHp.State.groggy)
                {   //������ü�� Tag�� �÷��̾�� ���� �÷��̾ �׷α� ���¶��
                    if (Input.GetKey("Get"))
                    {   //f��������������
                        hPlayer = hitInfo.collider.gameObject;
                        //���� ���̸� ���� �÷��̾ ȸ���ϰ��ִ� �÷��̾� ������ �����Ѵ�.
                        photonView.RPC("CureOther", RpcTarget.All);
                        //cureOther�Լ��� RPC�Ѵ�.
                    }
                    else
                    {
                        photonView.RPC("StopCure", RpcTarget.All);
                        //ȸ�� �ߴ� �� ȸ�� ���¸� �ʱ�ȭ�մϴ�.
                    }
                }
            }
            else
            {//���� ���̰� �÷��̾ ����ٸ� ����ġ�������� ��Ÿ���� ������ false�� �����.
                isCure = false;
            }
        }
    }
    #endregion
}

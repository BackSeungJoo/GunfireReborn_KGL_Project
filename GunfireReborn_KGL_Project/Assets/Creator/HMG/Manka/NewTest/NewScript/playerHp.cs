
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class playerHp : MonoBehaviourPun //,IPunObservable
{
    private int maxHealth;       //�ִ�HP
    private int curHealth;       //����HP
    private int maxShield;       //�ִ�shield;
    private float curShield;       //����shield;

    public bool isturnGroggy;    //�׷α���°��ƴ��� Ȯ���ϴº���
    public bool isturnPlay;      //�÷��̻��°��ƴ��� Ȯ���ϴ� ����

    private Image hpBar;                       //�÷���hp��
    private Image shieldBar;                   //�÷��̽����
    private Animator animator;                  //�÷��̾� �ִϸ�����
    private IK1 ik;                             //3��Ī ik�� �޾ƿ��� ����
    private CinemachineVirtualCamera virtualCam;//�� �ó׸ӽ��� �޾ƿ��º���, //���̸� �������� ķ
    private PlayerRoation roation;              //�÷��̾��� ȸ���� ����ϴ½�ũ��Ʈ;
    public Transform weapon;                    //3��Ī�÷��̾��� �����
    private GameObject FPSUnityChan;            //1��Ī����Ƽ¯
    public GameObject recoveryBarOB;            //ȸ���� false true�� �ٲٰ� �ϱ����ؼ�
    public Image recoveryBar;                   //ȸ���� Slider ������������� ���̰��� ��
    private float dyingTime = 20.0f;            //�״µ� �ɸ��½ð�
    private float dieSpeed = 1f;                //�׾�¼ӵ�
    private float cureTime = 5f;                //�ϴµ� �ɸ��½ð�
    private float recoverySpeed = 1;            //ȸ���ϴ¼ӵ�
    public RaycastHit hitInfo;                  //���������� ��������
    private float rayDistance = 5f;             //������ �����Ÿ�
    public bool isCure;                         //ȸ�������� üũ�ϴ� ����
    private GameObject hPlayer;                 //���� ȸ����Ű���ִ� �÷��̾ ���� ����
    private bool isDead;                        //������ üũ�ϴ� ����

    private float shieldRecharge = 10f;     //1�ʸ��� ȸ���� ���差
    private float shieldRechargeCool = 5f;      //����ȸ�� ���� ��Ÿ��
    private float rechargeTimer = 0;            //����ȸ�� Ÿ�̸� �ʱ�ȭ

    playerHp otherHP;

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
        hpBar = GameObject.Find("HPBar").GetComponent<Image>();              //hp�� �޾ƿ���
        shieldBar = GameObject.Find("ShieldBar").GetComponent<Image>();      //shield�� �޾ƿ���
        virtualCam = FindObjectOfType<CinemachineVirtualCamera>();              //�����ķ �޾ƿ���
        ik = GetComponent<IK1>();                                               //3��Īik �޾ƿ���
        animator = GetComponent<Animator>();                                    //�� �ִϸ����� �޾ƿ���
        FPSUnityChan = Camera.main.transform.GetChild(0).gameObject;               //1��Īik �޾ƿ���

        maxHealth = 100;
        maxShield = 100;
        state = State.play;                                                     //���� ���¸� play�� �س���
        curHealth = maxHealth;                                                  //����HP�� MAXHP�� �ʱ�ȭ
        curShield = maxShield;                                                  //���罯�带 MAXShield�� �ʱ�ȭ
        hpBar.fillAmount = (float)curHealth / (float)maxHealth;                      //hp���� �ʱ�ȭ
        shieldBar.fillAmount = (float)curShield / (float)maxShield;                  //Shield���� �ʱ�ȭ

        recoveryBar.fillAmount = 1;                                             //��Ŀ������ 1�� �ʱ�ȭ
        isturnGroggy = true;
        isturnPlay = true;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        // ���� ȸ�� Ÿ�̸Ӹ� ������Ŵ
        rechargeTimer += Time.deltaTime;

        // 5�� ��� �� ���� ȸ��
        if (rechargeTimer >= shieldRechargeCool)
        {
            //Todo : 1�ʸ��� ���� 10�� ȸ���ϰԸ����
            curShield += Time.deltaTime * shieldRecharge;
        }
        // ���� �ִ밪 ����
        curShield = Mathf.Clamp(curShield, 0, maxShield); // maxShieldValue�� ���� �ִ밪

        if (state == State.groggy)
        {   //�׷α� ���¿����� �׾���Լ��� ����Ѵ�.
            if (isturnGroggy == false)
            {
                roation.enabled = false;                                            // �÷��̾��� �����̼��� ������
                photonView.RPC("MakeTrueRecoveryBar", RpcTarget.All);                // ť�� �ٸ� Ȱ��ȭ��Ŵ
                isturnGroggy = true;
            }
            photonView.RPC("GoingDead", RpcTarget.All);
        }
        StateUpdate();     //���¸� ������Ʈ�ϴ� �Լ�,hp,shield,�ִϸ������Ķ����,
        //RPC�� ���������ʾƵ��ȴ�, �����ָ�ȵǴ� �κ�
        if (state == State.groggy)                              // �׷α���¶�� 
        {
            animator.SetBool("groggy", true);                   // �ִϸ����� �Ķ���͸� groggy�� ���� groggy�ִϸ��̼��� ����ǰԸ�����
        }
        if (state == State.play)                                // �÷��̻��¶��
        {
            
            animator.SetBool("groggy", false);                  // �ִϸ����� �Ķ���͸� groggy�� ���� groggy�ִϸ��̼��� ��������ʰԸ�����
            if(isturnPlay == false)
            {
                TurnPlay();
                roation.enabled = true;                         // �÷��̾��� �����̼��� �ѹ���
                photonView.RPC("MakeFalseRecoveryBar", RpcTarget.All);               // ť�� �ٸ� Ȱ��ȭ��Ŵ
            }
            ShotRayCast();                                      // �÷������̶�� rayCast�� ��Ը�����
        }
        if (state == State.die)                                 // �������¶�� 
        {
            if (isDead == false)
            {
                animator.SetTrigger("Dead");                        // ���� �ִϸ��̼��� ����ǰ���
                
                isDead = true;
            }
            ik.enabled = false;
        }
    }

    #region �ǰ��Լ�
    //player�� ���ݹ޾����� ���� ���ν��� �� �Լ�
    //[PunRPC]
    public void PlayerTakeDamage(int damage)
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            if (curShield <= 0)
            {
                curHealth -= damage;
            }
            else
            {
                curShield -= damage;
            }
            rechargeTimer = 0;
            photonView.RPC("PlayerHealthUpdated", RpcTarget.All, curHealth, curShield, rechargeTimer);
           
        }
        //���尡 �����ִٸ� ���尡 ���̰� �ϰ� ���尡 0�̰ų� ���϶�� hp�� ���̰���
        //���� hp�� 0���� ���Եȴٸ� �÷��̾ �׷α���·θ�����
       
        //�����Ʈ��ũ������ Ŭ���̾�Ʈ�� ����� hp�� �ٸ������� ���� �����Ŵ
    }
    #endregion
    [PunRPC]
    private void TurnStateGroggy()
    {
        state = State.groggy;
    }


    [PunRPC]
    private void PlayerHealthUpdated(int newCurHealth, float newCurShield,float rechargeTime)
    {
        curHealth = newCurHealth;
        curShield = newCurShield;
        rechargeTimer = rechargeTime;
        if (curHealth <= 0)
        {
            photonView.RPC("TurnStateGroggy", RpcTarget.All);
            isturnGroggy = false;
            isturnPlay = false;
            Debug.Log("BeforeTurn");
            TurnGroggy();
            Debug.Log("AfterTurn");
            photonView.RPC("TagChangeGroggy", RpcTarget.All);
        }
    }

    #region ���¾�����Ʈ�Լ�
    private void StateUpdate()
    {

        shieldBar.fillAmount = (float)curShield / (float)maxShield;
        hpBar.fillAmount = (float)curHealth / (float)maxHealth;

        if (state == State.groggy)                              // �׷α���¶�� 
        {
            
        }
        else                                                    // �׷α���°� �ƴ϶��
        {
            
        }
    }
    #endregion
    #region ���⸦ ��Ȱ��ȭ�ϴ��Լ�
    [PunRPC]
    private void FalseWeapons()
    {
        for (int i = 0; i < weapon.childCount; i++)
        {
            weapon.GetChild(i).gameObject.SetActive(false);
        }
    }
    #endregion
    [PunRPC]
    private void TagChangeGroggy()
    {
        gameObject.tag = "Groggy";
    }
    
    #region �׷α���·� ���ϴ� �Լ���, �÷��̻��·� ���ϴ� �Լ�
    private void TurnGroggy()
    {
        Debug.Log("Turn1");
        if(!photonView.IsMine)
        {
            return;
        }
        Debug.Log("Turn2");
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

        photonView.RPC("FalseWeapons", RpcTarget.All);      // 3��Ī ���� ��� false�� �����
        ik.enabled = false;                                 // 3��Ī ik false�� �����
        FPSUnityChan.SetActive(false);                      // 1��Ī ����Ƽ¯ ����
    }

    public void TurnPlay()
    {
        curHealth = maxHealth / 2;
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
            // 10�� ���� �����ؾ� �� ���� ����մϴ�.
            float decreaseAmount = 1.0f / dyingTime * dieSpeed * Time.deltaTime;

            
            // fillAmount�� ������Ʈ�մϴ�.
            recoveryBar.fillAmount = Mathf.Max(recoveryBar.fillAmount - decreaseAmount, 0);
            // fillAmount�� 0�̸� �ʿ��� ó���� �����մϴ�.
            if (recoveryBar.fillAmount == 0)
            {
                //�������� ó����.
                state = State.die;
            }
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
        if (Physics.Raycast(virtualCam.transform.position, virtualCam.transform.forward, out hitInfo, rayDistance))
        {   //���̰� �浹�Ѱ�� hitInfo�� �浹 ������ �޾ƿ´�.

            //Debug.LogFormat("���� ��ü : {0}" , hitInfo.collider.gameObject.name);
            Debug.DrawRay(virtualCam.transform.position, virtualCam.transform.forward * rayDistance, Color.black);
            //playerHp test = hPlayer.GetComponent<playerHp>();
            if (hitInfo.transform.CompareTag("Groggy"))
            {
                //������ü�� Tag�� �׷α��(�÷��̾��Ͻ� ���Ͱ� ��� ������ ��鸲) ���� �÷��̾ �׷α� ���¶��
                if (Input.GetButton("Get"))
                {   //f��������������
                    hPlayer = hitInfo.collider.gameObject;
                    Debug.Log("Ŭ���� �±� : " + hPlayer.tag);
                    //���� ���̸� ���� �÷��̾ ȸ���ϰ��ִ� �÷��̾� ������ �����Ѵ�.

                    hPlayer.GetComponent<playerHp>().photonView.RPC("Cure", RpcTarget.All);
                    //cureOther�Լ��� �����Ѵ�.
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
    #endregion


    [PunRPC]
    private void MakeTrueRecoveryBar()
    {
        recoveryBarOB.SetActive(true);
        ik.NullIK();
    }

    [PunRPC]
    private void MakeFalseRecoveryBar()
    {
        recoveryBarOB.SetActive(false);
        ik.ChangeIK("Pistol");
    }
    [PunRPC]
    private void Cure()
    {
        // ���� ġ���� ���θ� ��Ÿ���� ������ true�� ������ 
        if (PhotonNetwork.IsMasterClient)
        {
            otherHP = GetComponent<playerHp>();
            isCure = true;
            // 5�� ���� �����ؾ� �� ���� ����մϴ�.
            float increaseAmount = 1.0f / cureTime * recoverySpeed * Time.deltaTime;
            // fillAmount�� ������Ʈ�մϴ�.
            otherHP.recoveryBar.fillAmount = Mathf.Max(recoveryBar.fillAmount + increaseAmount, 0);
            photonView.RPC("UpdateRecoveryBar", RpcTarget.All,otherHP.recoveryBar.fillAmount);
            if (otherHP.recoveryBar.fillAmount >= 1.0f)
            {
                // �÷��̾��� ������Ʈ�� play���Ǹ� playerCure�� �����Ƿ� �ݵ�� ���� �������� �ξ���Ѵ�.
                photonView.RPC("ChangeTagState", RpcTarget.All);
                photonView.RPC("TrueWeapons",RpcTarget.All);
                isCure = false;
            }
        }
    }
    [PunRPC]
    private void UpdateRecoveryBar(float fillAmount)
    {
        recoveryBar.fillAmount = fillAmount;
    }

    [PunRPC]

    private void ChangeTagState()
    {
        gameObject.tag = "Player";
        otherHP.state = State.play;
        gameObject.GetComponent<Animator>().SetBool("groggy", false);
    }

    [PunRPC]
    private void TrueWeapons()
    {
        weapon.GetChild(2).gameObject.SetActive(true);
        FPSUnityChan.SetActive(true);                       // 1��Ī ����Ƽ¯ Ű��
    }
}

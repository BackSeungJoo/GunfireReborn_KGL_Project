
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
    public int curHealth;       //����HP
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

   /* private Camera Vcamera;
    public GameObject cross;
    public GameObject cube;*/

    [SerializeField]
    private bool activeRecoveryBar;
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
        hpBar = GameObject.Find("HPBar").GetComponent<Image>();                 //hp�� �޾ƿ���
        shieldBar = GameObject.Find("ShieldBar").GetComponent<Image>();         //shield�� �޾ƿ���
        virtualCam = FindObjectOfType<CinemachineVirtualCamera>();              //�����ķ �޾ƿ���
        ik = GetComponent<IK1>();                                               //3��Īik �޾ƿ���
        animator = GetComponent<Animator>();                                    //�� �ִϸ����� �޾ƿ���
        FPSUnityChan = Camera.main.transform.GetChild(0).gameObject;            //1��Īik �޾ƿ���

        maxHealth = 100;                                                        //�ִ� hp����
        maxShield = 100;                                                        //�ִ� shield����
        state = State.play;                                                     //���� ���¸� play�� �س���
        curHealth = maxHealth;                                                  //����HP�� MAXHP�� �ʱ�ȭ
        curShield = maxShield;                                                  //���罯�带 MAXShield�� �ʱ�ȭ
        hpBar.fillAmount = (float)curHealth / (float)maxHealth;                 //hp���� �ʱ�ȭ
        shieldBar.fillAmount = (float)curShield / (float)maxShield;             //Shield���� �ʱ�ȭ

        recoveryBar.fillAmount = 1;                                             //��Ŀ������ 1�� �ʱ�ȭ
        isturnGroggy = true;                                                    //isturnGroggy true�� �ʱ�ȭ  //state�� ���Ҷ� ����Ǵ� �Լ����� �ѹ��� ����ǰ� ����� ������
        isturnPlay = true;                                                      //isturnPlayr true�� �ʱ�ȭ   //���� true�� ���س����� �Լ����� �ѹ�����Ǽ� hp�� �ݹۿ����� ���·� ������.
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        //hp�� shield ������Ʈ
        shieldBar.fillAmount = (float)curShield / (float)maxShield;
        hpBar.fillAmount = (float)curHealth / (float)maxHealth;

        #region ���� ȸ�� 
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
        #endregion
/*
        if(activeRecoveryBar == true)
        {
            recoveryBarOB.SetActive(true);
        }
        else
        {
            recoveryBarOB.SetActive(false);
        }*/


        //�׷α� �����ϋ�
        if (state == State.groggy)
        {
            Debug.Log("�׷α��");
            Debug.Log(isturnGroggy);
            if (isturnGroggy == false)
            {
               
                Debug.Log(isturnGroggy);
                Debug.Log("������");
                animator.SetBool("groggy", true);                                    // �ִϸ����� �Ķ���͸� groggy�� ���� groggy�ִϸ��̼��� ����ǰԸ�����
                roation.enabled = false;                                             // 3��Ī �÷��̾��� �����̼��� ������
                photonView.RPC("MakeTrueRecoveryBar", RpcTarget.All);                // ť�� �ٸ� Ȱ��ȭ��Ű�� ik�� ����. rpc�� �ٸ� ���嵵 Ű�Ը�����
                
                isturnGroggy = true;                                                 // isturnGroggy�� true�� ���������� �Լ��� ��������������ʰ���
            }

            photonView.RPC("GoingDead",RpcTarget.MasterClient);                                                             // recoverybar�� fillamount�� ���� �پ��� ������
        }
        // �÷��̻��¶��
        if (state == State.play)                                
        {
            
            if (isturnPlay == false)
            {
                animator.SetBool("groggy", false);                                   // �ִϸ����� �Ķ���͸� groggy�� ���� groggy�ִϸ��̼��� ��������ʰԸ�����
                TurnPlay();
                roation.enabled = true;                                              // �÷��̾��� �����̼��� �ѹ���
                photonView.RPC("MakeFalseRecoveryBar", RpcTarget.All);               // ť�� �ٸ� ��Ȱ��ȭ��Ű�� ik�� �ٽ� Ȱ��ȭ��Ų��.
                virtualCam.transform.parent = gameObject.transform;
                virtualCam.transform.localPosition = Vector3.zero;
                Debug.Log(virtualCam.transform.localPosition);
                virtualCam.transform.localPosition = new Vector3(0f,0.8f,0.4f);
                //virtualCam.transform.localPosition = new Vector3(0f, transform.localPosition.y + 0.8f, 0.4f);
                Debug.Log(virtualCam.transform.localPosition);
            }
            ShotRayCast();                                                           // �÷������̶�� rayCast�� ��Ը�����
        }

        // �������¶�� 
        if (state == State.die)                                 
        {
            if (isDead == false)
            {
                animator.SetTrigger("Dead");                                         // ���� �ִϸ��̼��� ����ǰ���
                isDead = true;
            }
            ik.enabled = false;                   
        }
    }

    [PunRPC]
    private void MakeTrueRecoveryBar()
    {
        Debug.Log("MakeTrue");
        //activeRecoveryBar = true;
        recoveryBarOB.SetActive(true);
    }

    [PunRPC]
    private void MakeFalseRecoveryBar()
    {
       // activeRecoveryBar = false;
        recoveryBarOB.SetActive(false);
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
    private void PlayerHealthUpdated(int newCurHealth, float newCurShield,float rechargeTime)
    {
        curHealth = newCurHealth;
        curShield = newCurShield;
        rechargeTimer = rechargeTime;
        if (curHealth <= 0)
        {
            photonView.RPC("TurnTagStateGroggy", RpcTarget.All);        //�±�
            isturnGroggy = false;
            isturnPlay = false;
            TurnGroggy();
        }
    }
    [PunRPC]
    private void TurnTagStateGroggy()
    {
        state = State.groggy;
        gameObject.tag = "Groggy";
    }

   
    
    #region �׷α���·� ���ϴ� �Լ���, �÷��̻��·� ���ϴ� �Լ�
    private void TurnGroggy()
    {
        if(!photonView.IsMine)
        {
            return;
        }

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
        ik.isIk = false;                                // 3��Ī ik false�� �����
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
        ik.isIk = true;
        ik.ChangeIK("Pistol");
        isturnPlay = true;
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


    #region �׾���Լ�
    [PunRPC]
    private void GoingDead()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (isCure == false)
            {
                // 10�� ���� �����ؾ� �� ���� ����մϴ�.
                float decreaseAmount = 1.0f / dyingTime * dieSpeed * Time.deltaTime;
                // fillAmount�� ������Ʈ�մϴ�.
                recoveryBar.fillAmount = Mathf.Max(recoveryBar.fillAmount - decreaseAmount, 0);
                photonView.RPC("UpdateRecoveryBarDead", RpcTarget.All,recoveryBar.fillAmount);
                // fillAmount�� 0�̸� �ʿ��� ó���� �����մϴ�.
                if (recoveryBar.fillAmount == 0)
                {
                    //�������� ó����.
                    state = State.die;
                }
            }
        }
    }
    #endregion

    [PunRPC]
    private void UpdateRecoveryBarDead(float fillAmount)
    {
        recoveryBar.fillAmount = fillAmount;
    }

    
    #region ���̸� ����Լ�
    private void ShotRayCast()
    {
        if (Physics.Raycast(virtualCam.transform.position, virtualCam.transform.forward, out hitInfo, rayDistance))
        {   //���̰� �浹�Ѱ�� hitInfo�� �浹 ������ �޾ƿ´�.
            Debug.DrawRay(virtualCam.transform.position, virtualCam.transform.forward * rayDistance, Color.black);
            //playerHp test = hPlayer.GetComponent<playerHp>();
            if (hitInfo.transform.CompareTag("Groggy"))
            {
                //������ü�� Tag�� �׷α��(�±װ� �÷��̾��Ͻ� ���Ͱ� ��� ������ ī�޶� ��鸲) ���� �÷��̾ �׷α� ���¶��
                if (Input.GetButton("Get"))
                {   //f��������������
                    hPlayer = hitInfo.collider.gameObject;
                    //���� ���̸� ���� �÷��̾ ȸ���ϰ��ִ� �÷��̾� ������ �����Ѵ�.
                    hPlayer.GetComponent<playerHp>().photonView.RPC("Cure", RpcTarget.All);
                    //�÷��̾��� cure�Լ��� �����Ѵ�.
                }
                else
                {
                    photonView.RPC("StopCure", RpcTarget.All);
                    //ȸ�� �ߴ� �� ȸ�� ���¸� �ʱ�ȭ�մϴ�.
                }
            }
            else
            {
                photonView.RPC("StopCure", RpcTarget.All);
            }
        }

    }
    #endregion
    #region �߰��� ġ�Ḧ ���⶧ �Լ�
    [PunRPC]

    private void StopCure()
    {
       hPlayer.GetComponent<playerHp>().isCure = false;
    }
    #endregion

   
    [PunRPC]
    private void Cure()
    {
        // ���� ġ���� ���θ� ��Ÿ���� ������ true�� ������ 
        if (PhotonNetwork.IsMasterClient)
        {

            isCure = true;
            // 5�� ���� �����ؾ� �� ���� ����մϴ�.
            float increaseAmount = 1.0f / cureTime * recoverySpeed * Time.deltaTime;
            // fillAmount�� ������Ʈ�մϴ�.
            recoveryBar.fillAmount = Mathf.Max(recoveryBar.fillAmount + increaseAmount, 0);
            photonView.RPC("UpdateRecoveryBar", RpcTarget.All,recoveryBar.fillAmount);
            if (recoveryBar.fillAmount >= 1.0f)
            {
                photonView.RPC("TurnPlayMode", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    private void UpdateRecoveryBar(float fillAmount)
    {
        recoveryBar.fillAmount = fillAmount;
    }

    [PunRPC]

    private void TurnPlayMode()
    {
        isCure = false;                                     // ȸ�������� ��Ÿ���º����� false�� �����.
        gameObject.tag = "Player";                          // �±׸� �÷��̾�� �����
        state = State.play;                                 // �÷��̾��� ������Ʈ�� play�� �����.
        weapon.GetChild(0).gameObject.SetActive(true);      // 3��Ī������ Ȱ��ȭ �Ѵ�.
        FPSUnityChan.SetActive(true);                       // 1��Ī ����Ƽ¯ Ű��

    }

    #region �÷��̾� ��Ȱ
    private void ReStart()
    {
        gameObject.tag = "Player";
        state = State.play;
        weapon.GetChild(0).gameObject.SetActive(true);      // 3��Ī������ Ȱ��ȭ �Ѵ�.
        FPSUnityChan.SetActive(true);                       // 1��Ī ����Ƽ¯ Ű��
        
        if(GameManager.instance.nowStage == 1)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 1)
            {
                gameObject.transform.position = new Vector3(192, -23, 16);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 2)
            {
                gameObject.transform.position = new Vector3(192, -23, 18);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 3)
            {
                gameObject.transform.position = new Vector3(195, -23, 16);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 0)
            {
                gameObject.transform.position = new Vector3(195, -23, 18);
            }
            else
            {
                gameObject.transform.position = new Vector3(192, -23, 16);
            }
        }

        if (GameManager.instance.nowStage == 2)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 1)
            {
                gameObject.transform.position = new Vector3(3, 0, 27);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 2)
            {
                gameObject.transform.position = new Vector3(0, 0, 27);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 3)
            {
                gameObject.transform.position = new Vector3(-3, 0, 27);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 0)
            {
                gameObject.transform.position = new Vector3(-6, 0, 27);
            }
            else
            {
                gameObject.transform.position = new Vector3(3, 0, 27);
            }
        }

        if (GameManager.instance.nowStage == 3)
        {
            if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 1)
            {
                gameObject.transform.position = new Vector3(-30, 6, -32);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 2)
            {
                gameObject.transform.position = new Vector3(-32, 6, -32);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 3)
            {
                gameObject.transform.position = new Vector3(-34, 6, -32);
            }
            else if (PhotonNetwork.LocalPlayer.ActorNumber % 4 == 0)
            {
                gameObject.transform.position = new Vector3(-36, 6, -32);
            }
            else
            {
                gameObject.transform.position = new Vector3(-30, 6, -32);
            }
        }
    }
    #endregion
}

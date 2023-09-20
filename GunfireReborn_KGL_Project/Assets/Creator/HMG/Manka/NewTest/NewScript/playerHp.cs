
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class playerHp : MonoBehaviourPun
{
    public int maxHealth;       //�ִ�HP
    public int curHealth;       //����HP

    public int ammo;            //��üźâ
    public int remainAmmo;      //����źâ

    public int Gold;            //������

    public GameObject hpBar;

    private Animator animator;

    private playerCure cure;

    private IK1 ik;

    private CinemachineVirtualCamera virtualCam;
    public enum State
    {
        play,
        groggy,
        die
    }

    public State state;
    private void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();  
        ik = GetComponent<IK1>();
        cure = GetComponent<playerCure>();
        animator = GetComponent<Animator>();
        //ó�� ���� �� HP 
        //����HP�� MAXHP�� �ʱ�ȭ
        state = State.play;
        curHealth = maxHealth;
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        //photonView.RPC("StateUpdate", RpcTarget.All);
    }

    //player�� ���ݹ޾����� ���� ���ν��� �� �Լ�
    public void PlayerTakeDamage(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            state = State.groggy;
            
            cure.enabled = true;
        }

        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("PlayerHealthUpdated", RpcTarget.Others, curHealth);
        }
    }

    [PunRPC]
    public void PlayerHealthUpdated(int newHealth)
    {
        curHealth = newHealth;
    }


    private void StateUpdate()
    {
        if (state == State.groggy)
        {
            hpBar.SetActive(true);
            animator.SetBool("groggy", true);
            ik.enabled = false;
            cure.enabled = true;
            virtualCam.Follow = gameObject.transform;
            virtualCam.LookAt = gameObject.transform;
        }
        else
        {
            hpBar.SetActive(false);
            animator.SetBool("groggy", false);
            virtualCam.Follow = null;
            virtualCam.LookAt = null;
        }
        if (state == State.die)
        {
            animator.SetTrigger("Dead");
            ik.enabled = false;
            cure.enabled = false;
        }
        if(state == State.play)
        {
            ik.enabled = true;
            cure.enabled = false;
        }

    }

}

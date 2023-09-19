using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using Photon.Pun;
using System;

public class Pistol_Lie001 : MonoBehaviourPun, IPunObservable
{
    // �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������
    public enum State { READY, EMPTY, RELOADING}
    public State state {  get; private set; }

    // �Ѿ��� ������ �ѱ� ��ġ
    private Transform muzzle;

    // ��ݽ� �ѱ� ȭ�� ��ƼŬ
    public ParticleSystem muzzlFlash;

    private AudioSource fireSound;
    public AudioClip basicShot;
    public AudioClip EmptyMagAmmo;
    public AudioClip Pistol_Reload;

    // ��� ���ݽð�
    public float attackSpeed = 1f;
    private float attackTimer = 0f;

    // źâ �ִ� �뷮
    public int magCapacity = 9;
    // źâ ���� �Ѿ� ��
    public int magAmmo;

    private WaitForSeconds reloadTime;

    public Vector3 clonePos = default;
    public Quaternion cloneRot = default;



    private void Start()
    {
        if(photonView.IsMine)
        {
            muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        }

        fireSound = GetComponent<AudioSource>();
        reloadTime = new WaitForSeconds(0.7f);
        magAmmo = magCapacity;
        state = State.READY;
    }

    // ���� ���� ���¸� Ŭ�п��Ե� ����ȭ �����ִ� �޼ҵ�, ��ܿ� �������̽��� �߰��ؾ��Ѵ�.
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(magAmmo);
            stream.SendNext(state);
        }
        else
        {
            magAmmo = (int)stream.ReceiveNext();
            state = (State)stream.ReceiveNext();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            clonePos = transform.position;
            cloneRot = transform.rotation;

            if (state == State.EMPTY)
            {
                // ���콺 �Է½� �� źâ �Ҹ� ����
                if (Input.GetMouseButtonDown(0))
                {
                    photonView.RPC("EmptyShot", RpcTarget.Others);
                }
                // ����Ű�� ������ ��
                else if (Input.GetKeyDown(KeyCode.R) && magAmmo < magCapacity && state != State.RELOADING)
                {
                    state = State.RELOADING;
                    photonView.RPC("CloneReload", RpcTarget.All);
                }

                // �� �ܿ� ��Ȳ���� ������ �������� �ʴ´� ( ���� �Ұ� )
                return;
            }

            // źâ �뷮 ������� ������ ������
            if (Input.GetKeyDown(KeyCode.R) && magAmmo < magCapacity && state != State.RELOADING)
            {
                photonView.RPC("CloneReload", RpcTarget.All);
            }


            if (state == State.READY)
            {
                if (magAmmo <= 0)
                {
                    magAmmo = 0;
                    photonView.RPC("ShotStop", RpcTarget.Others);
                    muzzlFlash.Stop();
                    state = State.EMPTY;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    // �� �ѱ��� �ٶ󺸴� ���� ����
                    Vector3 foward = transform.right;
                    photonView.RPC("CloneShot", RpcTarget.Others, foward, muzzle.position, muzzle.rotation);
                    magAmmo -= 1;


                }
                else
                {
                    photonView.RPC("ShotStop", RpcTarget.Others);
                    muzzlFlash.Stop();
                }
            }

        }

    }


    [PunRPC]
    public void CloneShot(Vector3 foward, Vector3 Pos, Quaternion rot)
    {
        GameObject obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.PISTOLBULLET);
        Rigidbody objRigid = obj.GetComponent<Rigidbody>();

        obj.transform.position = Pos;
        obj.transform.rotation = rot;
        obj.gameObject.SetActive(true);

        objRigid.velocity = foward * 30f;

        muzzlFlash.Play();
        fireSound.clip = basicShot;
        fireSound.Play();
    }

    [PunRPC]
    public void ShotStop()
    {
        muzzlFlash.Stop();
    }

    [PunRPC]
    public void EmptyShot()
    {
        fireSound.clip = EmptyMagAmmo;
        fireSound.Play();
    }

    [PunRPC]
    public void CloneReload()
    {
        StartCoroutine(ReLoading());
    }
    IEnumerator ReLoading()
    {
        state = State.RELOADING;

        int reloadBullet = 0;
        reloadBullet = magCapacity - magAmmo;

        fireSound.clip = Pistol_Reload;
        fireSound.Play();

        yield return reloadTime;

        // ������ �ð� ���� �����غ� ���·� �ٲٸ� �ڷ�ƾ ����
        magAmmo += reloadBullet;
        state = State.READY;

    }


}

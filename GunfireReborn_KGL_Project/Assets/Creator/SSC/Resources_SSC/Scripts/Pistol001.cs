using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class Pistol001 : MonoBehaviourPun
{
    // �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������
    public enum State { READY, EMPTY, RELOADING}
    public State state {  get; private set; }

    // �Ѿ��� ������ �ѱ� ��ġ
    private Transform muzzle;
    //private Transform lieMuzzle;

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
    public int magAmmo = 0;

    private WaitForSeconds reloadTime;

    private CinemachineVirtualCamera cam;

    void Start()
    {

        cam = FindObjectOfType<CinemachineVirtualCamera>();
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        reloadTime = new WaitForSeconds(0.7f);

        magAmmo = magCapacity;
        state = State.READY;
        

    }
    // Update is called once per frame
    void Update()
    {
        if(transform.parent == null)
        {
            return;
        }

        if (state == State.EMPTY)
        {
            // ���콺 �Է½� �� źâ �Ҹ� ����
            if (Input.GetMouseButtonDown(0))
            {
                fireSound.clip = EmptyMagAmmo;
                fireSound.Play();
            }
            // ����Ű�� ������ ��
            else if (Input.GetKeyDown(KeyCode.R) && magAmmo < magCapacity && state != State.RELOADING)
            {
                state = State.RELOADING;
                fireSound.clip = Pistol_Reload;
                StartCoroutine(ReLoading());
            }

            // �� �ܿ� ��Ȳ���� ������ �������� �ʴ´� ( ���� �Ұ� )
            return;
        }

        // źâ �뷮 ������� ������ ������
        if (Input.GetKeyDown(KeyCode.R) && magAmmo < magCapacity && state != State.RELOADING)
        {
            StartCoroutine(ReLoading());
        }


        if (state == State.READY)
        {
            if (magAmmo <= 0)
            {
                magAmmo = 0;
                muzzlFlash.Stop();
                state = State.EMPTY;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GameObject obj = null;
                Rigidbody objRigid = null;

                obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.PISTOLBULLET);

                if (obj != null)
                {
                    obj.transform.position = muzzle.transform.position;
                    obj.transform.rotation = muzzle.transform.rotation;

                    obj.gameObject.SetActive(true);

                    objRigid = obj.GetComponent<Rigidbody>();
                
                    objRigid.velocity = cam.transform.forward * 30f;
                }


                magAmmo -= 1;
                muzzlFlash.Play();
                fireSound.clip = basicShot;
                fireSound.Play();

            }
            else
            {
                muzzlFlash.Stop();
            }
        }
        // } �⺻ ��� : ��Ŭ��

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

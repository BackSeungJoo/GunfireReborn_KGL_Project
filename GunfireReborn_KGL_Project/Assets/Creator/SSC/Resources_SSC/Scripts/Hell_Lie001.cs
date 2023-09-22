using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Resources;
using Cinemachine;
using Photon.Pun;

public class Hell_Lie001 : MonoBehaviourPun, IPunObservable
{
    // �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������, �����׼�
    public enum State { READY, EMPTY, RELOADING, PUMP_ACTION }
    // ���¸� ������ ������Ƽ state
    public State state {  get; private set; }

    // �Ѿ��� ������ �ѱ� ��ġ
    private Transform muzzle;

    // ��ݽ� �ѱ� ȭ�� ��ƼŬ
    public ParticleSystem muzzlFlash;

    // ���� ����� Ŭ��
    private AudioSource fireSound;
    public AudioClip Hell_Shot;
    public AudioClip Hell_Reload;
    public AudioClip EmptyMagAmmo;

    // ��ü �ִ� �Ѿ� ��
    private int maxAmmoRemain = 24;

    // �����ִ� ��ü �Ѿ� ��
    private int ammoRemain;

    // źâ �ִ� �뷮
    private int magCapacity = 8;
    // źâ ���� �Ѿ� ��
    private int magAmmo;
    private float bulletSpeed = 30f;

    // ��� ���ݽð�
    private WaitForSeconds attackSpeed;
    private WaitForSeconds reloadingTime;

    private float xMax = 0.15f;
    private float xMin = -0.15f;

    private float yMax = 0.15f;
    private float yMin = -0.15f;

    private float zMax = 0.15f;
    private float zMin = -0.15f;

    // ������ ��ݽ� ���� �ڷ�ƾ�� ���߱����� Reloading() �ڷ�ƾ�� ���� reload

    IEnumerator reload;

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

    private void Awake()
    {
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        reloadingTime = new WaitForSeconds(1.0f);
        attackSpeed = new WaitForSeconds(0.75f);

        ammoRemain = maxAmmoRemain;
        magAmmo = magCapacity;

        // ���� �ڷ�ƾ ��Ƶα�
        reload = ReLoading();

        // ���� ������ �������
        state = State.READY;       

    }
    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine)
        {
            // �÷��̾��� �տ� �ִ°��� �ƴ϶�� �������� �ʴ´�.
            if (transform.parent == null)
            {
                return;
            }

            // źâ�� ����ִ� ���¶��
            if (state == State.EMPTY)
            {
                // ���콺 �Է½� �� źâ �Ҹ� ����
                if (Input.GetMouseButtonDown(0))
                {
                    photonView.RPC("CloneEmpty", RpcTarget.Others);
                }
                // ����Ű�� ������ ��
                else if (Input.GetKeyDown(KeyCode.R) && state != State.RELOADING)
                {
                    state = State.RELOADING;
                    StartCoroutine(reload);
                }

                // �� �ܿ� ��Ȳ���� ������ �������� �ʴ´� ( ���� �Ұ� )
                return;
            }

            // ���� ����
            if(Input.GetKeyDown(KeyCode.R) && state != State.RELOADING)
            {
                state = State.RELOADING;
                fireSound.clip = Hell_Reload;
                StartCoroutine(reload);
            }

            // ����, ������ �� ��� ����
            if(state == State.READY || state == State.RELOADING)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    state = State.PUMP_ACTION;
                    magAmmo -= 1;
                    StartCoroutine(Attack());
                }
            }

        }

    }

    [PunRPC]
    public void CloneShot(Vector3 foward, Vector3 pos, Quaternion rot)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 muzzleFoward = foward;
            muzzleFoward.x = muzzleFoward.x + Random.Range(xMax, xMin);
            muzzleFoward.y = muzzleFoward.y + Random.Range(yMax, yMin);
            muzzleFoward.z = muzzleFoward.z + Random.Range(xMax, xMin);

            GameObject obj = null;
            Rigidbody objRigid = null;

            obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

            if (obj != null)
            {
                obj.transform.position = pos;
                obj.transform.rotation = rot;

                objRigid = obj.GetComponent<Rigidbody>();

                obj.gameObject.SetActive(true);
                objRigid.velocity = muzzleFoward * bulletSpeed;

            }

        }

        fireSound.clip = Hell_Shot;
        fireSound.Play();
    }

    [PunRPC]
    public void CloneReload()
    {
        Debug.Log("�ݺ��� �󸶳� �ϴ���");
        fireSound.clip = Hell_Reload;
        fireSound.Play();
    }

    [PunRPC]
    public void CloneEmpty()
    {
        fireSound.clip = EmptyMagAmmo;
        fireSound.Play();
    }



    IEnumerator Attack()
    {
        StopCoroutine(reload);
        reload = ReLoading();

        photonView.RPC("CloneShot", RpcTarget.Others,
                         muzzle.transform.forward, muzzle.transform.position, muzzle.transform.rotation);

        // ���� źâ�Ѿ��� 0���� �۾����ٸ�
        if (magAmmo <= 0)
        {
            // ������ �Ѿ�� ����
            magAmmo = 0;
            // źâ�� ����ִ� ���·� ����
            state = State.EMPTY;

            yield return attackSpeed;
            yield break;
        }

        yield return attackSpeed;

        state = State.READY;
    }

    IEnumerator ReLoading()
    {
        // ���� �Ѿ��� �� źâ�� �ѷ�����(8��) �������� �ݺ�
        while (magAmmo < magCapacity)
        {
            // �����ִ� �Ѿ� ���� 0 ���ϰ� �ɽ�
            if (ammoRemain <= 0)
            {
                ammoRemain = 0;
                yield break;
            }

            yield return reloadingTime;

            magAmmo++;
            photonView.RPC("CloneReload", RpcTarget.Others);

        }

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Cinemachine;


// �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������ ��
public class CrimsonFirescale001 : MonoBehaviour
{
    public bool useSkiil = false;

    public enum State { READY, EMPTY, RELOADING }
    public State state {  get; private set; }

    private CinemachineVirtualCamera cam;

    public ParticleSystem muzzlFlash;
    private Transform muzzle;
    private AudioSource fireSound;
    public AudioClip basicShot;
    public AudioClip skillShot;
    public AudioClip EmptyMagAmmo;
    public AudioClip CrimsonFirescale_Reload;

    // ��� ���ݽð�
    private float attackSpeed = 0.1f;    
    private float attackTimer = 0f;    

    // ��ü �ִ� �Ѿ� ��
    private int maxAmmoRemain = 90;
    // �����ִ� ��ü �Ѿ� ��
    private int ammoRemain;

    // źâ �ִ� �뷮
    private int magCapacity = 30;
    // źâ ���� �Ѿ� ��
    private int magAmmo;

    private int skillAmmo = 4;

    private float bulletSpeed = 50f;

    private WaitForSeconds reloadTime;

    private float xMax = 0.1f;
    private float xMin = -0.1f;

    private float yMax = 0.1f;
    private float yMin = -0.1f;

    private float zMax = 0.1f;
    private float zMin = -0.1f;


    private void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();

        //shoot = FindObjectOfType<PlayerAttack>();
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        reloadTime = new WaitForSeconds(2f);

        // { �����ִ� ��ü �Ѿ�, ���� źâ �Ѿ� �ؽ�Ʈ ����
        ammoRemain = maxAmmoRemain;
        //AmmoRemainText.text = "" + maxAmmoRemain;
        magAmmo = magCapacity;
        //MagAmmoText.text = "" + magCapacity;
        // } �����ִ� ��ü �Ѿ�, ���� źâ �Ѿ� �ؽ�Ʈ ����

        state = State.READY;        

    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent == null)
        {
            return;
        }

        // źâ�� ����ִ� ���¶��
        if (state == State.EMPTY)
        {
            // ���콺 �Է½� �� źâ �Ҹ� ����
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                fireSound.clip = EmptyMagAmmo;
                fireSound.Play();
            }
            // ����Ű�� ������ ��
            else if (Input.GetKeyDown(KeyCode.R) && state != State.RELOADING)
            {
                StartCoroutine(ReLoading());
            }

        }

        // źâ �뷮 ������� ������ ������
        if(Input.GetKeyDown(KeyCode.R) && state != State.RELOADING)
        {
            StartCoroutine(ReLoading());
        }

        if(state == State.READY)
        {
            if(magAmmo <= 0)
            {
                magAmmo = 0;
                muzzlFlash.Stop();
                state = State.EMPTY;
            }

            if (Input.GetMouseButton(0) == true)
            {
                attackTimer += Time.deltaTime;

                if(attackTimer > attackSpeed)
                {
                    GameObject obj = null;
                    Rigidbody objRigid = null;

                    obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

                    if (obj != null)
                    {
                        obj.transform.position = muzzle.transform.position;
                        obj.transform.rotation = muzzle.transform.rotation;

                        objRigid = obj.GetComponent<Rigidbody>();

                        obj.gameObject.SetActive(true);
                        objRigid.velocity = cam.transform.forward * bulletSpeed;

                    }

                    magAmmo -= 1;                   
                    muzzlFlash.Play();
                    fireSound.clip = basicShot;
                    fireSound.volume = 0.4f;
                    fireSound.Play();

                    attackTimer = 0f;
                }
            }
            else
            {
                muzzlFlash.Stop();
            }
            // } �⺻ ��� : ��Ŭ��

            if(Input.GetMouseButtonDown(1))
            {                
                UsingSkill(P_PoolObjType.BULLET);                              
            }

        }

        //AmmoRemainText.text = "" + ammoRemain;
        //MagAmmoText.text = "" + magAmmo;

    }

    private void UsingSkill(P_PoolObjType type)
    {
        useSkiil = true;
        // �����ִ� źâ�� ��ų�뷮���� ���� �� 
        if(magAmmo < skillAmmo )
        {
            // ������ ��ų �Ѿ� ������ ���� źâ�� ���� �縸ŭ
            for (int i = 0; i < magAmmo; i++)
            {
                Vector3 foward = cam.transform.forward;
                foward.x = foward.x + Random.Range(xMax, xMin);
                foward.y = foward.y + Random.Range(yMax, yMin);
                foward.z = foward.z + Random.Range(xMax, xMin);

                GameObject obj = null;
                Rigidbody objRigid = null;

                obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

                if(obj != null)
                {
                    obj.transform.position = muzzle.transform.position;
                    obj.transform.rotation = muzzle.transform.rotation;

                    objRigid = obj.GetComponent<Rigidbody>();

                    obj.gameObject.SetActive(true);
                    objRigid.velocity = foward * bulletSpeed;

                }

            }
            magAmmo -= magAmmo;
        }
        else
        {
            for (int i = 0; i < skillAmmo; i++)
            {
                Vector3 foward = cam.transform.forward;
                foward.x = foward.x + Random.Range(xMax, xMin);
                foward.y = foward.y + Random.Range(yMax, yMin);
                foward.z = foward.z + Random.Range(xMax, xMin);

                GameObject obj = null;
                Rigidbody objRigid = null;

                obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

                if (obj != null)
                {
                    obj.transform.position = muzzle.transform.position;
                    obj.transform.rotation = muzzle.transform.rotation;

                    objRigid = obj.GetComponent<Rigidbody>();

                    obj.gameObject.SetActive(true);
                    objRigid.velocity = foward * bulletSpeed;

                }

                magAmmo -= 1;
            }

        }

        muzzlFlash.Play();
        fireSound.clip = skillShot;
        fireSound.volume = 0.8f;
        fireSound.Play();

        useSkiil = false;
    }

    IEnumerator ReLoading()
    {
        state = State.RELOADING;

        int reloadBullet = 0;

        reloadBullet = magCapacity - magAmmo;

        fireSound.clip = CrimsonFirescale_Reload;
        fireSound.Play();

        if(reloadBullet > ammoRemain)
        {
            magAmmo += ammoRemain;
            ammoRemain = 0;

            yield return reloadTime;

            // ������ �ð� ���� �����غ� ���·� �ٲٸ� �ڷ�ƾ ����
            state = State.READY;

            yield break;
        }
        // ������ �ð�
        yield return reloadTime;
            
        ammoRemain -= reloadBullet;
        magAmmo += reloadBullet;

        // ������ �ð� ���� �����غ� ���·� �ٲٸ� �ڷ�ƾ ����
        state = State.READY;

        yield break;
    }
}

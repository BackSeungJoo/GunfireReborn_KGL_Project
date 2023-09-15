using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


// �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������ ��
public enum State { READY, EMPTY, RELOADING }
public class CrimsonFirescale001 : MonoBehaviour
{
    public bool useSkiil = false;

    public State state {  get; private set; }

    // �Ѿ��� ������ �ѱ� ��ġ
    private Transform muzzle;

    // ��ݽ� �ѱ� ȭ�� ��ƼŬ
    public ParticleSystem muzzlFlash;

    private AudioSource fireSound;
    public AudioClip basicShot;
    public AudioClip skillShot;
    public AudioClip EmptyMagAmmo;
    public AudioClip CrimsonFirescale_Reload;

    // ��� ���ݽð�
    public float attackSpeed = 0.1f;    
    private float attackTimer = 0f;    

    // ��ü �ִ� �Ѿ� ��
    public int maxAmmoRemain = 90;
    // �����ִ� ��ü �Ѿ� ��
    public int ammoRemain;

    // źâ �ִ� �뷮
    public int magCapacity = 30;
    // źâ ���� �Ѿ� ��
    public int magAmmo;

    public int skillAmmo = 10;

    //public TMP_Text MagAmmoText;
    //public TMP_Text AmmoRemainText;

    PlayerAttack shoot;

    private WaitForSeconds reloadTime;

    private void Start()
    {
        //shoot = FindObjectOfType<PlayerAttack>();
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        reloadTime = new WaitForSeconds(1.1f);

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
                    GameObject obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

                    obj.transform.position = muzzle.transform.position;
                    obj.transform.rotation = muzzle.transform.rotation;

                    obj.gameObject.SetActive(true);

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
                GameObject obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

                obj.transform.position = muzzle.transform.position;
                obj.transform.rotation = muzzle.transform.rotation;

                obj.gameObject.SetActive(true);
            }
            magAmmo -= magAmmo;
        }
        else
        {
            for (int i = 0; i < skillAmmo; i++)
            {
                GameObject obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.BULLET);

                obj.transform.position = muzzle.transform.position;
                obj.transform.rotation = muzzle.transform.rotation;

                obj.gameObject.SetActive(true);
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

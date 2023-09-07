using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    // �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������
    public enum State { Ready, Empty, Reloading}
    public State state {  get; private set; }

    // ��ݽ� ������ �Ѿ� ������
    public GameObject bulletPrefab;
    // �Ѿ��� ������ �ѱ� ��ġ
    private Transform muzzle;

    // ��ݽ� �ѱ� ȭ�� ��ƼŬ
    public ParticleSystem muzzlFlash;

    private AudioSource fireSound;
    public AudioClip basicShot;
    public AudioClip skillShot;

    // ��� ���ݽð�
    public float attackSpeed = 0.01f;
    private float attackTimer = 0f;

    private bool useskill = false;

    PlayerController shoot;

    private void Start()
    {
        shoot = FindObjectOfType<PlayerController>();
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        //muzzlFlash = GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        // { �⺻ ��� : ��Ŭ��
        if(shoot.isShoot == true)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer > attackSpeed)
            {
                
                Instantiate(bulletPrefab, muzzle.transform.position, transform.rotation);         
                muzzlFlash.Play();
                fireSound.clip = basicShot;
                fireSound.volume = 0.4f;
                fireSound.Play();

                shoot.camRotate.x += 50f;
                attackTimer = 0f;
            }
        }
        else
        {
            muzzlFlash.Stop();
        }
        // } �⺻ ��� : ��Ŭ��


        // { ��ų ��� : ��Ŭ��
        if(useskill == false)
        {
            UsingSkill();
        }
        // } ��ų ��� : ��Ŭ��


    }

    private void UsingSkill()
    {
        if (shoot.useSkill == true)
        {
            useskill = true;
            for (int i = 0; i < 10; i++)
            {
                Instantiate(bulletPrefab, muzzle.transform.position, transform.rotation);
            }

            muzzlFlash.Play();
            fireSound.clip = skillShot;
            fireSound.volume = 0.8f;
            fireSound.Play();

            useskill = false;
        }
    }
}

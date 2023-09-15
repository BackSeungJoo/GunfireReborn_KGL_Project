using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Pistol_HMG : MonoBehaviourPun
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
    public float attackSpeed = 1f;
    private float attackTimer = 0f;

    private bool useskill = false;

    public PlayerAttack shoot;

    private void Start()
    {
        if(!photonView.IsMine)
        { return; }
        //shoot = GetComponent<PlayerAttack>();
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        //muzzlFlash = GetComponent<ParticleSystem>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        { return; }
        // { �⺻ ��� : ��Ŭ��
        if (shoot.isShootPistol == true)
        {
            PhotonNetwork.Instantiate(bulletPrefab.name, muzzle.transform.position, muzzle.transform.rotation);
            muzzlFlash.Play();
            fireSound.clip = basicShot;
            fireSound.volume = 0.4f;
            fireSound.Play();
            shoot.isShootPistol = false;
        }
        else
        {
            muzzlFlash.Stop();
        }
        // } �⺻ ��� : ��Ŭ��


    }

    private void UsingSkill()
    {
        if (shoot.useSkill == true)
        {
            useskill = true;

            for (int i = 0; i < 10; i++)
            {
                Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
            }

            muzzlFlash.Play();
            fireSound.clip = skillShot;
            fireSound.volume = 0.8f;
            fireSound.Play();

            useskill = false;
        }
    }
}

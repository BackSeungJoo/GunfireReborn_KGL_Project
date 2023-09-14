using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Resources;

public class Hell001 : MonoBehaviour
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

    //public TMP_Text MagAmmoText;
    //public TMP_Text AmmoRemainText;

    // ��� ���ݽð�
    private WaitForSeconds attackSpeed;
    private WaitForSeconds reloadingTime;

    // ������ ��ݽ� ���� �ڷ�ƾ�� ���߱����� Reloading() �ڷ�ƾ�� ���� reload

    IEnumerator reload;

    private void Start()
    {
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        reloadingTime = new WaitForSeconds(1.0f);
        attackSpeed = new WaitForSeconds(0.75f);

        // { �����ִ� ��ü �Ѿ�, ���� źâ �Ѿ� �ؽ�Ʈ ����
        ammoRemain = maxAmmoRemain;
        //AmmoRemainText.text = "" + maxAmmoRemain;
        magAmmo = magCapacity;
        //MagAmmoText.text = "" + magCapacity;
        // } �����ִ� ��ü �Ѿ�, ���� źâ �Ѿ� �ؽ�Ʈ ����

        // ���� �ڷ�ƾ ��Ƶα�
        reload = ReLoading();

        // ���� ������ �������
        state = State.READY;       

    }
    // Update is called once per frame
    void Update()
    {
        // �÷��̾��� �տ� �ִ°��� �ƴ϶�� �������� �ʴ´�.
        //if(transform.parent == null)
        //{
        //    return;
        //}

        // źâ�� ����ִ� ���¶��
        if (state == State.EMPTY)
        {
            // ���콺 �Է½� �� źâ �Ҹ� ����
            if (Input.GetMouseButtonDown(0))
            {
                fireSound.clip = EmptyMagAmmo;
                fireSound.Play();
            }
            // ����Ű�� ������ ��
            else if (Input.GetKeyDown(KeyCode.R) && state != State.RELOADING)
            {
                state = State.RELOADING;
                fireSound.clip = Hell_Reload;
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
                StartCoroutine(Attack());
            }
        }

        //AmmoRemainText.text = "" + ammoRemain;
        //MagAmmoText.text = "" + magAmmo;

    }

    IEnumerator Attack()
    {
        StopCoroutine(reload);
        reload = ReLoading();

        for (int i = 0; i < 10; i++)
        {
            GameObject obj = ObjectPoolManager.instance.GetPoolObj(PoolObjType.HELLBULLET);

            obj.transform.position = muzzle.transform.position;
            obj.transform.rotation = muzzle.transform.rotation;

            obj.gameObject.SetActive(true);
        }

        magAmmo -= 1;
        fireSound.clip = Hell_Shot;
        fireSound.Play();

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
        yield break;
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

            fireSound.Play();

            yield return reloadingTime;
            magAmmo += 1;
            ammoRemain -= 1;

        }

    }

}

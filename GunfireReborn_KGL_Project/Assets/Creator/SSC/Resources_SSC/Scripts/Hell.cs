using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hell : MonoBehaviour
{
    // �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������, �����׼�
    public enum State { READY, EMPTY, RELOADING, PUMP_ACTION }
    public State state {  get; private set; }

    // ��ݽ� ������ �Ѿ� ������
    public GameObject bulletPrefab;
    // �Ѿ��� ������ �ѱ� ��ġ
    private Transform muzzle;

    // ��ݽ� �ѱ� ȭ�� ��ƼŬ
    public ParticleSystem muzzlFlash;

    private AudioSource fireSound;
    public AudioClip Hell_Shot;
    public AudioClip Hell_Reload;
    public AudioClip EmptyMagAmmo;

    // ��� ���ݽð�
    public float attackSpeed = 1f;
    public float skillSpeed = 0.5f;

    // ��ü �ִ� �Ѿ� ��
    public int maxAmmoRemain = 24;

    // �����ִ� ��ü �Ѿ� ��
    public int ammoRemain;

    // źâ �ִ� �뷮
    public int magCapacity = 8;
    // źâ ���� �Ѿ� ��
    public int magAmmo;

    public TMP_Text MagAmmoText;
    public TMP_Text AmmoRemainText;

    private WaitForSeconds reloadingTime;

    IEnumerator reload;

    private void Start()
    {        
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        fireSound = GetComponent<AudioSource>();
        reloadingTime = new WaitForSeconds(1.0f);

        // { �����ִ� ��ü �Ѿ�, ���� źâ �Ѿ� �ؽ�Ʈ ����
        ammoRemain = maxAmmoRemain;
        AmmoRemainText.text = "" + maxAmmoRemain;
        magAmmo = magCapacity;
        MagAmmoText.text = "" + magCapacity;
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
        if(transform.parent == null)
        {
            return;
        }

        // �����׼� (���� �� ����) ���°� �ȴٸ� ������ �����.
        if(state == State.PUMP_ACTION)
        {
            StopCoroutine(reload);
        }

        // źâ�� ����ִ� ���¶��
        if(state == State.EMPTY)
        {
            // ���콺 �Է½� �� źâ �Ҹ� ����
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                fireSound.clip = EmptyMagAmmo;
                fireSound.Play();
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
            StartCoroutine(reload);
        }

        // ���� ���¿����� ��� ����
        if(state == State.READY)
        {
            // ���� źâ�Ѿ��� 0���� �۾����ٸ�
            if(magAmmo <= 0)
            {
                // ������ �Ѿ�� ����
                magAmmo = 0;
                // źâ�� ����ִ� ���·� ����
                state = State.EMPTY;
            }

            // PlayerAttack Cs���Ͽ��� ���콺Ŭ���� ���� bool �� ��ȣ�� ����
            if (Input.GetMouseButtonDown(0))
            {
                state = State.PUMP_ACTION;
                StartCoroutine(Attack());
            }
            // } �⺻ ��� : ��Ŭ��
        }

        AmmoRemainText.text = "" + ammoRemain;
        MagAmmoText.text = "" + magAmmo;

    }

    IEnumerator Attack()
    {       
        for (int i = 0; i < 10; i++)
        {
            Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);
        }
        magAmmo -= 1;
      
        fireSound.clip = Hell_Shot;
        fireSound.Play();

        yield return reloadingTime;

        state = State.READY;

        yield break;
    }

    IEnumerator ReLoading()
    {
        fireSound.clip = Hell_Reload;

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
            ammoRemain -= 1;
            magAmmo += 1;

            // �ѹ� ������ ���������� ���� ���� ���� ( �����׼� ���� )
            state = State.READY;

        }

        yield break;

    }
}

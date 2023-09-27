using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Resources;
using Cinemachine;
using UnityEngine.UI;

public class Hell001 : MonoBehaviour
{
    // �ѱ��� ���� ���¸� ������ Enum : �߻簡��, źâ�������, ������, �����׼�
    public enum State { READY, EMPTY, RELOADING, PUMP_ACTION }
    // ���¸� ������ ������Ƽ state
    public State state {  get; private set; }

    private CinemachineVirtualCamera cam;

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

    // źâ �ִ� �뷮
    private int magCapacity = 8;
    // źâ ���� �Ѿ� ��
    private int magAmmo;
    private float bulletSpeed = 50f;

    // ��� ���ݽð�
    private WaitForSeconds attackSpeed;
    private WaitForSeconds reloadingTime;

    private float xMax = 0.15f;
    private float xMin = -0.15f;

    private float yMax = 0.15f;
    private float yMin = -0.15f;

    private float zMax = 0.15f;
    private float zMin = -0.15f;

    private playerBullet bulletInfo;

    [SerializeField] private GameObject BBullet;
    [SerializeField] private GameObject BBulletBack;
    [SerializeField] private TextMeshProUGUI BulletText;
    [SerializeField] private GameObject BlackSmith;

    private Image bulletFill;

    // ������ ��ݽ� ���� �ڷ�ƾ�� ���߱����� Reloading() �ڷ�ƾ�� ���� reload

    IEnumerator reload;

    private void Awake()
    {
        bulletInfo = transform.parent.GetComponent<playerBullet>();
        magAmmo = magCapacity;

        bulletFill = BBullet.GetComponent<Image>();

        bulletFill.fillAmount = (float)bulletInfo.remainBBullet / (float)bulletInfo.maxBBullet;
        // ���� źâ�� / �ƽ� źâ�� 
    }

    private void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        muzzle = transform.Find("Muzzle").GetComponentInChildren<Transform>();
        BlackSmith = GameObject.Find("BlackSmithCanvas");
        fireSound = GetComponent<AudioSource>();
        reloadingTime = new WaitForSeconds(1.0f);
        attackSpeed = new WaitForSeconds(0.75f);

        // ���� �ڷ�ƾ ��Ƶα�
        reload = ReLoading();

        // ���� ������ �������
        state = State.READY;       

    }

    private void OnEnable()
    {
        BBullet.SetActive(true);
        BBulletBack.SetActive(true);
        BulletText.text = magAmmo + " / " + bulletInfo.remainBBullet;
        bulletFill.fillAmount = (float)bulletInfo.remainBBullet / (float)bulletInfo.maxBBullet;

    }

    private void OnDisable()
    {
        BBullet.SetActive(false);
        BBulletBack.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        bulletFill.fillAmount = (float)bulletInfo.remainBBullet / (float)bulletInfo.maxBBullet;
        BulletText.text = magAmmo + " / " + bulletInfo.remainBBullet;

        if (BlackSmith != null && BlackSmith.activeSelf)
        {
            return;
        }

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

    }

    IEnumerator Attack()
    {
        StopCoroutine(reload);
        reload = ReLoading();

        for(int i =  0; i < 10; i++)
        {
            Vector3 foward = cam.transform.forward;
            foward.x = foward.x + Random.Range(xMax, xMin);
            foward.y = foward.y + Random.Range(yMax, yMin);
            foward.z = foward.z + Random.Range(xMax, xMin);

            GameObject obj = null;
            Rigidbody objRigid = null;
            HellBullet001 objDamage;

            obj = PhotonPoolManager.P_instance.GetPoolObj(P_PoolObjType.HELLBULLET);

            if (obj != null)
            {
                obj.transform.position = muzzle.transform.position;
                obj.transform.rotation = muzzle.transform.rotation;

                objRigid = obj.GetComponent<Rigidbody>();
                objDamage = obj.GetComponent<HellBullet001>();

                objDamage.bulletDamage = UpgradeManager.up_Instance.shotgunDamage;
                obj.gameObject.SetActive(true);
                objRigid.velocity = foward * bulletSpeed;

            }

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
            if (bulletInfo.remainBBullet <= 0)
            {
                bulletInfo.remainBBullet = 0;
                yield break;
            }

            fireSound.Play();

            yield return reloadingTime;
            magAmmo += 1;
            bulletInfo.remainBBullet -= 1;

        }

    }

}

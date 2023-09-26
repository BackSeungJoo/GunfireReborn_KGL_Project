using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;
public class WeaponManager1 : MonoBehaviourPun
{
    //���� Ȱ��ȭ���ִ� ������ Ȯ���ϱ����Ѻ���
    public bool[] ActiveSlot;
    //���� ���Կ� �ִ� ������ Ȯ���ϱ����� ����
    public string[] slotWeapons;
    //������������ �������� ������ ����迭
    public GameObject[] weaponPrefabs;
    //�÷��̾��� IK�Լ��� ������������ ����
    private IK1 playerIK;
    //������������ �ȿ��ִ� ����������Ʈ���� �����״� �ϱ����� ����迭
    public GameObject[] Equip_weapons;
    //1��Ī ������Ʈ��;
    public GameObject[] Front_weapons;
    //����Ʈ�÷��̾��� IK�Լ��� ������������ ����
    private FrontIK1 frontIK;
    //����Ʈ �÷����� �����ϱ����Ѻ���
    public GameObject frontPlayer;
    // Update is called once per frame
    private Animator frontAnimator;


    private void Start()
    {
        //����ó������ ����0�� 1�� �ƹ��͵� ���»��°��Ǿ��Ѵ�.
        slotWeapons[0] = null;
        slotWeapons[1] = null;
        //����2�� �⺻�������ִ�.
        slotWeapons[2] = "Pistol";
        //��Ƽ�꽽��3�� �����.
        ActiveSlot = new bool[3];
        //����2�� Ȱ��ȭ��Ų��.
        ActiveSlot[2] = true;
        //�÷��̾�ik ik���޴´�.
        playerIK = gameObject.GetComponent<IK1>();
        //
        frontPlayer = Camera.main.transform.GetChild(0).gameObject;

        frontIK = frontPlayer.GetComponent<FrontIK1>();
        //Todo: Equip_weapons[]�迭�� �����Ҹ�� ������� �߰�������Ѵ�.
        //Equp_weapons[]�迭�� ������� ������ ����������迭�� �����Ͱ��ƾ��Ѵ�.
        //������ ����迭�� �ݵ�� �ν�����â���� �����̵Ǿ��־���Ѵ�.
        //���� ���ο� ���⸦ �߰��ϰ�ʹٸ� Equip_weapons[]�� weaponPrefabs[]�迭�� �ʼ������� �߰��ؾ��Ѵ�.

        //�������Ҷ��� Activeslot[]�� �������ְ�, ik, Equipweapons[]�� �����ؾ��Ѵ�.

        for (int i = 0; i < Front_weapons.Length; i++)
        {
            Transform child = frontPlayer.transform.GetChild(2).GetChild(i); // �ڽ� ���� ������Ʈ ��������
            Front_weapons[i] = child.gameObject; // �迭�� �ֱ�
        }
        for (int i =0; i < frontIK.FrontWeaponChilds.Length; i++)
        {
            Transform child = frontPlayer.transform.GetChild(2).GetChild(i);
            frontIK.FrontWeaponChilds[i] = child.gameObject;
        }

        frontIK.ChangeIK("Pistol");
        frontAnimator = frontPlayer.GetComponent<Animator>();
        frontIK.IKAnimator = frontAnimator;
        
    }
    void Update()
    {

        if(!photonView.IsMine)
        {
            return;
        }
        if (Input.GetButtonDown("Swap1"))
        {
            //����1�� ��������� return
            if (slotWeapons[0] == null)
            {
                return;
            }
            //Todo : 2��, Ȥ�� 3���̶��  ���� ���� ����ְų� �������̶�� �ִϸ��̼��� ����ϰ�

            if (ActiveSlot[0] == true)
            {
                return;
            }
            ActiveSlot[0] = true;
            ActiveSlot[1] = false;
            ActiveSlot[2] = false;
            frontAnimator.SetTrigger("Swap");
            StartCoroutine(DelayedWeaponChange(0)); // DelayedWeaponChange �ڷ�ƾ�� �����Ͽ� 0.6�� �Ŀ� �ڵ� ��� ����
        }
        else if(Input.GetButtonDown("Swap2"))
        {
            //����2�� ����ִٸ� return;
            if (slotWeapons[1] == null)
            {
                return;
            }
            //Todo : ���� ����ϰ��ִ� ��� ������Կ��ִ��� Ȯ���ϰ� 2���̶�� �״�γ��ΰ�
            //1��, Ȥ�� 3���̶��  ���� ���� ����ְų� �������̶�� �ִϸ��̼��� ����ϰ�
            //2���� �����ִ� ���⸦ �ٽ�Ȱ��ȭ��Ű�� ik�� �����Ѵ�.
            if (ActiveSlot[1] == true)
            {
                return;
            }
            ActiveSlot[0] = false;
            ActiveSlot[1] = true;
            ActiveSlot[2] = false;
            frontAnimator.SetTrigger("Swap");
            StartCoroutine(DelayedWeaponChange(1));
        }
        else if(Input.GetButtonDown("Swap3"))
        {
            //Todo : ���� ����ϰ��ִ� ��� ������Կ��ִ��� Ȯ���ϰ� 3���̶�� �״�γ��ΰ�
            //1��, Ȥ�� 2���̶��  ���� ���� ����ְų� �������̶�� �ִϸ��̼��� ����ϰ�
            //3���� �����ִ� ���⸦ �ٽ�Ȱ��ȭ��Ű�� ik�� �����Ѵ�.
            if (ActiveSlot[2] == true)
            {
                return;
            }
            ActiveSlot[0] = false;
            ActiveSlot[1] = false;
            ActiveSlot[2] = true;
            frontAnimator.SetTrigger("Swap");
            StartCoroutine(DelayedWeaponChange(2));
        }
    }

    public void EquipWeapon(string weaponName,int First)
    {
        if(photonView.IsMine)
        {
            if (First == 0)
            {   //���� ù��° ������ ����־ �����Ȱ����;    
                //����2,3�� ��Ȱ��ȭ�Ѵ�.
                ActiveSlot[2] = false;
                ActiveSlot[1] = false;
                //����1�� Ȱ��ȭ�Ѵ�.
                ActiveSlot[0] = true;
                //ù��° ������ �������� ���� ���������� �ٲ۴�.
                weaponName = weaponName.Replace("(get)(Clone)", "");
                slotWeapons[0] = weaponName;
                frontAnimator.SetTrigger("Swap");
                StartCoroutine(GetWeapon(weaponName));
            }
            else if (First == 1)
            {   //���� �ι�° ������ ����־ �����Ȱ����
                //3��° ������ Ȱ��ȭ�Ȼ��¶��
                //����1,3�� ��Ȱ��ȭ�Ѵ�.
                ActiveSlot[2] = false;
                ActiveSlot[0] = false;
                //����2�� Ȱ��ȭ�Ѵ�.
                ActiveSlot[1] = true;
                //�ι�° ������ �������� ���� ���������� �ٲ۴�.
                weaponName = weaponName.Replace("(get)(Clone)", "");
                slotWeapons[1] = weaponName;
                frontAnimator.SetTrigger("Swap");
                StartCoroutine(GetWeapon(weaponName));
            }
            else
            {
                //���� 1,2�������� ��� ���Ⱑ �����Ǿ��ִ� �����
                //���� Ȱ��ȭ�� ������ ����������� üũ�ϰ�
                //�׽����� �������� ���� ���������� �ٲ۴�.
                weaponName = weaponName.Replace("(get)(Clone)", "");
                slotWeapons[CheckActiveslot()] = weaponName;
                frontAnimator.SetTrigger("Swap");
            }
        }

    }

    public int CheckActiveslot()
    {   //���� Ȱ��ȭ�� ������ üũ�ϴ��Լ�
        //�ƹ����⵵ �ȸ������´� �⺻����3�������� Ȱ��ȭ�Ǿ��ϱ⿡ 2�� �⺻���̴�.
        int ActiveSlotNum=2;
        for(int i =0; i<3; i++)
        {
            if(ActiveSlot[i] == true)
            {
                ActiveSlotNum = i;
            }
        }

        return ActiveSlotNum;
    }

    public int SearchWeapon()
    {// ���� �������ִ� ���Ⱑ ��ü����Ʈ�� ����ε����� �ִ��� Ȯ���ϴ��Լ�. ���ٸ� 999�� ��ȯ�Ѵ�.
        for(int i=0; i<Equip_weapons.Length;i++)
        {
            if (slotWeapons[CheckActiveslot()] == Equip_weapons[i].name)
            {
                return i;
            }
        }
        return 999;
    }

    public void TurnWeapon(string weaponName)
    {// �տ��ִ� ����迭�� �ƴѰ��� ��β��� �Լ�.

        if (photonView.IsMine)
        { 
            for(int i =0; i< Equip_weapons.Length; i++)
            {
                if (Equip_weapons[i].name == weaponName)
                {
                    photonView.RPC("LastChange_T", RpcTarget.All, i);
                    //Equip_weapons[i].SetActive(true);
                    Debug.LogFormat("{0},{1}", Equip_weapons[i].name, Front_weapons[i].name);
                    Front_weapons[i].SetActive(true);
                }
                else
                {
                    photonView.RPC("LastChange_F", RpcTarget.All, i);
                    //Equip_weapons[i].SetActive(false);
                    Front_weapons[i].SetActive(false);
                }
            }
        }
    }


    IEnumerator DelayedWeaponChange(int number)
    {
        yield return new WaitForSeconds(0.6f);

        playerIK.ChangeIK(slotWeapons[number]);
        frontIK.ChangeIK(slotWeapons[number]);
        TurnWeapon(slotWeapons[number]);
    }
    [PunRPC]
    public void LastChange_T(int i)
    {
        Equip_weapons[i].SetActive(true);
    }

    [PunRPC]
    public void LastChange_F(int i)
    {
        Equip_weapons[i].SetActive(false);
    }
    IEnumerator GetWeapon(string weaponName)
    {
        if (photonView.IsMine)
        {
            yield return new WaitForSeconds(0.6f); // 0.6�� ���

            //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
            playerIK.ChangeIK(weaponName);
            //Debug.Log("format1");
            //1��Ī������ IK���ѹٲ��ش�.
            frontIK.ChangeIK(weaponName);
            //Debug.Log("format2");

            //�ƴ� �͵��� ��� false�� �ٲٰ� �´°͹���� true�� �ٲ۴�.

            //photonView.RPC("TurnWeapon", RpcTarget.All, weaponName);

            // =============== Legacy : Shin =====================

            TurnWeapon(weaponName);

            // =============== Legacy : Shin =====================
        }

    }


    public void GetWeapon_SSC(string weaponName)
    {
        StartCoroutine(GetWeapon(weaponName));
    }
}

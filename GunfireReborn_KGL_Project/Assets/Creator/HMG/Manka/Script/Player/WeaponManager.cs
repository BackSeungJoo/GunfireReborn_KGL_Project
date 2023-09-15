using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponManager : MonoBehaviourPun
{
    //���� Ȱ��ȭ���ִ� ������ Ȯ���ϱ����Ѻ���
    public bool[] ActiveSlot;
    //���� ���Կ� �ִ� ������ Ȯ���ϱ����� ����
    public string[] slotWeapons;
    //������������ �������� ������ ����迭
    public GameObject[] weaponPrefabs;
    //�÷��̾��� IK�Լ��� ������������ ����
    private IK playerIK;
    //������������ �ȿ��ִ� ����������Ʈ���� �����״� �ϱ����� ����迭
    public GameObject[] Equip_weapons;
    //1��Ī ������Ʈ��;
    public GameObject[] Front_weapons;
    //����Ʈ�÷��̾��� IK�Լ��� ������������ ����
    private FrontIK frontIK;
    //����Ʈ �÷����� �����ϱ����Ѻ���
    public GameObject frontPlayer;
    // Update is called once per frame
    private Animator frontAnimator;

    public GameObject playerGun;

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
        playerIK = gameObject.GetComponent<IK>();
        //
        frontPlayer = Camera.main.transform.GetChild(0).gameObject;

        frontIK = frontPlayer.GetComponent<FrontIK>();
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
        if (!photonView.IsMine)
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

            /*//1�����⿡ �ش��ϴ� ik�� ����
            Debug.Log("change1");
            playerIK.ChangeIK(slotWeapons[0]);
            //����Ʈik�� ����
            Debug.Log("change2");
            frontIK.ChangeIK(slotWeapons[0]);
            //1���� �����ִ� ���⸦ �ٽ�Ȱ��ȭ��Ŵ.
            Debug.Log("change3");
            //1���� �ƴ� �ٸ�������� ��Ȱ��ȭ��Ŵ front�� �ִ� ���⵵ ��Ȱ��ȭ��Ŵ
            TurnWeapon(slotWeapons[0]);*/
            StartCoroutine(DelayedWeaponChange1()); // DelayedWeaponChange �ڷ�ƾ�� �����Ͽ� 0.6�� �Ŀ� �ڵ� ��� ����
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
           /* playerIK.ChangeIK(slotWeapons[1]);
            //����Ʈik�� ����
            frontIK.ChangeIK(slotWeapons[1]);
            Equip_weapons[SearchWeapon()].SetActive(true);
            TurnWeapon(slotWeapons[1]);*/
            StartCoroutine(DelayedWeaponChange2());
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
            /*playerIK.ChangeIK(slotWeapons[2]);
            //����Ʈik�� ����
            frontIK.ChangeIK(slotWeapons[2]);
            Equip_weapons[SearchWeapon()].SetActive(true);
            TurnWeapon(slotWeapons[2]);*/
            StartCoroutine(DelayedWeaponChange3());
        }
    }

    public void EquipWeapon(string weaponName,int First)
    {
        if (First == 0)
        {   //���� ù��° ������ ����־ �����Ȱ����
            Debug.Log("EquipWeapon1");
            Debug.LogFormat("{0}", weaponName);    
            //����2,3�� ��Ȱ��ȭ�Ѵ�.
            ActiveSlot[2] = false;
            ActiveSlot[1] = false;
            //����1�� Ȱ��ȭ�Ѵ�.
            ActiveSlot[0] = true;
            //ù��° ������ �������� ���� ���������� �ٲ۴�.
            weaponName = weaponName.Replace("(get)", "");
            slotWeapons[0] = weaponName;
            frontAnimator.SetTrigger("Swap");
            /* //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
             playerIK.ChangeIK(weaponName);
             //Debug.Log("format1");
             //1��Ī������ IK���ѹٲ��ش�.
             frontIK.ChangeIK(weaponName);
             //Debug.Log("format2");

             //�ƴ� �͵��� ��� false�� �ٲٰ� �´°͹���� true�� �ٲ۴�.
             TurnWeapon(weaponName);*/
            Debug.LogFormat("{0}", weaponName);
            StartCoroutine(GetWeapon(weaponName));
        }
        else if (First == 1)
        {   //���� �ι�° ������ ����־ �����Ȱ����
            Debug.Log("EquipWeapon2");
            //3��° ������ Ȱ��ȭ�Ȼ��¶��
            //����1,3�� ��Ȱ��ȭ�Ѵ�.
            ActiveSlot[2] = false;
            ActiveSlot[0] = false;
            //����2�� Ȱ��ȭ�Ѵ�.
            ActiveSlot[1] = true;
            //�ι�° ������ �������� ���� ���������� �ٲ۴�.
            weaponName = weaponName.Replace("(get)", "");
            slotWeapons[1] = weaponName;
            frontAnimator.SetTrigger("Swap");
         /*   //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
            playerIK.ChangeIK(weaponName);
            //1��Ī������ IK���ѹٲ��ش�.
            frontIK.ChangeIK(weaponName);
            //�ƴ� �͵��� ��� false
            TurnWeapon(weaponName);*/
            StartCoroutine(GetWeapon(weaponName));
        }
        else
        {
            Debug.Log("EquipWeapon");
            //���� 1,2�������� ��� ���Ⱑ �����Ǿ��ִ� �����
            //���� Ȱ��ȭ�� ������ ����������� üũ�ϰ�
            //�׽����� �������� ���� ���������� �ٲ۴�.
            weaponName = weaponName.Replace("(get)", "");
            slotWeapons[CheckActiveslot()] = weaponName;
            frontAnimator.SetTrigger("Swap");
            StartCoroutine(GetWeapon(weaponName));
            /* //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
             playerIK.ChangeIK(weaponName);
             //1��Ī������ IK���ѹٲ��ش�.
             frontIK.ChangeIK(weaponName);
             //�׸��� �ȿ��ִ� ���⸦ Ȱ��ȭ��Ų��.
             Equip_weapons[SearchWeapon()].SetActive(true);
             //1��Ī ���� ���⵵ ��ü�Ѵ�.
             Front_weapons[SearchWeapon()].SetActive(true);*/
            //
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

    public void WeaponDrop()
    {
        int ActiveWeaponNum = 999;
        //�����ϰ��ִ� ������ ������ Ȯ���Ѵ�.
        ActiveWeaponNum = SearchWeapon();

        if (ActiveWeaponNum != 999)
        {//���� �����������ִ� ���Ⱑ ��ü���⸮��Ʈ�� �ִٸ�  (���� ���ٸ� 999����)
         //prefabs�迭���� �� �ε�����ȣ�� instantiate�Ѵ�.

            // �÷��̾��� ���� ��ġ�� �����´�. (��: transform.position�� ���)
            Vector3 playerPosition = transform.position;

            // �÷��̾��� ���� ��ġ���� z ��ǥ�� +2�� �̵��� ��ġ�� Instantiate�Ѵ�.
            Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 2);

            // Instantiate �Լ��� ����Ͽ� ���⸦ �����Ѵ�.
            Instantiate(weaponPrefabs[ActiveWeaponNum], spawnPosition, Quaternion.identity);
            // ���������� ���տ� �ִ� ���⸦ false�� ��Ȱ��ȭ��Ų��.
            Equip_weapons[ActiveWeaponNum].SetActive(false);
        }
        else
        {
            Debug.LogFormat("�����������ִ� ���Ⱑ ��ü ����迭�� ����.");
        }
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
        for(int i =0; i< Equip_weapons.Length; i++)
        {
            if (Equip_weapons[i].name == weaponName )
            {
                Equip_weapons[i].SetActive(true);
                Debug.LogFormat("{0},{1}", Equip_weapons[i].name, Front_weapons[i].name);
                Front_weapons[i].SetActive(true);
            }
            else
            {
                Equip_weapons[i].SetActive(false);
                Front_weapons[i].SetActive(false);
            }
        }
    }


    IEnumerator DelayedWeaponChange1()
    {
        yield return new WaitForSeconds(0.6f); // 0.6�� ���

        // 1�� ���⿡ �ش��ϴ� IK�� ����
        Debug.Log("change1");
        playerIK.ChangeIK(slotWeapons[0]);

        // ����Ʈ IK�� ����
        Debug.Log("change2");
        frontIK.ChangeIK(slotWeapons[0]);

        // 1���� ���� �ִ� ���⸦ �ٽ� Ȱ��ȭ��Ŵ
        Debug.Log("change3");

        // 1���� �ƴ� �ٸ� ������� ��Ȱ��ȭ��Ŵ, front�� �ִ� ���⵵ ��Ȱ��ȭ��Ŵ
        TurnWeapon(slotWeapons[0]);
    }
    IEnumerator DelayedWeaponChange2()
    {
        yield return new WaitForSeconds(0.6f); // 0.6�� ���

        // 2�� ���⿡ �ش��ϴ� IK�� ����
        //Debug.Log("change1");
        playerIK.ChangeIK(slotWeapons[1]);

        // ����Ʈ IK�� ����
        //Debug.Log("change2");
        frontIK.ChangeIK(slotWeapons[1]);

        // 2���� ���� �ִ� ���⸦ �ٽ� Ȱ��ȭ��Ŵ
        //Debug.Log("change3");

        // 2���� �ƴ� �ٸ� ������� ��Ȱ��ȭ��Ŵ, front�� �ִ� ���⵵ ��Ȱ��ȭ��Ŵ
        TurnWeapon(slotWeapons[1]);
    }

    IEnumerator DelayedWeaponChange3()
    {
        yield return new WaitForSeconds(0.6f); // 0.6�� ���

        // 2�� ���⿡ �ش��ϴ� IK�� ����
        //Debug.Log("change1");
        playerIK.ChangeIK(slotWeapons[2]);

        // ����Ʈ IK�� ����
        //Debug.Log("change2");
        frontIK.ChangeIK(slotWeapons[2]);

        // 2���� ���� �ִ� ���⸦ �ٽ� Ȱ��ȭ��Ŵ
        //Debug.Log("change3");

        // 2���� �ƴ� �ٸ� ������� ��Ȱ��ȭ��Ŵ, front�� �ִ� ���⵵ ��Ȱ��ȭ��Ŵ
        TurnWeapon(slotWeapons[2]);
    }

    IEnumerator GetWeapon(string weaponName)
    {
        yield return new WaitForSeconds(0.6f); // 0.6�� ���
                                               
        //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
        playerIK.ChangeIK(weaponName);
        //Debug.Log("format1");
        //1��Ī������ IK���ѹٲ��ش�.
        frontIK.ChangeIK(weaponName);
        //Debug.Log("format2");

        //�ƴ� �͵��� ��� false�� �ٲٰ� �´°͹���� true�� �ٲ۴�.
        TurnWeapon(weaponName);
    }

}

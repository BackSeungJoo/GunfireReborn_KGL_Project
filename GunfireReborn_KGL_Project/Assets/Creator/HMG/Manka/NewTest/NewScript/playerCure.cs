using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class playerCure : MonoBehaviourPun
{
    public List<GameObject> otherPlayers;

    //ȸ���� Ui�̹���
    public Image recoveryBar;
    //�׾�¼ӵ�
    public float dieSpeed;
    //ȸ���ӵ�
    public float recoverySpeed;
    //���� ȸ�����൵
    private float currentRecovery = 1.0f;
    //�÷��̾� ���¸� �������� ����
    private playerHp hp;
    //ȸ�������� üũ�ϴ� ����
    private bool isCure;
    //���� ����
    public RaycastHit hitInfo;
    //���̸� �������� ķ
    public CinemachineVirtualCamera cam;
    //������ �����Ÿ�
    private float rayDistance = 5f;
    // Start is called before the first frame update
    void Start()
    {
        otherPlayers = new List<GameObject>();
        hp = GetComponent<playerHp>();
        cam = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        photonView.RPC("GoingDead", RpcTarget.All);

        if (SearchPlayer() ==2)
        {//�ֺ��� �÷��̾ �������
         //���⼭ ����ĳ��Ʈ�� �߻��ؼ� �׷α� ������ �÷��̾��ϰ�� cureOther()�� ����ϰԸ�����\
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, rayDistance))
            {   //���̰� �浹�Ѱ��
                Debug.DrawRay(cam.transform.position, cam.transform.forward * rayDistance, Color.blue);
                if (hitInfo.transform.CompareTag("Player")&&hitInfo.collider.gameObject.GetComponent<playerHp>().state == playerHp.State.groggy)
                {
                    if (Input.GetKey("Get"))
                    {
                        photonView.RPC("CureOther", RpcTarget.All);
                    }
                    else
                    {
                        // ȸ�� �ߴ� �� ȸ�� ���¸� �ʱ�ȭ�մϴ�.
                        photonView.RPC("StopCure", RpcTarget.All);
                    }
                }
            }
            else
            {
                isCure = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            otherPlayers.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            otherPlayers.Remove(other.gameObject);
        }
    }

    //�������ִ� �÷��̾ �ִ��� �Ǻ��ϴ� �Լ�
    private int SearchPlayer()
    {
        int peopleNum = 0;
        for(int i=0; i<otherPlayers.Count; i++)
        {
            if (otherPlayers[i] !=null)
            {
                peopleNum++;
            }
        }
        if(peopleNum ==0)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    [PunRPC]
    private  void CureOther()
    {
        isCure =true;
        currentRecovery += recoverySpeed * Time.deltaTime;
        recoveryBar.fillAmount = currentRecovery;
        if (currentRecovery >= 1.0f)
        {
            // ȸ���� �Ϸ�Ǹ� ��Ȱ ������ ȣ���ϰų� �ٸ� �ʿ��� �۾��� �����մϴ�.
            hp.state = playerHp.State.play;
            hp.curHealth = hp.maxHealth / 2;
            isCure = false;
            currentRecovery = 1.0f;
        }
    }
    [PunRPC]
    private void GoingDead()
    {

        if (isCure == false)
        {
            currentRecovery -= dieSpeed * Time.deltaTime;
            recoveryBar.fillAmount = currentRecovery;
        }
    }

    [PunRPC]

    private void StopCure()
    {
        currentRecovery = 0.0f;
        recoveryBar.fillAmount = currentRecovery;
        isCure = false;
    }
}

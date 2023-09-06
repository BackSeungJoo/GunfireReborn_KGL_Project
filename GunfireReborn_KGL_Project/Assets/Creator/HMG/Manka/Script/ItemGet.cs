using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGet : MonoBehaviour
{

    GameObject nearObject;

    Item nearItem;

    PlayerInput input;


    //���̸� �������� ķ
    public CinemachineVirtualCamera cam;

    //���� ����
    public RaycastHit hitInfo;

    //������ �����Ÿ�
    private float rayDistance = 10f;

    //���̰� �������� �����Һ���
    Vector3 hitPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        ShotRaycast();
    }
    private void ShotRaycast()
    {
        

        //����ĳ��Ʈ �߻�
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, rayDistance))
        {   //���̰� �浹�Ѱ��
            Debug.DrawRay(cam.transform.position, cam.transform.forward * rayDistance, Color.red);
            if (hitInfo.transform.CompareTag("weapon"))
            {
                GetItem();
            }

        }
        else
        {   //���̰� �ƹ��͵� �����������
            hitPosition = cam.transform.position + cam.transform.forward * rayDistance;
        }
        


    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "weapon")
        {
            //���� Ʈ���ſ� ������� nearObject�� ���⸦��´�.
            nearObject = other.gameObject;
            nearItem = nearObject.GetComponent<Item>(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "weapon")
        {
            //���� Ʈ���Ÿ� ������� nearObject��  null�� ����.
            nearObject = null;
        }
    }

    private void GetItem()
    {
        
        if (nearObject != null)
        {
            Debug.LogFormat("{0}", nearObject.name);
            if (nearObject.tag == "weapon" && input.get == true)
            {//������ �ְ�, ������ ���� �Է����ް�, ����ĳ��Ʈ�� �¾�����
             //�ϴ��� �ӽ������� �������� �ı��ϰ� ������
                Destroy(nearObject.gameObject);
                input.get = false;
            }
        }
       
    }
}

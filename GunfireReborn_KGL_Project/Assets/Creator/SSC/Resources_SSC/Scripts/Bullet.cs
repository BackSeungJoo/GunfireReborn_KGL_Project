using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerAttack shoot;
    public CinemachineVirtualCamera cam;
    private Rigidbody myRigid = default;
    private float speed = 30.0f;

    private float randposMin = -3.0f;
    private float randposMax = 3.0f;
   
    // Start is called before the first frame update
    void Awake()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        shoot = FindObjectOfType<PlayerAttack>();
        myRigid = GetComponent<Rigidbody>();

        //Vector3 target = shoot.hitPoint - transform.position;
        //Vector3 targetnormal = target.normalized;
        if (shoot.useSkill == true)
        {
            Vector3 randomHit = cam.transform.forward * speed ;
            Vector3 random = new Vector3
                (randomHit.x + Random.Range(randposMin, randposMax),
                randomHit.y + Random.Range(randposMin, randposMax),
                randomHit.z + Random.Range(randposMin, randposMax));
            myRigid.velocity = random;

            //Debug.Log("���ν�Ƽ�� ����� ����? : " + randomHit);

            //myRigid.velocity = randomHit * speed;
            //target = randomHit - transform.position;

            //myRigid.velocity = target.normalized * speed;

            return;
        }

        myRigid.velocity = cam.transform.forward * speed;        

        //Debug.Log("�Ѿ� �����̼� �� : " + transform.rotation);

        //Debug.Log("��ǥ�� �Ѿ� ������ �Ÿ� : " + target + " ��ǥ�� ���� : " + targetnormal);

        ////Debug.Log("Ÿ�� ���� ��ֶ���� : " + target.normalized + " �Ѿ��� ���ԵǴ� ���ν�Ƽ �� : " + myRigid.velocity);

        //if (Mathf.Abs(target.z) <= 0.5f)
        //{
        //    myRigid.velocity = cam.transform.forward * speed;
        //}
    }


}

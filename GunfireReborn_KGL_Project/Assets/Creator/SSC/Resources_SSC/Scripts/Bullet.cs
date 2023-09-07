using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerController shoot;
    private Rigidbody myRigid = default;
    private float speed = 30.0f;

    private float randposMin = -1.0f;
    private float randposMax = 1.0f;
   
    // Start is called before the first frame update
    void Start()
    {
        shoot = FindObjectOfType<PlayerController>();
        myRigid = GetComponent<Rigidbody>();

        Vector3 target = shoot.hitPoint - transform.position;
        Vector3 targetnormal = target.normalized;
        myRigid.velocity = target.normalized * speed;
        
        Debug.Log("Ÿ�� ���� ��ֶ���� : " + target.normalized + " �Ѿ��� ���ԵǴ� ���ν�Ƽ �� : " + myRigid.velocity);
        if(shoot.useSkill == true )
        {
            Vector3 randomHit = new Vector3(shoot.hitPoint.x + Random.Range(randposMin, randposMax), shoot.hitPoint.y + Random.Range(randposMin, randposMax), shoot.hitPoint.z);
            target = randomHit - transform.position;

            myRigid.velocity = target.normalized * speed;
            
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Enumy"))
        {
            Debug.Log("�����");
            Destroy(gameObject);
        }
    }

}

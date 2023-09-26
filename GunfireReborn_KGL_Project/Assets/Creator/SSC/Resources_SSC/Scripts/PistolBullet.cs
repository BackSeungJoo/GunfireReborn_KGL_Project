using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Photon.Pun;
using System.Runtime.Serialization;
using TMPro;

public class PistolBullet : MonoBehaviourPun
{

    private WaitForSeconds poolingTime;

    [SerializeField] private GameObject damageText;

    private TextMeshProUGUI damageSetting;

    public int bulletDamage;

    private void Awake()
    {
        damageSetting = damageText.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        poolingTime = new WaitForSeconds(5f);
    }
    private void OnEnable()
    {
        // ========= ���� �׽�Ʈ �ϸ鼭 ���Ϳ��� ��¼��� ( ������ƮǮ���� �Ǵ� ���� ) �ڷ�ƾ�� ���� ���̴��� Ȯ���ؾ��� ===================

        StartCoroutine(DestroyBullet(P_PoolObjType.PISTOLBULLET));

        // ========= ���� �׽�Ʈ �ϸ鼭 ���Ϳ��� ��¼��� ( ������ƮǮ���� �Ǵ� ���� ) �ڷ�ƾ�� ���� ���̴��� Ȯ���ؾ��� ===================

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            damageSetting.text = "" + bulletDamage;
            damageSetting.color = Color.yellow;

            Instantiate(damageText, transform.position, Quaternion.identity);

            EnemyHealth health = other.GetComponent<EnemyHealth>();
            health.EnemyTakeDamage(bulletDamage);

            PhotonPoolManager.P_instance.CoolObj(this.gameObject, P_PoolObjType.PISTOLBULLET);

        }
        else if (other.CompareTag("LuckyShotPoint"))
        {
            damageSetting.text = bulletDamage * 2 + "!";
            damageSetting.color = Color.red;

            Instantiate(damageText, transform.position, Quaternion.identity);

            EnemyHealth health = GFunc.FindRootObj(other.gameObject).GetComponent<EnemyHealth>();
            health.EnemyTakeDamage(bulletDamage * 2);

            PhotonPoolManager.P_instance.CoolObj(this.gameObject, P_PoolObjType.PISTOLBULLET);
        }
    }

    private IEnumerator DestroyBullet(P_PoolObjType type)
    {
        yield return poolingTime;
        PhotonPoolManager.P_instance.CoolObj(this.gameObject, type);
    }

}

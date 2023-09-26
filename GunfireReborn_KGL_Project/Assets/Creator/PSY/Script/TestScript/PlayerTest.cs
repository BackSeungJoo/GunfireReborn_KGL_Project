using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    private UIManager uiManager;

    public List<ShopItemBox> shopScripts = new List<ShopItemBox>();  // ���� ���� �ִ� ShopScript�� List�� ��´�

    private float speed = 5f;

    private bool isShop = false;
    private bool isBlackSmith = false;

    private void Awake()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");  // Item �±װ� �޸� ������Ʈ�� �迭�� �ִ´�.
                                                                         // �ش� ������Ʈ���� ShopScript�� ������ �ֱ� ����.
        for (int i = 0; i < items.Length; i++)
        {
            shopScripts.Add(items[i].GetComponent<ShopItemBox>());  // ������ �� �±װ� �������� ������Ʈ�鿡�� ShopScript�� �̾Ƽ� List�� �ִ´�.
        }
    }

    private void Start()
    {
        uiManager = GameObject.Find("@Managers").GetComponent<UIManager>();
    }
    private void Update()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //transform.position += Time.deltaTime * speed * new Vector3(h, 0f, v);

        if (isShop && Input.GetKeyDown(KeyCode.F))
        {
            uiManager.SetActiveShopPopup(true);

            for ( int i = 0; i < shopScripts.Count; i++ )
            {
                shopScripts[i].enabled = true;  // ������ ������ ������ ShopScript�� Ȱ��ȭ�Ѵ�.
            }
        }

        if ( isBlackSmith && Input.GetKeyDown(KeyCode.F))
        {
            uiManager.SetActiveBlackSmith(true);
        }

        if ( Input.GetKeyDown(KeyCode.Tab) )
        {
            uiManager.SetActiveInven(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Shop"))
        {
            isShop = true;
        }
        else if ( other.tag == ("BlackSmith"))
        {
            isBlackSmith = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isShop = false;
        isBlackSmith = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadAndDropItem : MonoBehaviour
{
    private SetDropItem _SetDropItem;
    private GameObject _ItemDropManager;

    private void Awake()
    {
        // ���̾��Ű â���� _ItemDropManager ������Ʈ�� ã�ƿ´�.
        _ItemDropManager = GameObject.Find("ItemDropManager");

        if( _ItemDropManager != null )
        {
            _SetDropItem = _ItemDropManager.GetComponent<SetDropItem>();
        }
    }

    private void OnDisable()
    {
        _SetDropItem.DropItem(gameObject.transform);
    }
}

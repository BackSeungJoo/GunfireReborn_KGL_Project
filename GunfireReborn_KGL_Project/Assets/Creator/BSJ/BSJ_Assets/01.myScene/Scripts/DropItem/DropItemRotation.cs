using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemRotation : MonoBehaviour
{
    // ȸ���� ��
    public float rotateValue;

    private void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * rotateValue);
    }

}

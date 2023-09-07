using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RayCastShot : MonoBehaviour
{
    //���̸� �׸� ������
    private LineRenderer RayLineRenderer;

    //���̸� �������� ķ
    public CinemachineVirtualCamera cam;

    //���� ����
    public RaycastHit hitInfo;

    //������ �����Ÿ�
    private float rayDistance = 100f;

    //���̰� �������� �����Һ���
    Vector3 hitPosition = Vector3.zero;

    void Start()
    {
        RayLineRenderer = GetComponent<LineRenderer>();

        //����� ���� �ΰ��� ����
        RayLineRenderer.positionCount = 2;
        //���η������� Ȱ��ȭ
        //RayLineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        ShotRaycast();
    }

    private void ShotRaycast()
    {
        

        //����ĳ��Ʈ �߻�
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo,rayDistance))
        {   //���̰� �浹�Ѱ��


            //���̰� �浹�� ��ġ����
            hitPosition = hitInfo.point;

        }
        else
        {   //���̰� �ƹ��͵� �����������
            hitPosition = cam.transform.position + cam.transform.forward * rayDistance;
        }

        //����ĳ��Ʈ ���η����� �׸��� �Լ�

        RayLineDraw();

    }

    private void RayLineDraw()
    {
        //���� �������� ķ�� ��ġ
        RayLineRenderer.SetPosition(0, cam.transform.position);
        //���� ������ ������ �浹��ġ
        RayLineRenderer.SetPosition(1, hitPosition);

    }
}

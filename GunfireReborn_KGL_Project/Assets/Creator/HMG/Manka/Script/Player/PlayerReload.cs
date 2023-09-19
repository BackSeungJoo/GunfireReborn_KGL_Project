using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerReload : MonoBehaviourPun
{

    private Animator playerAnimator;
    private IK1 playerIk;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        playerAnimator = gameObject.GetComponent<Animator>();
        playerIk = gameObject.GetComponent<IK1>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }
    }

    private void Reload()
    {
        Debug.LogFormat("���ε���~");
        //������������ �ڽĿ�����Ʈ�� Ȱ��ȭ�� ���⸦ ã�Ƽ� �÷����� ���� �ڽĿ�����Ʈ�� �������Ѵ�.
        //���� ik�� �������� �ִϸ��̼�������ϵ����Ѵ�.
        playerIk.enabled = false;
        playerAnimator.Play("Reloading", 1, 1);

        //���Ŀ� ik�� �ٽ�Ű�� Ȱ��ȭ�� ���⸦ �ٽÿ����� ������ġ�� �ǵ������Ѵ�.
        //��....
    }
}

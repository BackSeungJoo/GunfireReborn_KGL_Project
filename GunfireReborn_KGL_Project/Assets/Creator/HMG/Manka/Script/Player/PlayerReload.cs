using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerReload : MonoBehaviour
{

    private Animator playerAnimator;
    private IK playerIk;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = gameObject.GetComponent<Animator>();
        playerIk = gameObject.GetComponent<IK>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Reload"))
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

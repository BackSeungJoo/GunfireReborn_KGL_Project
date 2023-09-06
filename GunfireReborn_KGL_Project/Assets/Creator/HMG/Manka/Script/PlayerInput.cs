using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public string VMoveAxisName = "Vertical";
    public string HMoveAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
    public string reloadButtonName = "Reload";
    public string RotateName = "Mouse X";
    public string jumpName = "Jump";
    public string dashName = "Dash";
    public string swap1Name = "Swap1";
    public string swap2Name = "Swap2";
    public string swap3Name = "Swap3";
    public string getItem = "Get";

    public float VMove;
    public float HMove;
    public bool fire = default;
    public bool reload = default;
    public float RMove;
    public bool jump = default;
    public bool dash = default;
    public bool swap1 = default;
    public bool swap2 = default;
    public bool swap3 = default;
    public bool get = default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�� ������ ����� �Է��� ����
        //ToDo:���ӸŴ����� �ν��Ͻ��� null�� �ƴѵ� GameOver�����϶��� �Է��� ������� if�� ó���ؾ���
        //�뽬 ���¿��� �Է��� ��� ���ް� �ϱ� �뽬 false�� true�� �ٲٴ����� Movement ��ũ��Ʈ���� ó����
        if(dash == true)
        {
            return;
        }
        
        //Vmove�� ���� �Է°���
        VMove = Input.GetAxis(VMoveAxisName);
        //Hmove�� ���� �Է°���
        HMove = Input.GetAxis(HMoveAxisName);
        //fire�� ���� �Է°���
        fire = Input.GetButton(fireButtonName);
        //reload�� ���� �Է°���
        reload = Input.GetButtonDown(reloadButtonName);
        //Rmove�� ���� �Է°���
        RMove = Input.GetAxis(RotateName);
        //jump�� ���� �Է°���
        jump = Input.GetButtonDown(jumpName);
        //dash�� ���� �Է°���
        dash = Input.GetButtonDown(dashName);
        //swap1�� ���� �Է°���
        swap1 = Input.GetButtonDown(swap1Name);
        //swap2�� ���� �Է°���
        swap2 = Input.GetButtonDown(swap2Name);
        //swap3�� ���� �Է°���
        swap3 = Input.GetButtonDown(swap3Name);
        //get�� ���� �Է°���
        get = Input.GetButton(getItem);

        //ToDo:
        //InputManager���� fire,reload,dash,swap1,2,3, getItemó���� ������Ѵ�.

        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public string VMoveAxisName = "Vertical";
    public string HMoveAxisName = "Horizontal";
    public string fireButtonName = "Fire1";
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
    public float RMove;
    public bool jump = default;
    public bool dash = default;

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
        //Rmove�� ���� �Է°���
        RMove = Input.GetAxis(RotateName);
        //jump�� ���� �Է°���
        jump = Input.GetButtonDown(jumpName);
        //dash�� ���� �Է°���
        dash = Input.GetButtonDown(dashName);

        //ToDo:
        //InputManager���� fire,reload,dash,swap1,2,3, getItemó���� ������Ѵ�.

        
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player_Controller", menuName = "InputController/Player_Controller")]

public class Player : Input_Controller
{
    public override bool RetrieveJumpHoldInput()
    {
        return Input.GetButton("Jump");
    }

    public override bool RetrieveJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public override float RetrieveMoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool RetrieveDashInput()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public override bool RetrieveAttackInput()
    {
        return Input.GetButtonDown("Attack");
    }
}

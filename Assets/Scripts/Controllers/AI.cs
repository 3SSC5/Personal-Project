using UnityEngine;

[CreateAssetMenu(fileName = "Ai_Controller", menuName = "InputController/AIController")]

public class AI : Input_Controller
{
    public override bool RetrieveJumpHoldInput()
    {
        return false;
    }

    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        return 1f;
    }

    public override bool RetrieveDashInput()
    {
        return false;
    }

    public override bool RetrieveAttackInput()
    {
        return true;
    }
}

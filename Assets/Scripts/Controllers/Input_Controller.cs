using UnityEngine;

public abstract class Input_Controller : ScriptableObject
{
    public abstract bool RetrieveJumpInput();

    public abstract float RetrieveMoveInput();

    public abstract bool RetrieveJumpHoldInput();

    public abstract bool RetrieveDashInput();

    public abstract bool RetrieveAttackInput();

    
}

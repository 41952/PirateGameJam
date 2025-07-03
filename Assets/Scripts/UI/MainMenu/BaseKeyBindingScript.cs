using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class BaseKeyBindingScript : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset _actionAsset;

    public void ChangeBinding( )
    {
        Debug.Log("Binding changed");
        //InputHolder.ChangeControlsAction("Jump","<Keyboard>/B");
        
        //InputHolder.ChangeControlsComposite("Movement","Up","<Keyboard>/B");

        //InputSystem.onAnyButtonPress.CallOnce(context => Debug.Log(context));
        
    }
   
}

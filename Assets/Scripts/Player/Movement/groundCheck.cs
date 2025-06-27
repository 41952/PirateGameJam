using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool onGround;

    private void OnTriggerStay(Collider other)
    {
        if (!other.isTrigger && (other.gameObject.layer == 8 || other.gameObject.layer == 24))
        {
            onGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.isTrigger && (other.gameObject.layer == 8 || other.gameObject.layer == 24))
        {
            onGround = false;
        }
    }
}

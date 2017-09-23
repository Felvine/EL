using UnityEngine;

public abstract class ICharacterController : MonoBehaviour{
    public abstract void ReceiveHit ();
    public abstract Character GetUser ();
}

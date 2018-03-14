using Znko.Events;
using UnityEngine;
using Znko.Characters;


public abstract class ICharacterController : MonoBehaviour{
    public abstract void AddEvent(ICharacterEvent eventIn);
    public abstract Character GetUser ();
}

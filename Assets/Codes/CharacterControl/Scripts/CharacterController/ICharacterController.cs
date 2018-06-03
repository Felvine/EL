using Znko.Events;
using UnityEngine;
using Znko.Characters;
using Znko.Actions;

public delegate void AttackEventHandler (ICharacterController other, ICharacterAction attackCause);

public abstract class ICharacterController : MonoBehaviour{

    public abstract void AddEvent(ICharacterEvent eventIn);
    public abstract Character GetUser ();

    public abstract void ReceiveDamage(ICharacterController other, ICharacterAction attackCause);
    

    public abstract void CauseDamage(ICharacterController other, ICharacterAction attackCause);
}

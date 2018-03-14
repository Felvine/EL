using UnityEngine;
using Znko.Events;
using Znko.Characters;


public class ColliderBehaviour : MonoBehaviour
{

    private Character user;
    private CharacterCollider zCollider;

    public Character User {
        get {
            return user;
        }

        set {
            user = value;
        }
    }

    public CharacterCollider ZCollider {
        get {
            return zCollider;
        }

        set {
            zCollider = value;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (ZCollider.IsAttacking)
        {
            if (user.Faction == Character.Factions.Player)
            {
                if (other.tag != "Player")
                {
                    if (other.tag == "Enemy")
                        other.GetComponentInChildren<ICharacterController>().AddEvent(new ReceiveDamageEvent(10));
                }
            }
            else if (user.Faction == Character.Factions.Enemy)
            {
                if (other.tag == "Player")
                {
                    other.GetComponentInChildren<ICharacterController>().AddEvent(new ReceiveDamageEvent(10));
                }
            }
        }
    }
}

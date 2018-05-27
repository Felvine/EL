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

    void OnTriggerStay(Collider other)
    {
        ICharacterController targetController = other.GetComponentInChildren<ICharacterController>();
        if (targetController == null)
            targetController = other.GetComponentInParent<ICharacterController>();
        ICharacterController userController = GetComponentInParent<ICharacterController>();
        if (user.Properties.IsAttacking && !targetController.GetUser().Properties.IsInvulnerable)
        {
            if (user.Faction == Character.Factions.Player)
            {
                if (other.tag != "Player")
                {
                    if (other.tag == "Enemy")
                    {
                        userController.AddEvent(new SetAttackEvent(false));
                        targetController.AddEvent(new ReceiveDamageEvent(10));
                        targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("ReceiveHit")));
                    }
                }
            }
            else if (user.Faction == Character.Factions.Enemy)
            {
                if (other.tag == "Player")
                {
                    userController.AddEvent(new SetAttackEvent(false));
                    targetController.AddEvent(new ReceiveDamageEvent(10));
                    targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("Fall")));
                }
            }
        }
    }
}

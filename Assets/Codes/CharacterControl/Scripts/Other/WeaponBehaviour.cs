using Znko.Events;
using UnityEngine;
using Znko.Characters;

public class WeaponBehaviour : MonoBehaviour {

    private Character user;

    public Character User {
        get {
            return user;
        }

        set {
            user = value;
        }
    }
    //void OnCollisionStay(Collision collisionInfo)
    //{
    //    Debug.Log("");
    //}

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



    //void OnTriggerEnter(Collider other)
    //{
    //    ICharacterController targetController = other.GetComponentInParent<ICharacterController>();
    //    if (targetController == null)
    //        return;
    //    if (user.Properties.IsAttacking && !targetController.GetUser().Properties.IsInvulnerable)
    //    {
    //        if (user.Faction == Character.Factions.Player)
    //        {
    //            if (other.tag != "Player")
    //            {
    //                if (other.tag == "Enemy")
    //                {
    //                    targetController.AddEvent(new ReceiveDamageEvent(10));
    //                }
    //            }
    //        }
    //        else if (user.Faction == Character.Factions.Enemy)
    //        {
    //            if (other.tag == "Player")
    //            {
    //                targetController.AddEvent(new ReceiveDamageEvent(10));
    //                targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("Fall")));
    //            }
    //        }
    //    }
    //}
}

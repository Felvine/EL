using Znko.Events;
using UnityEngine;

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


    void OnTriggerStay(Collider other)
    {
        ICharacterController targetController = other.GetComponentInChildren<ICharacterController>();
        ICharacterController userController = GetComponentInParent<ICharacterController>();
        if (user.Properties.IsAttacking)
        {
            if (user.Faction == Character.Factions.Player)
            {
                if (other.tag != "Player")
                {
                    if (other.tag == "Enemy")
                    {
                        targetController.AddEvent(new ReceiveDamageEvent(10));
                        userController.AddEvent(new SetAttackEvent(false));
                    }
                }
            }
            else if (user.Faction == Character.Factions.Enemy)
            {
                if (other.tag == "Player")
                {
                    targetController.AddEvent(new ReceiveDamageEvent(10));
                    targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("Fall")));
                    userController.AddEvent(new SetAttackEvent(false));
                }
            }
        }
    }



    //void OnTriggerEnter (Collider other) {
    //    ICharacterController targetController = other.GetComponentInChildren<ICharacterController>();
    //    if (user.Properties.IsAttacking) {
    //        if (user.Faction == Character.Factions.Player) {
    //            if (other.tag != "Player") {
    //                if (other.tag == "Enemy")
    //                {
    //                    targetController.AddEvent(new ReceiveDamageEvent(10));
    //                }
    //            }
    //        } else if (user.Faction == Character.Factions.Enemy) {
    //            if (other.tag == "Player") {
    //                targetController.AddEvent(new ReceiveDamageEvent(10));
    //                targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("Fall")));
    //            }
    //        }
    //    }
    //}
}

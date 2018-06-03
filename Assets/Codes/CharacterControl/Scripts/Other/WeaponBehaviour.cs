using Znko.Events;
using UnityEngine;
using Znko.Characters;
using System;
using Znko.Actions;

public delegate void AttackRegisteredEventHandler(ICharacterController attacker, ICharacterController receiver, EventArgs e = null);

public class WeaponBehaviour : MonoBehaviour {

    public event AttackRegisteredEventHandler AttackRegistered;
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
        if (targetController == null)
            targetController = other.GetComponentInParent<ICharacterController>();
        if (targetController == null)
            return;
        ICharacterController userController = GetComponentInParent<ICharacterController>();
        if (user.Properties.IsAttacking && !targetController.GetUser().Properties.IsInvulnerable)
        {
            if (user.Faction == Character.Factions.Player)
            {
                if (other.tag != "Player")
                {
                    if (other.tag == "Enemy")
                    {
                        if (AttackRegistered != null)
                            AttackRegistered(userController, targetController);
                        targetController.AddEvent(new ReceiveDamageEvent(10));
                        targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("ReceiveHit")));
                    }
                }
            }
            else if (user.Faction == Character.Factions.Enemy)
            {
                if (other.tag == "Player")
                {
                    if (AttackRegistered != null)
                        AttackRegistered(userController, targetController);
                    targetController.AddEvent(new ReceiveDamageEvent(10));
                    targetController.AddEvent(new AddActionEvent(targetController.GetUser().GetAction("Fall")));
                }
            }
        }
    }
}

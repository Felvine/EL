using Znko.Events;
using UnityEngine;
using Znko.Characters;
using System;
using Znko.Actions;


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
        if (targetController == null)
            targetController = other.GetComponentInParent<ICharacterController>();
        if (targetController == null)
            return;
        ICharacterController userController = GetComponentInParent<ICharacterController>();
        if (user.Properties.IsAttacking && !targetController.GetUser().Properties.IsInvulnerable)
        {
            if (user.Faction == Character.Factions.Player)
            {
                if (other.tag == "Enemy")
                {
                    targetController.ReceiveDamage(userController, null);
                    userController.CauseDamage(targetController, null);
                }
            }
            else if (user.Faction == Character.Factions.Enemy)
            {
                if (other.tag == "Player")
                {
                    targetController.ReceiveDamage(userController, null);
                    userController.CauseDamage(targetController, null);
                }
            }
        }
    }
}

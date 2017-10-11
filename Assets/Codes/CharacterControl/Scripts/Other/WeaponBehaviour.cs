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

    void OnTriggerEnter (Collider other) {
        if (user.Properties.IsAttacking) {
            if (user.Faction == Character.Factions.Player) {
                if (other.tag != "Player") {
                    if (other.tag == "Enemy")
                        other.GetComponentInChildren<ICharacterController> ().ReceiveHit ();
                }
            } else if (user.Faction == Character.Factions.Enemy) {
                if (other.tag == "Player") {
                    other.GetComponentInChildren<ICharacterController> ().ReceiveHit ();
                }
            }
        }
    }
}

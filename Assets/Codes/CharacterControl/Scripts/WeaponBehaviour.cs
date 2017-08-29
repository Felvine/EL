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
            if (other.tag != "Player") {
                if (other.tag == "Enemy")
                    other.GetComponentInChildren<CharacterBehaviour>().ReceiveHit ();
            }
        }
    }
}

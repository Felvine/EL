using UnityEngine;
using Actions;
using System;

class GammaController : ActionBasedController {

    protected override void Awake () {
        Characters.Player.Create (transform);
        this.User = Characters.Player.Instance ();
    }
    protected override void Start () {
        base.Start ();
        GetComponentInChildren<WeaponBehaviour> ().User = this.User;
    }

    protected override ICharacterAction DetermineAction () {
        return DetermineActionFromInputs ();
    }

    private ICharacterAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        if (CurrentAction == null || CurrentAction.Priority == 0)
            this.User.Direction = moveDirection;
        if (moveDirection != Vector3.zero) {
            if (Input.GetKeyDown (KeyCode.Space))
                return this.User.GetAction ("Roll");
            else if (Input.GetKey (KeyCode.LeftShift))
                return this.User.GetAction ("Run");
            else
                return this.User.GetAction ("Walk");
        } else {
            if (Input.GetKeyDown (KeyCode.E)) {
                return this.User.GetAction ("ComboAttack");
            }
            if (Input.GetKeyDown (KeyCode.R))
                return this.User.GetAction ("Attack4");
        }
        return this.User.GetAction ("Idle");
    }

    protected override void BranchComboAttacks () {
        CharacterActionSequence currentActionSequence = (CurrentAction as CharacterActionSequence);
        if (currentActionSequence != null) {
            if (currentActionSequence.Actions[currentActionSequence.Step].IsFinishing () && (currentActionSequence.Step + 1) < currentActionSequence.Actions.Count) {
                ComboAction nextComboAction = (currentActionSequence.Actions[currentActionSequence.Step + 1] as ComboAction);
                if (nextComboAction != null) {
                    if (Input.GetKeyDown (KeyCode.E))
                        nextComboAction.Selected = 1;
                    else if (Input.GetKeyDown (KeyCode.R))
                        nextComboAction.Selected = 2;
                }
            }
        }
    }
}


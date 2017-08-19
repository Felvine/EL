using UnityEngine;
using Actions;

[RequireComponent(typeof(CharacterController))]
class BetaController : MonoBehaviour {
    Character player;
    ICharacterAction currentAction;
    ICharacterAction nextAction;

    void Start () {
        player = new Character (GetComponentInChildren<CharacterController> (), transform, GetComponentInChildren<Animator> ());
        player.AddAction ("Walk", new Move (player, 0.001f, 6f, "PlayerWalking"));
        player.AddAction ("Run", new Move (player, 0.001f, 10f, "PlayerRunning"));
        player.AddAction ("Roll", new Roll (player, 1.35f, 10, false));
        player.AddAction ("RollSequence", new CharacterActionSequence (new Roll (player, 0.2f, 2, false), new Roll (player, 0.1f, 1, true), new Roll (player, 0.2f, 2, false)));
        this.currentAction = null;
    }

    void Update () {
        if (currentAction != null) {
            ICharacterAction tempAction = DetermineActionFromInputs ();
            if (tempAction != null && !tempAction.IsPrimitive () && currentAction.AlmostDone ())
                nextAction = tempAction;
            if (currentAction.Execute () == Phase.NotActing) { 
                currentAction = nextAction;
                nextAction = null;
            }
        } else {
            currentAction = DetermineActionFromInputs ();
            if (currentAction == null)
                RemovePrimitiveAnimatorBools ();
        }
    }

    private ICharacterAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        moveDirection.Normalize ();
        if (currentAction == null || currentAction.IsPrimitive())
            player.Direction = moveDirection;
        if (moveDirection != Vector3.zero) {
            if (Input.GetKeyDown (KeyCode.Space))
                return player.GetAction ("Roll");
            else if (Input.GetKey (KeyCode.LeftShift))
                return player.GetAction ("Run");
            else
                return player.GetAction ("Walk");
        }
        return null;
    }

    private void RemovePrimitiveAnimatorBools () {
        player.Animations.SetBool ("PlayerWalking", false);
        player.Animations.SetBool ("PlayerRunning", false);
    }
}


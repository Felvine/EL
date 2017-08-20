using UnityEngine;
using Actions;

[RequireComponent(typeof(CharacterController))]
class BetaController : MonoBehaviour {
    Character player;
    ICharacterAction previousAction;
    ICharacterAction currentAction;
    ICharacterAction nextAction;

    public ICharacterAction NextAction {
        get {
            return nextAction;
        }

        set {
            if (nextAction == null || nextAction is ContinuousAction) {
                nextAction = value;
            } else if (value is TriggeredAction && ((TriggeredAction)value).AlmostDone ()) {
                nextAction = value;
            }
        }
    }

    void Start () {
        Animator playerAnimator = GetComponentInChildren<Animator> ();
        if (playerAnimator == null)
            throw new System.MissingFieldException ("Player need an animator");

        player = new Character (GetComponentInChildren<CharacterController> (), transform, playerAnimator);
        player.AddAction ("Walk", new Move (player, 0.001f, 6f, "PlayerWalking"));
        player.AddAction ("Run", new Move (player, 0.001f, 10f, "PlayerRunning"));
        player.AddAction ("Roll", new Roll (player, 1.35f, 10, "PlayerRoll", false));
    }

    void Update () {
        if (currentAction != null) {
            NextAction = DetermineActionFromInputs ();
            if (currentAction.Execute (null, NextAction) == Phase.NotActing) {
                previousAction = currentAction;
                currentAction = NextAction;
                nextAction = null;
            }
        } else {
            currentAction = DetermineActionFromInputs ();
        }
    }

    private ICharacterAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        moveDirection.Normalize ();
        if (currentAction == null || currentAction is ContinuousAction)
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
}


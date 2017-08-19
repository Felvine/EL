using UnityEngine;
using Actions;

[RequireComponent(typeof(CharacterController))]
class BetaController : MonoBehaviour {
    Character player;
    IAction currentAction;
    IAction nextAction;

    void Start () {
        player = new Character (GetComponentInChildren<CharacterController> (), transform);
        player.AddAction ("Walk", new Move (player, 0.001f, 6f));
        player.AddAction ("Run", new Move (player, 0.001f, 10f));
        player.AddAction ("Roll", new ActionSequence (new Roll (player, 0.2f, 2, false), new Roll (player, 0.1f, 1, true), new Roll (player, 0.2f, 2, false)));
        this.currentAction = null;
    }

    void Update () {
        if (currentAction != null) {
            IAction tempAction = DetermineActionFromInputs ();
            if (tempAction != null && !tempAction.IsPrimitive ())
                nextAction = tempAction;
            if (currentAction.Execute () == Phase.NotActing) { 
                currentAction = nextAction;
                nextAction = null;
            }
        } else {
            currentAction = DetermineActionFromInputs ();
        }
    }

    private IAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        moveDirection.Normalize ();
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


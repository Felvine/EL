using UnityEngine;
using Actions;

class GammaController : MonoBehaviour {
    public const float playerWalkSpeed = 6.0f;
    public const float playerRunSpeed = 10.0f;
    public const float playerWalkStep = 0.001f;
    public const float playerRollDuration = 1.40f;
    public const float playerRollLength = 10.0f;

    ControlledCharacter player;
    ICharacterAction previousAction;
    ICharacterAction currentAction;
    ICharacterAction nextAction;

    public ICharacterAction NextAction {
        get {
            return nextAction;
        }

        set {
            if (value != null) {
                if (nextAction == null || ((CharacterAction)nextAction).Priority <= ((CharacterAction)value).Priority) {
                    nextAction = value;
                }
            }
        }
    }

    void Start () {
        Animation playerAnimation = GetComponentInChildren<Animation> ();
        if (playerAnimation == null)
            throw new System.MissingFieldException ("Need Animation");
        player = new ControlledCharacter (transform, playerAnimation, GetComponent<CharacterController> ());
        player.AddAction ("Walk", new Move (player, playerWalkStep, playerAnimation.GetClip("Player_Walk"), playerWalkSpeed));
        player.AddAction ("Run", new Move (player, playerWalkStep, playerAnimation.GetClip ("Player_Run"), playerRunSpeed));
        player.AddAction ("Roll", new Roll (player, playerRollDuration, playerAnimation.GetClip("Player_Roll"),playerRollLength, false));
        player.AddAction ("Idle", new Idle (player, playerWalkStep, playerAnimation.GetClip ("Player_Idle")));
    }

    void Update () {
        if (currentAction != null) {
            if (currentAction.IsFinishing ())
                NextAction = DetermineActionFromInputs ();
            if (currentAction.Execute (previousAction, NextAction) == Phase.NotActing) {
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
        if (currentAction == null || ((CharacterAction)currentAction).Priority == 0)
            player.Direction = moveDirection;
        if (moveDirection != Vector3.zero) {
            if (Input.GetKeyDown (KeyCode.Space))
                return player.GetAction ("Roll");
            else if (Input.GetKey (KeyCode.LeftShift))
                return player.GetAction ("Run");
            else
                return player.GetAction ("Walk");
        }
        return player.GetAction ("Idle");
    }
}


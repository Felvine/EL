using UnityEngine;
using Actions;

public abstract class ActionBasedController : ICharacterController {
    private ControlledCharacter user;
    private ICharacterAction previousAction;
    private ICharacterAction currentAction;
    private ICharacterAction nextAction;
    public const float actionMinimumStep = 0.01f;


    protected ICharacterAction NextAction {
        get {
            return nextAction;
        }

        set {
            if (value != null) {
                if (nextAction == null || (nextAction.Priority <= value.Priority)) {
                    nextAction = value;
                }
            }
        }
    }

    protected ICharacterAction CurrentAction {
        get {
            return currentAction;
        }

        set {
            currentAction = value;
        }
    }

    protected ICharacterAction PreviousAction {
        get {
            return previousAction;
        }

        set {
            previousAction = value;
        }
    }

    protected ControlledCharacter User {
        get {
            return user;
        }

        set {
            user = value;
        }
    }

    protected virtual void Update () {
        if (CurrentAction != null) {
            BranchComboAttacks ();
            if (CurrentAction.IsFinishing ()) {
                NextAction = DetermineAction ();
            }
            if (CurrentAction.Execute (PreviousAction, NextAction) == Phase.NotActing) {
                PreviousAction = CurrentAction;
                CurrentAction = NextAction;
                nextAction = null;
            }
        } else {
            CurrentAction = DetermineAction();
        }
    }

    protected virtual void Awake () {
        Animation playerAnimation = GetComponentInChildren<Animation> ();
        if (playerAnimation == null)
            throw new System.MissingFieldException ("Need Animation");
        this.User = new ControlledCharacter (transform, playerAnimation, GetComponent<CharacterController> ());
    }

    protected virtual void Start () {

    }

    protected abstract ICharacterAction DetermineAction ();

    protected virtual void BranchComboAttacks () { }

    public override void ReceiveHit () {

    }

    public override Character GetUser () {
        return this.User;
    }
}

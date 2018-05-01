using UnityEngine;
using Znko.Actions;
using Znko.Events;
using Znko.Characters;
using System.Collections.Generic;

public abstract class ActionBasedController : ICharacterController {
    private Character user;
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

    protected Character User {
        get {
            return user;
        }

        set {
            user = value;
        }
    }

    protected List<ICharacterEvent> Events {
        get {
            return this.User.Events;
        }

        set {
            this.User.Events = value;
        }
    }

    protected virtual void Update () {
        ICharacterAction highestPriorityAction = DetermineAction();
        if (CharacterAction.CanInterrupt(currentAction, highestPriorityAction))
        {
            currentAction.PostActions(PreviousAction, this);
            CurrentAction = highestPriorityAction;
        }
        else if (CurrentAction != null) {
            BranchComboAttacks ();
            if (CurrentAction.IsFinishing ()) {
                NextAction = highestPriorityAction;
            }
            if (CurrentAction.Execute (PreviousAction, NextAction, this) == Phase.NotActing) {
                PreviousAction = CurrentAction;
                CurrentAction = NextAction;
                nextAction = null;
                ActionFinished();
            }
        } else {
            CurrentAction = highestPriorityAction;
        }
    }

    protected ICharacterAction ProcessEventQueue()
    {
        ICharacterAction result = null;
        ICharacterAction temp = null;
        foreach (ICharacterEvent ce in this.Events)
        {
            ce.Do();
            if (ce is AddActionEvent)
            {
                temp = ((AddActionEvent)ce).GetAction();
                if (temp != null && (result == null || result.Priority < temp.Priority))
                    result = temp;
            }
        }
        this.Events.Clear();
        return result;
    }

    protected virtual void Awake () {
        Animation playerAnimation = GetComponentInChildren<Animation> ();
        if (playerAnimation == null)
            throw new System.MissingFieldException ("Need Animation");
        this.User = new Character (transform, playerAnimation, GetComponent<CharacterController> ());
    }

    protected virtual void Start () {

    }

    protected abstract ICharacterAction DetermineAction ();

    protected virtual void BranchComboAttacks () { }

    public override Character GetUser () {
        return this.User;
    }

    public override void AddEvent(ICharacterEvent eventIn)
    {

        Debug.Log(eventIn.ToString() + Time.time);

        eventIn.SetUser(this.User);
        this.Events.Add(eventIn);
    }

    public virtual void ActionFinished()
    {

    }
}

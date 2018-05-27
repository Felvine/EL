using Znko.Actions;
using UnityEngine;
using Znko.Characters;
using UnityEngine.UI;
using System.Collections.Generic;
using Znko.AI;
using System;

public class SparkyController : AIController {
    private const int followDistance = 15;
    private const int attackDistance = 10;
    private bool clickedButton = false;

    static System.Random rnd = new System.Random ();
    private System.Collections.Generic.List<ICharacterAction> attacks = new System.Collections.Generic.List<ICharacterAction> ();

    protected override void Awake () {
        this.User = Characters.Sparky.Create (transform);
    }

    // Use this for initialization
    protected override void Start () {
        base.Start ();
        foreach (WeaponBehaviour wp in GetComponentsInChildren<WeaponBehaviour> ())
            wp.User = this.User;
        this.target = Characters.Player.Instance ();
        this.attacks.Add(this.User.GetAction("Bite"));
        this.attacks.Add(this.User.GetAction("TailSwipe"));
        this.attacks.Add(this.User.GetAction("Headbutt"));
        this.attacks.Add(this.User.GetAction("RushHeadbutt"));
        this.attacks.Add(this.User.GetAction("JumpAttack"));
    }


    protected override ICharacterAction DetermineAction ()
    {
        ICharacterAction buttonAction = DetermineActionFromUIForDebug();
        if (buttonAction != null)
            return buttonAction;
        ICharacterAction eventAction = ProcessEventQueue();
        ICharacterAction aiAction = DetermineActionFromAI();
        if (eventAction == null)
            return aiAction;
        else if (aiAction == null)
            return eventAction;
        if (eventAction.Priority > aiAction.Priority)
            return eventAction;
        else
            return aiAction;
    }

    private ICharacterAction DetermineActionFromUIForDebug()
    {
        if (clickedButton)
        {
            this.clickedButton = false;
            return this.User.GetAction("Jump");
        }
        else
            return null;
    }

    private ICharacterAction DetermineActionFromAI()
    {
        Vector3 diff = this.target.Transform.position - this.User.Transform.position;
        diff.y = 0;
        float rnd = UnityEngine.Random.value;
        if (User.Zones["farZone"].IsIn(target.GetCoord()))
        {
            if (User.Zones["closeZone"].IsIn(target.GetCoord()))
            {
                if (User.Zones["underZone"].IsIn(target.GetCoord()))
                {
                    if (rnd < 0.8)
                    {
                        return this.User.GetAction("JumpAttack");
                    }
                    else
                    {
                        return this.User.GetAction("Jump");
                    }

                }
                if (User.Zones["frontZone"].IsIn(target.GetCoord()))
                {
                    if (rnd < 0.45)
                    {
                        return this.User.GetAction("Bite");
                    }
                    else
                    {
                        return this.User.GetAction("Headbutt");
                    }

                }
                if (User.Zones["topZone"].IsIn(target.GetCoord()))
                {
                    if (rnd < 0.8)
                    {
                        return this.User.GetAction("JumpAttack");
                    }
                    else
                    {
                        return this.User.GetAction("Jump");
                    }
                }
                if (User.Zones["bottomZone"].IsIn(target.GetCoord()))
                {
                    if (rnd < 0.8)
                    {
                        return this.User.GetAction("JumpAttack");
                    }
                    else
                    {
                        return this.User.GetAction("Jump");
                    }
                }
                if (User.Zones["backZone"].IsIn(target.GetCoord()))
                {
                    return this.User.GetAction("TailSwipe");
                    if (rnd < 0.5)
                    {
                        return this.User.GetAction("TailSwipe");
                    }
                    else if (rnd < 0.75)
                    {
                        return this.User.GetAction("JumpAttack");
                    }
                    else
                    {
                        return this.User.GetAction("Jump");
                    }
                }


            }
            else
            {
                return this.User.GetAction("Walk");
            }
        }
        return this.User.GetAction("Idle");
    }

    protected override void Update()
    {
        base.Update();
        PrintDebugCanvas();
    }

    private void PrintDebugCanvas()
    {
        Znko.Root.Angle angle = new Znko.Root.Angle(target.GetCoord(), User.GetCoord());
        if (User.GetHorizontalDirection() == Character.HorizontalDirection.East)
            angle = angle * -1;
        float distance = Znko.Root.Coord.Distance(target.GetCoord(), User.GetCoord());

        string mytext = " ";
        foreach(KeyValuePair<string, Zone> entry in User.Zones)
        {
            if (entry.Value.IsIn(target.GetCoord()))
            {
                mytext = mytext + "," + entry.Key;
            }
        }
        mytext = mytext + " Current: " + this.User.GetActionName(CurrentAction);
        mytext = mytext + ", Next: " + this.User.GetActionName(NextAction);

        GameObject.Find("SparkyText").GetComponent<Text>().text = angle + "° " + distance + mytext;
   }

    public void FlipForDebug()
    {
        this.clickedButton = true;
    }

    public override void ActionFinished()
    {
        base.ActionFinished();
        Vector3 diff = this.target.Transform.position - this.User.Transform.position;
        diff.y = 0;
        if ((CurrentAction is MoveWithSpeed || NextAction is MoveWithSpeed))
            this.User.Direction = diff;
    }
}

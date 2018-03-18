using Znko.Actions;
using UnityEngine;
using Znko.Characters;
using UnityEngine.UI;
using System.Collections.Generic;
using Znko.AI;

public class SparkyController : ActionBasedController {
    private const int followDistance = 15;
    private const int attackDistance = 10;
    private Character target;

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

    private ICharacterAction DetermineActionFromAI()
    {
        Vector3 diff = this.target.Transform.position - this.User.Transform.position;
        diff.y = 0;
        if (diff.magnitude < followDistance)
        {
            //this.User.Direction = diff;
            if (diff.magnitude < attackDistance)
            {
                if (User.Zones["underZone"].IsIn(target.GetCoord()))
                    return this.User.GetAction("Idle");
                if (User.Zones["frontZone"].IsIn(target.GetCoord()))
                {
                    return this.User.GetAction("Bite");
                } else if (User.Zones["backZone"].IsIn(target.GetCoord()))
                {
                    return this.User.GetAction("TailSwipe");
                }

            }
            return this.User.GetAction("Walk");
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
                mytext = mytext + entry.Key;
            }
        }
        GameObject.Find("SparkyText").GetComponent<Text>().text = angle + "° " + distance + mytext;
   }

    public void FlipForDebug()
    {
        if (this.User.GetHorizontalDirection () == Character.HorizontalDirection.East)
            this.User.SetHorizontalDirectionDebug(Character.HorizontalDirection.West);
        else
            this.User.SetHorizontalDirectionDebug(Character.HorizontalDirection.East);
    }

    public override void ActionFinished()
    {
        base.ActionFinished();
        Vector3 diff = this.target.Transform.position - this.User.Transform.position;
        diff.y = 0;
        //this.User.Direction = diff;
    }
}

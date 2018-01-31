using Znko.Actions;
using UnityEngine;

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
        this.attacks.Add (this.User.GetAction ("Bite"));
        //this.attacks.Add (this.User.GetAction ("TailSwipe"));
        //this.attacks.Add (this.User.GetAction ("Headbutt"));
        //this.attacks.Add (this.User.GetAction ("RushHeadbutt"));
        //this.attacks.Add (this.User.GetAction ("JumpAttack"));
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
            this.User.Direction = diff;
            if (diff.magnitude < attackDistance)
            {
                return this.attacks[rnd.Next(attacks.Count)];
            }
            return this.User.GetAction("Walk");
        }
        return this.User.GetAction("Idle");
    }
}

using Actions;
using UnityEngine;

public class SparkyController : ActionBasedController {
    private const int followDistance = 15;
    private const int attackDistance = 3;
    private Character target;

    private bool gotHit;

    static System.Random rnd = new System.Random ();
    private System.Collections.Generic.List<ICharacterAction> attacks = new System.Collections.Generic.List<ICharacterAction> ();

    protected override void Awake () {
        this.User = Characters.Sparky.Create (transform);
    }

    // Use this for initialization
    protected override void Start () {
        base.Start ();
        GetComponentInChildren<WeaponBehaviour> ().User = this.User;
        this.target = Characters.Player.Instance ();
        this.attacks.Add (this.User.GetAction ("Bite"));
        this.attacks.Add (this.User.GetAction ("TailSwipe"));
        this.attacks.Add (this.User.GetAction ("Headbutt"));
    }


    protected override ICharacterAction DetermineAction () {
        Vector3 diff = this.target.Transform.position - this.User.Transform.position;
        diff.y = 0;
        //Debug.Log (diff);
        if (diff.magnitude < followDistance) {
            this.User.Direction = diff;
            if (diff.magnitude < attackDistance) {
                return this.attacks[rnd.Next (attacks.Count)];
            }
            return this.User.GetAction ("Walk");
        }
        return this.User.GetAction ("Idle");
    }


    public override void ReceiveHit()
    {
        this.User.GetResource(CharacterResource.Type.Health).Decrease(10);
        if (this.User.GetResource(CharacterResource.Type.Health).Percentage <= 0)
        {
            Destroy(this.transform.gameObject);
        }
        gotHit = true;
    }

}

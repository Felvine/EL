using Actions;
using UnityEngine;

public class SparkyController : ActionBasedController {
    private const int followDistance = 7;
    private Character target;

    private bool gotHit;

    protected override void Awake () {
        this.User = Characters.Sparky.Create (transform);
    }

    // Use this for initialization
    protected override void Start () {
        base.Start ();
        this.target = Characters.Player.Instance ();
    }


    protected override ICharacterAction DetermineAction () {
        Vector3 diff = this.target.Transform.position - this.User.Transform.position;
        diff.y = 0;
        //Debug.Log (diff);
        if (diff.magnitude > followDistance) {
            this.User.Direction = diff;
            return this.User.GetAction ("Walk");
        }
        return this.User.GetAction ("Idle");
    }


    public override void ReceiveHit () {
        //this.User.GetResource (CharacterResource.Type.Health).Decrease (10);
        //if (this.User.GetResource (CharacterResource.Type.Health).Percentage <= 0) {
        //    Destroy (this.transform.gameObject);
        //}
        //gotHit = true;
    }

}

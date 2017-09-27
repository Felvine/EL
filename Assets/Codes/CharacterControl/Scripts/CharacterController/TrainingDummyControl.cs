using Actions;
using UnityEngine;

public class TrainingDummyControl : ActionBasedController {

    private bool gotHit;

    protected override void Awake () {
        this.User = Characters.TrainingDummy.Create (transform);
    }   

    // Use this for initialization
    protected override void Start () {
        base.Start ();
    }


    protected override ICharacterAction DetermineAction () {
        if (gotHit) {
            gotHit = false;
            return this.User.GetAction ("ReceiveHit");
        }
        return null;
    }


    public override void ReceiveHit () {
        this.User.GetResource (CharacterResource.Type.Health).Decrease (10);
        if (this.User.GetResource(CharacterResource.Type.Health).Percentage <= 0) {
            Destroy (this.transform.gameObject);
        }
        gotHit = true;
    }

}

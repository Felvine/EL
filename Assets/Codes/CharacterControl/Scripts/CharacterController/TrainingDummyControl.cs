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
        return ProcessEventQueue();
    }
}

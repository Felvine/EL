﻿using Actions;
using UnityEngine;

public class TrainingDummyControl : ActionBasedController {

    private bool gotHit;
    
    public override void ReceiveHit () {
        this.User.Properties.Health.Decrease (10);
        if (this.User.Properties.Health.Percentage <= 0) {
            Destroy (this.transform.gameObject);
        }
        gotHit = true;
    }

    // Use this for initialization
    protected override void Start () {
        base.Start ();
        this.User.AddAction ("ReceiveHit", new Idle (this.User, 1f, this.User.Animaton.GetClip ("Dummy_Hit")));
    }

    protected override ICharacterAction DetermineAction () {
        if (gotHit) {
            gotHit = false;
            Debug.Log (this.User.GetAction ("ReceiveHit"));
            return this.User.GetAction ("ReceiveHit");
        }
        return null;
    }
}

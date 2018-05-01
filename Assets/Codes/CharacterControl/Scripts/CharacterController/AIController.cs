using Znko.Actions;
using UnityEngine;
using Znko.Characters;
using UnityEngine.UI;
using System.Collections.Generic;
using Znko.AI;
using System;

public abstract class AIController : ActionBasedController
{
    protected Character target;

    public Character Target {
        get {
            return target;
        }
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }


    protected abstract override ICharacterAction DetermineAction();


    protected override void Update()
    {
        base.Update();
    }

    public override void ActionFinished()
    {
        base.ActionFinished();
    }
}

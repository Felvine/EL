﻿using Znko.Events;
using UnityEngine;

public abstract class ICharacterController : MonoBehaviour{
    public abstract void AddEvent(ICharacterEvent eventIn);
    public abstract Character GetUser ();
}

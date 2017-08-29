using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummyControl : CharacterBehaviour {

    private Character user;
    private Transform healthBarObject;
    private SpriteRenderer healthBar;

    public override void ReceiveHit () {
        this.user.Properties.Health.Decrease (10);
        if (this.user.Properties.Health.Percentage <= 0) {
            Destroy (this.transform.parent.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        this.user = new Character (transform);
        foreach (Transform child in transform) {
            if (child.CompareTag ("HealthBar")) {
                healthBarObject = child;
                healthBar = child.GetComponent<SpriteRenderer> ();
            }
        }
        this.healthBar.color = user.Properties.Health.Color;

    }
	
	// Update is called once per frame
	void Update () {
        UpdateResourceBars ();
	}

    private void UpdateResourceBars () {
        this.healthBarObject.localScale = new Vector3 (user.Properties.Health.Percentage*10, 1, 1);
    }
}

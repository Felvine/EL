using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class HPBar : MonoBehaviour {

    private Character user;
    private Transform healthBarObject;
    private SpriteRenderer healthBar;

    // Use this for initialization
    protected virtual void Start () {
        this.user = GetComponent<ActionBasedController> ().GetUser ();
        if (this.user == null)
            throw new MissingComponentException ("Need user");
        foreach (Transform child in transform) {
            if (child.CompareTag ("HealthBar")) {
                healthBarObject = child;
                healthBar = child.GetComponent<SpriteRenderer> ();
            }
        }
        this.healthBar.color = this.user.Properties.Health.Color;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        UpdateResourceBars ();
    }

    private void UpdateResourceBars () {
        this.healthBarObject.localScale = new Vector3 (this.user.Properties.Health.Percentage * 10, 1, 1);
    }
}

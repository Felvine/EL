using UnityEngine;
using Znko.Characters;

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
        this.healthBar.color = this.user.GetResource (CharacterResource.Type.Health).Color;
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        UpdateResourceBars ();
    }

    private void UpdateResourceBars () {
        this.healthBarObject.localScale = new Vector3 (this.user.GetResource (CharacterResource.Type.Health).Percentage * GetMaxScale(), 1, 1);
    }

    private int GetMaxScale()
    {
        switch (this.user.Race)
        {
            case Character.Races.Humanoid:
                return 10;
            case Character.Races.Giant:
                return 70;
        }
        return 10;
    }
}

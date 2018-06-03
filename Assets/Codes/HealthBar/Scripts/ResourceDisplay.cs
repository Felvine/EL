using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Znko.Characters;
[RequireComponent(typeof(TextMesh))]
public class ResourceDisplay : MonoBehaviour {
    private Character user;
    private TextMesh textMesh;
    // Use this for initialization
    void Start () {
        this.user = GetComponentInParent<ActionBasedController>().GetUser();
        if (this.user == null)
            throw new MissingComponentException("Need user");
        this.user.CharacterFlippedEvent += (object sender, Character.HorizontalDirection direction) =>
        {
            switch (direction)
            {
                case Character.HorizontalDirection.East:
                    transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    break;
                case Character.HorizontalDirection.West:
                    transform.localScale = new Vector3(-0.1f, 0.1f, 0.1f);
                    break;
            }
        };
        textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.fontSize = 100;
        textMesh.richText = true;
    }
	// Update is called once per frame
	void Update () {
        string text = "";
        foreach (KeyValuePair<CharacterResource.Type, CharacterResource> cr in this.user.GetResources())
        {
            text += "<color=#" + ((int)(255 *cr.Value.Color.r)).ToString("X2") +
                                ((int)(255 * cr.Value.Color.g)).ToString("X2") +
                                ((int)(255 * cr.Value.Color.b)).ToString("X2") + ">" + cr.Key.ToString() + ": " + cr.Value.Percentage*100 + "%</color>" + "\n";
        }
        textMesh.text = text;

    }
}

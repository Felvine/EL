using UnityEngine;
using UnityEngine.UI;
using Znko.Actions;
using Znko.Events;

class GammaController : ActionBasedController {

    protected override void Awake () {
        Characters.Player.Create (transform);
        this.User = Characters.Player.Instance ();
    }
    protected override void Start () {
        base.Start ();
        WeaponBehaviour wb = GetComponentInChildren<WeaponBehaviour>();
        wb.User = this.User;
    }

    protected override ICharacterAction DetermineAction () {
        ICharacterAction eventAction = ProcessEventQueue();
        ICharacterAction inputAction = DetermineActionFromInputs();
        if (eventAction == null)
            return inputAction;
        else if (inputAction == null)
            return eventAction;
        if (eventAction.Priority > inputAction.Priority)
            return eventAction;
        else
            return inputAction;
    }


    private ICharacterAction DetermineActionFromInputs () {
        Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        if (CurrentAction == null || CurrentAction is MoveWithSpeed)
            this.User.Direction = moveDirection;
        if (Input.GetButton("Fire1"))
            return this.User.GetAction("ComboAttack");
        else if (Input.GetButton("Fire2"))
            return this.User.GetAction("Attack4");
        if (moveDirection != Vector3.zero) {
            if (Input.GetKeyDown (KeyCode.Space))
                return this.User.GetAction ("Roll");
            else if (Input.GetKey (KeyCode.LeftShift))
                return this.User.GetAction ("Run");
            else
                return this.User.GetAction ("Walk");
        }
        return this.User.GetAction ("Idle");
    }

    protected override void BranchComboAttacks () {
        CharacterActionSequence currentActionSequence = (CurrentAction as CharacterActionSequence);
        if (currentActionSequence != null) {
            if (currentActionSequence.Actions[currentActionSequence.Step].IsFinishing () && (currentActionSequence.Step + 1) < currentActionSequence.Actions.Count) {
                ComboAction nextComboAction = (currentActionSequence.Actions[currentActionSequence.Step + 1] as ComboAction);
                if (nextComboAction != null) {
                    if (Input.GetButton("Fire1"))
                        nextComboAction.Selected = 1;
                    else if (Input.GetButton("Fire2"))
                        nextComboAction.Selected = 2;
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        PrintDebugCanvas();
    }

    private void PrintDebugCanvas()
    {
        GameObject.Find("Text").GetComponent<Text>().text = "FPS: " + (1 / Time.deltaTime) +  "\nUser Action Queue: \n" + CurrentAction +"\n" + NextAction;
    }

}


using UnityEngine;
using Znko.Events;
using Znko.Characters;
using Znko.Root;

namespace Znko.Actions
{
    class FleeToDistance : CharacterAction
    {
        private Vector3 moveDirection;
        private float distance;
        public FleeToDistance(Character characterIn, float durationIn, AnimationClip animationIn, float distanceIn, ResourceCost cost = null, params ActionEvent[] events) : base(characterIn, durationIn, animationIn, cost, events)
        {
            this.distance = distanceIn;
            this._priority = 1;
        }

        public override void PreActions(ICharacterAction previousAction, ICharacterController controller)
        {
            this.moveDirection = new Vector3(Random.value*2-1,0, Random.value*2-1).normalized;
            base.PreActions(previousAction, controller);
        }

        public override void PostActions(ICharacterAction nextAction, ICharacterController controller)
        {
            base.PostActions(nextAction, controller);
        }

        protected override void PerformAction()
        {
            float speed = 4 * distance / this.GetDuration() * (Time.time - this.StartTime);
            //Vector3 moveDirection = this.User.Transform.TransformDirection(this.User.Direction);
            this.User.Controller.Move(moveDirection * speed * Time.deltaTime);
        }
    }
}
using UnityEngine;
using Znko.Events;
using Znko.Characters;
using Znko.Root;

namespace Znko.Actions
{
    class MoveToTarget : CharacterAction
    {
        private Coord targetLocation;
        private Coord userLocation;
        private Coord offSet;
        public MoveToTarget(Character characterIn, float durationIn, AnimationClip animationIn, Coord offset, ResourceCost cost = null, params ActionEvent[] events) : base(characterIn, durationIn, animationIn, cost, events)
        {
            this._priority = 1;
            this.offSet = offset;
        }

        public override void PreActions(ICharacterAction previousAction, ICharacterController controller)
        {
            targetLocation = ((AIController)(controller)).Target.GetCoord() + new Coord (this.offSet.X * -this.User.GetDirectionSign(), this.offSet.Y);
            userLocation = this.User.GetCoord();
            base.PreActions(previousAction, controller);
        }

        public override void PostActions(ICharacterAction nextAction, ICharacterController controller)
        {
            base.PostActions(nextAction, controller);
        }

        protected override void PerformAction()
        {
            float distance = Coord.Distance(targetLocation, userLocation);
            Vector3 moveDirection = ((Vector3)(targetLocation - userLocation)).normalized;
            float speed = 4 * distance / this.GetDuration() * (Time.time - this.StartTime);
            //Vector3 moveDirection = this.User.Transform.TransformDirection(this.User.Direction);
            moveDirection *= speed;
            this.User.Controller.Move(moveDirection * Time.deltaTime);
        }
    }
}
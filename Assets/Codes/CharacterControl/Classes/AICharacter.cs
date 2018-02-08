using System.Collections.Generic;
using UnityEngine;
using Znko.AI;

namespace Znko.Characters
{
    public class AICharacter : Character
    {
        private List<Zone> zones;
        public AICharacter(Transform characterTransform, List<Zone> zonesIn) : base (characterTransform)
        {
            this.zones = zonesIn;
        }


        public AICharacter(Transform characterTransform, Animation animationIn, CharacterController controllerIn, List<Zone> zonesIn) : base (characterTransform, animationIn, controllerIn)
        {
            this.zones = zonesIn;
        }
    }
}

//using System.Collections.Generic;

//namespace Actions {
//    class CharacterActionSequence : ICharacterAction {
//        private List<ICharacterAction> actions;
//        private int step = 0;

//        public CharacterActionSequence(params ICharacterAction[] actionsIn) {
//            this.actions = new List<ICharacterAction> ();
//            step = 0;
//            for (int i = 0; i< actionsIn.Length; i++) {
//                this.actions.Add (actionsIn[i]);
//            }
//        }
//        public virtual void PreActions (ICharacterAction previousAction) {

//        }

//        public virtual void PostActions (ICharacterAction nextAction) {
//        }
//        public Phase Execute (ICharacterAction previousAction, ICharacterAction nextAction) {
//            if (step == (actions.Count-1)) {
//                if (actions[step].Execute (previousAction,nextAction) == Phase.NotActing) {
//                    step = 0;
//                    return Phase.NotActing;
//                } else {
//                    return Phase.Acting;
//                }
//            } else {
//                if (actions[step].Execute (previousAction,nextAction) == Phase.NotActing)
//                    step++;
//                return Phase.Acting;
//            }
//        }
//        public virtual bool IsPrimitive () {
//            return false;
//        }
//        public bool AlmostDone () {
//            return false;
//        }
//    }
//}

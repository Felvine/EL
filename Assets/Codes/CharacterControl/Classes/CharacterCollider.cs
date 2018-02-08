namespace Znko.Characters
{
    public class CharacterCollider
    {
        private string tagType;

        private bool isAttacking;

        public bool IsAttacking {
            get {
                return isAttacking;
            }

            set {
                isAttacking = value;
            }
        }

        public CharacterCollider(string typeIn)
        {
            this.tagType = typeIn;
        }

        public bool IsType(string typeIn)
        {
            return tagType == typeIn;
        }
    }
}

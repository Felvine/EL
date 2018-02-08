using Znko.Characters;

namespace Znko.Events
{
    public interface ICharacterEvent
    {
        void Do();
        void SetUser(Character userIn);
    }
}


namespace AsciiUtil
{
    public interface IStateTriggerable
    {
        void Initialize(AsciiStateMachine stateMachine);
        bool Verify(AsciiStateMachine stateMachine);
    }
}

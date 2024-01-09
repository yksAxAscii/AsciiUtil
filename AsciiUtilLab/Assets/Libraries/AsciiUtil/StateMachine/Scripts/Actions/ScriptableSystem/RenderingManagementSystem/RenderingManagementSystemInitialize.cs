using AsciiUtil.StateMachine;
using AsciiUtil;

[System.Serializable,AddTypeMenu("ScriptableSystem/RenderingManagementSystem/RenderingManagementSystemInitialize")]
public class RenderingManagementSystemInitialize : IStateActionable
{
    public void Action(AsciiStateMachine stateMachine)
    {
        ScriptableSystemProvider.Instance.GetSystem<RenderingManagementSystem>().Initialize();
    }
}

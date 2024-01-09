using AsciiUtil.StateMachine;
using AsciiUtil;

[System.Serializable, AddTypeMenu("ScriptableSystem/RenderingManagementSystem/StartOnDemandRendering")]
public class StartOnDemandRendering : IStateActionable
{
    public void Action(AsciiStateMachine stateMachine)
    {
        ScriptableSystemProvider.Instance.GetSystem<RenderingManagementSystem>().StartOnDemandRendering();
    }
}

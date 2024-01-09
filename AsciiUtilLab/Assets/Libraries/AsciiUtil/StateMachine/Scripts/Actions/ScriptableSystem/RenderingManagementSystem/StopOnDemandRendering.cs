using AsciiUtil.StateMachine;
using AsciiUtil;

[System.Serializable, AddTypeMenu("ScriptableSystem/RenderingManagementSystem/StopOnDemandRendering")]
public class StopOnDemandRendering : IStateActionable
{
    public void Action(AsciiStateMachine stateMachine)
    {
        ScriptableSystemProvider.Instance.GetSystem<RenderingManagementSystem>().StopOnDemandRendering();
    }
}

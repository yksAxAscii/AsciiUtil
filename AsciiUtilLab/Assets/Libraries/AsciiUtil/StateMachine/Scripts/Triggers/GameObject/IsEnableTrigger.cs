using UnityEngine;
using AsciiUtil;
[System.Serializable,AddTypeMenu("GameObject/IsEnableTrigger")]
public class IsEnableTrigger : IStateTriggerable
{
    [SerializeField, Header("子オブジェクトを検知したい時はチェック")]
    private bool isCheckChild;
    [SerializeField]
    private string childName;
    [SerializeField, Header("Disableを検知したい時はチェック")]
    private bool isDisable;
    [System.NonSerialized]
    private GameObject targetObject;
    public void Initialize(AsciiStateMachine stateMachine)
    {
        if (isCheckChild)
        {
            targetObject = stateMachine.transform.Find(childName).gameObject;
            return;
        }
        targetObject = stateMachine.gameObject;
    }

    public bool Verify(AsciiStateMachine stateMachine)
    {
        if (isDisable)
        {
            return !targetObject.activeSelf;
        }
        return targetObject.activeSelf;
    }


}

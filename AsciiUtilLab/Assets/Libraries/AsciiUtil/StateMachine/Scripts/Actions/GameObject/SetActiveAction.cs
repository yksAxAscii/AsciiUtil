using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable, AddTypeMenu("GameObject/SetActive")]
    public class SetActiveAction : IStateActionable
    {
        [SerializeField, Header("Disableに移行したい時はチェック")]
        private bool toDisable;
        [SerializeField, Header("子オブジェクトを変更したい時はチェック")]
        private bool isChangeChild;
        [SerializeField]
        private string targetChildName;
        [System.NonSerialized]
        private GameObject targetObject;

        public void Action(AsciiStateMachine stateMachine)
        {
            if (targetObject == null)
            {
                //対象のゲームオブジェクトを取得
                if (isChangeChild)
                {
                    targetObject = stateMachine.transform.Find(targetChildName).gameObject;
                }
                else
                {
                    targetObject = stateMachine.gameObject;
                }
            }

            if (toDisable)
            {
                targetObject.SetActive(false);
            }
            else
            {
                targetObject.SetActive(true);
            }
        }
    }
}

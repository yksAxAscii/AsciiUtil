using UnityEngine;
namespace AsciiUtil.StateMachine
{
    [System.Serializable, AddTypeMenu("Util/Material/SetColor")]
    public class MaterialSetColor : IStateActionable
    {
        [SerializeField]
        private string paramName;
        private Material material;

        public void Action(AsciiStateMachine stateMachine)
        {
            material = stateMachine.GetComponent<Renderer>().material;
            material.color = new Color(Random.value,Random.value,Random.value);
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsParticleFinished : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onFinished;

    private void OnParticleSystemStopped()
    {
        onFinished.Invoke();
    }

}

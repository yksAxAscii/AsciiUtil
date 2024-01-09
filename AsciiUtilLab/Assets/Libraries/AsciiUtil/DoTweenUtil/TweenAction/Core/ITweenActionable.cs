using UnityEngine;
using DG.Tweening;

namespace AsciiUtil
{
    public interface ITweenActionable
    {
        Tween Play(Transform transform);
    }
}


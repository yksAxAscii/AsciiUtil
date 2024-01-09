using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CanvasAspectAdjuster : MonoBehaviour
{
    private CanvasScaler canvasScalerCache;

    public void SetAspectAdjustParameter(Vector2 targetResolution)
    {
        canvasScalerCache = GetComponent<CanvasScaler>();
        canvasScalerCache.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScalerCache.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        canvasScalerCache.referenceResolution = targetResolution;
    }
}
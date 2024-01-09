using UnityEngine;

namespace AsciiUtil
{
    /// <summary>
    /// レンダリングの設定を管理するシステム
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableSystem/RenderingManagementSystem", fileName = "RenderingManagementSystem")]
    public class RenderingManagementSystem : ScriptableObject, IScriptableSystemable
    {
        //FPSの最大値の定数
        private const int MAX_FPS = 240;
        [SerializeField,Range(-1,MAX_FPS), Header("FPSを固定する場合の値 -1にすると固定されない")]
        private int fixedFPS;
        [SerializeField, Header("OnDemandRenderingのフレーム間隔")]
        private int renderFrameInterval;
        private const int DEFAULT_RENDER_FRAME_INTERVAL = 1;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            if (fixedFPS < 0) return;
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = fixedFPS;
        }

        /// <summary>
        /// OnDemandRenderingを開始
        /// </summary>
        public void StartOnDemandRendering()
        {
            OnDemandRendering(renderFrameInterval);
        }

        /// <summary>
        /// OnDemandRenderingを終了
        /// </summary>
        public void StopOnDemandRendering()
        {
            OnDemandRendering(DEFAULT_RENDER_FRAME_INTERVAL);
        }

        /// <summary>
        /// OnDemandRendering処理
        /// </summary>
        /// <param name="renderFrameInterval"></param>
        private void OnDemandRendering(int renderFrameInterval)
        {
            UnityEngine.Rendering.OnDemandRendering.renderFrameInterval = renderFrameInterval;
        }
    }
}
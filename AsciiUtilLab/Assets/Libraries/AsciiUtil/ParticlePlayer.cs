using UnityEngine;

namespace AsciiUtil
{
    public class ParticlePlayer : MonoBehaviour
    {
        [SerializeField]
        private AsciiParticleData[] particleData;
        private AsciiParticlePooler particlePooler;

        public void Play(string key, Vector3 position)
        {

            if (particlePooler == null)
            {
                particlePooler = FindAnyObjectByType<AsciiParticlePooler>();
            }
            //指定されたキーのパーティクルデータを探してあれば再生
            var targetParticleData = FindParticleData(key);
            if (targetParticleData == null) return;
            particlePooler.Play(targetParticleData, position);
        }

        private AsciiParticleData FindParticleData(string key)
        {
            foreach (var data in particleData)
            {
                if (data.ParticleKey == key)
                {
                    return data;
                }
            }
            AsciiDebug.LogError("指定されたキーのパーティクルデータが見つからないよ", "Key:", key);
            return null;
        }
    }
}

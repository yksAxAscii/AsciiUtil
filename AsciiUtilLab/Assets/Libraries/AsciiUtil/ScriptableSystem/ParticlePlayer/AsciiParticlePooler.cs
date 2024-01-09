using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil
{
    public class AsciiParticlePooler : MonoBehaviour
    {
        private Dictionary<string, List<ParticleSystem>> particles = new Dictionary<string, List<ParticleSystem>>();
        [SerializeField]
        private List<ParticleSystem> currentParticleList;
        private ParticleSystem currentPlayParticleObj;
        public void Play(AsciiParticleData targetParticleData, Vector3 position)
        {
            currentPlayParticleObj = null;
            currentParticleList = null;
            //targetParticleDataのキーに対応するパーティクルリストを取得
            if (particles.ContainsKey(targetParticleData.ParticleKey))
            {
                currentParticleList = particles[targetParticleData.ParticleKey];
            }
            //無ければ生成して登録
            else
            {
                currentParticleList = new List<ParticleSystem>();
                particles.Add(targetParticleData.ParticleKey, currentParticleList);
                //パーティクル初回生成
                for(int i = 0; i< targetParticleData.InitInstanceNum; i++)
                {
                    currentPlayParticleObj = Instantiate(targetParticleData.ParticleObj,transform);
                    currentPlayParticleObj.gameObject.SetActive(false);
                    currentParticleList.Add(currentPlayParticleObj);
                }
                currentPlayParticleObj = null;
            }

            //非アクティブのパーティクルを探して取得
            foreach (var particle in currentParticleList)
            {
                if (!particle.gameObject.activeSelf)
                {
                    currentPlayParticleObj = particle;
                }
            }
            //見つからなければ新たに生成してパーティクルリストに登録
            if (currentPlayParticleObj is null)
            {
                currentPlayParticleObj = Instantiate(targetParticleData.ParticleObj, position, Quaternion.identity);
                currentParticleList.Add(currentPlayParticleObj);
                currentPlayParticleObj.transform.SetParent(transform);
            }

            currentPlayParticleObj.gameObject.SetActive(true);
            currentPlayParticleObj.transform.position = position;
        }
    }
}

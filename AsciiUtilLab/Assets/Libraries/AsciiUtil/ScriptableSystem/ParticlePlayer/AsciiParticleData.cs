using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil
{
    [System.Serializable,CreateAssetMenu(menuName = "ParticleData")]
    public class AsciiParticleData : ScriptableObject
    {
        [SerializeField]
        private string particleKey;
        public string ParticleKey => particleKey;
        [SerializeField]
        private ParticleSystem particleObj;
        public ParticleSystem ParticleObj => particleObj;
        [SerializeField,Range(1,100),Header("初回生成時にまとめて生成する数 たくさん使いそうなエフェクトはこれ多めにしとくといいと思う")]
        private int initInstanceNum;
        public int InitInstanceNum => initInstanceNum;
    }
}

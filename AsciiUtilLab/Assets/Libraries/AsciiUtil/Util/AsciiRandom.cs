using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsciiUtil
{
    public class AsciiRandom<T>
    {
        private Dictionary<T, uint> contentWeightDic = new Dictionary<T, uint>();

        public void AddContent(T content, uint weight)
        {
            contentWeightDic.Add(content, weight);
        }

        public void ClearContents()
        {
            contentWeightDic.Clear();
        }

        public T ChooseRandomContent()
        {
            // 重みの合計を計算
            uint totalWeight = 0;
            foreach (var contentWeight in contentWeightDic)
            {
                totalWeight += contentWeight.Value;
            }

            uint randomValue = (uint)Random.Range(0, totalWeight);
            uint accumulatedWeight = 0;
            foreach (var contentWeight in contentWeightDic)
            {
                accumulatedWeight += contentWeight.Value;
                if (accumulatedWeight > randomValue)
                {
                    return contentWeight.Key;
                }
            }
            return default;
        }
    }
}

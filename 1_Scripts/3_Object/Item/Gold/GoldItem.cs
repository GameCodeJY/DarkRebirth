/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 24.
 *  Information :   Gold Item
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class GoldItem : MonoBehaviour // Data Field
    {
        #region Const Value
        /// <summary> 최소 골드량 </summary>
        private const int MIN_GOLD = 80;
        /// <summary> 최대 골드량 </summary>
        private const int MAX_GOLD = 120;
        #endregion
    }

    /// <summary>
    /// Life Cycle
    /// </summary>
    public partial class GoldItem : MonoBehaviour // Life Cycle
    {
        /// <summary>
        /// Consume Gold Item
        /// </summary>
        public void ConsumeGoldItem()
        {
            MainSystem.Instance.PlayerManager.GetGold(Random.Range(MIN_GOLD, MAX_GOLD + 1)); // +1 한 이유 : Random.Range의 2번째 인자는 범위에 포함되지 않기 때문에 +1하여 범위에 포함시킴

            gameObject.SetActive(false);
        }
    }
}
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
        /// <summary> �ּ� ��差 </summary>
        private const int MIN_GOLD = 80;
        /// <summary> �ִ� ��差 </summary>
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
            MainSystem.Instance.PlayerManager.GetGold(Random.Range(MIN_GOLD, MAX_GOLD + 1)); // +1 �� ���� : Random.Range�� 2��° ���ڴ� ������ ���Ե��� �ʱ� ������ +1�Ͽ� ������ ���Խ�Ŵ

            gameObject.SetActive(false);
        }
    }
}
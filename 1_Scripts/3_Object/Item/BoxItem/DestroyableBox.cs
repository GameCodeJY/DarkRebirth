/*
 *  Coder       :   JY
 *  Last Update :   2025. 01. 17.
 *  Information :   Destroyable Box
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class DestroyableBox : BaseMonster // Data Field
    {
        #region Serialize Field
        /// <summary> Box Origin GameObject </summary>
        [SerializeField] private GameObject gameObjectBoxOrigin = default;
        /// <summary> Broken Box GameObject List </summary>
        [SerializeField] private List<GameObject> listGameObjectBoxBroken = default;
        /// <summary> Item GameObject List </summary>
        [SerializeField] private List<GameObject> listGameObjectItem = default;
        #endregion
    }

    /// <summary>
    /// Crash
    /// </summary>
    public partial class DestroyableBox : BaseMonster // Crash
    {
        /// <summary>
        /// Crash Box Item
        /// </summary>
        public void CrashBoxItem()
        {
            gameObjectBoxOrigin.SetActive(false);
            for (int i = 0; i < listGameObjectBoxBroken.Count; i++)
            {
                listGameObjectBoxBroken[i].SetActive(true);
            }
            listGameObjectItem[Random.Range(0, listGameObjectItem.Count)].SetActive(true);
        }
    }
}
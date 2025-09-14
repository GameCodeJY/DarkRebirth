/*
 *  Coder       :   JY
 *  Last Update :   2025. 01. 22.
 *  Information :   ∫“±Ê«— ±‚µ’
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class OminousPillar : MonoBehaviour // Data Field
    {
        #region Serialize Field
        /// <summary> Ominous Pillar GameObject </summary>
        [SerializeField] private GameObject gameObjectOminousPillar = default;
        /// <summary> Ominous Pillar Boom Effect GameObject </summary>
        [SerializeField] private GameObject gameObjectOminousPillarBoomEffect = default;
        #endregion
    }

    /// <summary>
    /// Crash
    /// </summary>
    public partial class OminousPillar : MonoBehaviour // Crash
    {
        /// <summary>
        /// Crash Ominouus Pillar
        /// </summary>
        public void CrashOminousPillar()
        {
            gameObjectOminousPillar.SetActive(false);
            gameObjectOminousPillarBoomEffect.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
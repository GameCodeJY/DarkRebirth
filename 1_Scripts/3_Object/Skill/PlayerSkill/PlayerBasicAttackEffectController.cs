/*
 *  Coder       :   JY
 *  Last Update :   2025. 02. 14.
 *  Information :   Player Basic Attack Effect Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class PlayerBasicAttackEffectController : MonoBehaviour // Data Field
    {
        #region Member Value
        /// <summary> Basic Attack Effect GameObject List </summary>
        [SerializeField] private List<GameObject> ListGameObjectBasicAttackEffect = default;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class PlayerBasicAttackEffectController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
        }

        public void Initialize()
        {
            Allocate();
        }
    }

    /// <summary>
    /// Select
    /// </summary>
    public partial class PlayerBasicAttackEffectController : MonoBehaviour // Select
    {
        /// <summary>
        /// Select Basic Attack Effect
        /// </summary>
        public void SelectBasicAttackEffect(int numberBasicAttackEffect)
        {
            ListGameObjectBasicAttackEffect[numberBasicAttackEffect].SetActive(true);
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 03. 19.
 *  Information :   HP Bar
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class LookAtCamera : MonoBehaviour // Data Field
    {
        #region Member Value
        [Header("Transform")]
        /// <summary> HP UI Transform </summary>
        public Transform TransformUI = default;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class LookAtCamera : MonoBehaviour // Initialize
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
    /// Main
    /// </summary>
    public partial class LookAtCamera : MonoBehaviour // Main
    {
        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            TransformUI.forward = Camera.main.transform.forward;
        }
    }
}
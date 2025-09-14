/*
 *  Coder       :   JY
 *  Last Update :   2024. 10. 15.
 *  Information :   UI Camera Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class UICameraController : MonoBehaviour // Data Field
    {
        #region Member Value
        /// <summary> UI Camera </summary>
        public Camera UICamera = default;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class UICameraController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
            MainSystem.Instance.CameraManager.SignUpUICameraController(this);
        }

        private void Initialize()
        {
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class UICameraController : MonoBehaviour // Main
    {
        private void Awake()
        {
            Allocate();
        }

        private void Start()
        {
            Initialize();
        }
    }
}
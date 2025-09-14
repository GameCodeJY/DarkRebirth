/*
 *  Coder       :   JY
 *  Last Update :   2025. 06. 26.
 *  Information :   Global UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class GlobalUIController : MonoBehaviour // Data Field
    {
        /// <summary> MapUIController </summary>
        [SerializeField] private MapUIController MapUIController = default;
    }

    /// <summary>
    /// Map
    /// </summary>
    public partial class GlobalUIController : MonoBehaviour // Map
    {
        /// <summary>
        /// Map UI È°¼ºÈ­
        /// </summary>
        public void ToggleMapUI(bool isActive)
        {
            MapUIController.gameObject.SetActive(isActive);
        }
    }
}
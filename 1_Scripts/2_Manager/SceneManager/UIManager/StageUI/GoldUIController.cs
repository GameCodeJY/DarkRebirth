/*
 *  Coder       :   JY
 *  Last Update :   2025. 07. 24.
 *  Information :   Gold UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class GoldUIController : MonoBehaviour // Data Field
    {
        /// <summary> Gold Text </summary>
        [SerializeField] private TextMeshProUGUI _textGold = default;

        #region Property
        public TextMeshProUGUI TextGold { get => _textGold; set => _textGold = value; }
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class GoldUIController : MonoBehaviour // Initialize
    {
        private void Allocate()
        {
        }

        public void Initialize()
        {
            Allocate();
            MainSystem.Instance.SceneManager.UIManager.UpdateGoldText();
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class GoldUIController : MonoBehaviour // Main
    {
        private void Start()
        {
            Initialize();
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 01. 17.
 *  Information :   Home UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class HomeUIController : BaseUIController // Data Field
    {
        #region Serialize Field
        /// <summary> 느낌표 이미지 GameObject </summary>
        public GameObject gameObjectQuestion = default;
        /// <summary> 스킬창 GameObject </summary>
        public GameObject gameObjectSkillWindow = default;
        /// <summary> 상인점 Popup GameObject </summary>
        public GameObject gameObjectMerchantShopPopup = default;
        /// <summary> E Key Message GameObject </summary>
        public GameObject gameObjectEKeyMessage = default;
        /// <summary> System Popup GameObject </summary>
        public GameObject gameObjectSystemPopup = default;
        /// <summary> Question Symbol Controller </summary>
        public QuestionSymbolController QuestionSymbolController = default;
        /// <summary> Question Symbol Quater View Position </summary>
        public RectTransform RectTransformQuestionSymbolQuarterViewPosition = default;
        /// <summary> Canvas Transform </summary>
        public Transform TransformCanvas = default;
        #endregion
    }

    /// <summary>
    /// Initialize
    /// </summary>
    public partial class HomeUIController : BaseUIController // Initialize
    {
        private void Allocate()
        {
            MainSystem.Instance.SceneManager.UIManager.SignUpHomeUIController(this);
        }

        public void Initialize()
        {
            MainSystem.Instance.SceneManager.UIManager.NowTransformCanvas = TransformCanvas;
        }
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class HomeUIController : BaseUIController // Main
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
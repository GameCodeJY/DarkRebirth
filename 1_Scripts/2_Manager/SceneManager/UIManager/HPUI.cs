/*
 *  Coder       :   JY
 *  Last Update :   2024. 10. 29.
 *  Information :   HP UI
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class HPUI : MonoBehaviour // Data Field
    {
        #region Serialize Field
        /// <summary> Question Symbol RectTransform </summary>
        public RectTransform rectTransformQuestionSymbol = default; // UI 이미지의 RectTransform
        /// <summary> UI Canvas </summary>
        [SerializeField] private Canvas canvasUI = default; // Screen Space - Overlay 모드의 캔버스
        #endregion

        #region Member Value
        /// <summary> Camera Manager </summary>
        private CameraManager CameraManager = default;
        /// <summary> Instance Manager </summary>
        private InstanceManager InstanceManager = default;
        #endregion
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class HPUI : MonoBehaviour // Main
    {
        private void Awake()
        {
            CameraManager = MainSystem.Instance.CameraManager;
            InstanceManager = MainSystem.Instance.InstanceManager;
        }

        void Update()
        {
            // 월드 오브젝트의 위치를 스크린 좌표로 변환
            Vector3 screenPos = CameraManager.MainCamera.WorldToScreenPoint(InstanceManager.BaseMonster.HpBar.TransformValueUI.position);

            // 스크린 좌표를 캔버스의 로컬 좌표로 변환
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasUI.transform as RectTransform, // 캔버스의 RectTransform
                screenPos,                         // 변환할 스크린 좌표
                canvasUI.worldCamera,                // 월드 카메라 (null 가능)
                out localPoint);                   // 변환된 로컬 좌표

            // 로컬 좌표를 RectTransform에 적용
            rectTransformQuestionSymbol.localPosition = localPoint;
        }
    }
}
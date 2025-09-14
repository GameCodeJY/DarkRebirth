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
        public RectTransform rectTransformQuestionSymbol = default; // UI �̹����� RectTransform
        /// <summary> UI Canvas </summary>
        [SerializeField] private Canvas canvasUI = default; // Screen Space - Overlay ����� ĵ����
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
            // ���� ������Ʈ�� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
            Vector3 screenPos = CameraManager.MainCamera.WorldToScreenPoint(InstanceManager.BaseMonster.HpBar.TransformValueUI.position);

            // ��ũ�� ��ǥ�� ĵ������ ���� ��ǥ�� ��ȯ
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasUI.transform as RectTransform, // ĵ������ RectTransform
                screenPos,                         // ��ȯ�� ��ũ�� ��ǥ
                canvasUI.worldCamera,                // ���� ī�޶� (null ����)
                out localPoint);                   // ��ȯ�� ���� ��ǥ

            // ���� ��ǥ�� RectTransform�� ����
            rectTransformQuestionSymbol.localPosition = localPoint;
        }
    }
}
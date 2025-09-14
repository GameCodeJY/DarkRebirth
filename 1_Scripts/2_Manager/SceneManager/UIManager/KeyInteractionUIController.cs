/*
 *  Coder       :   JY
 *  Last Update :   2025. 02. 27.
 *  Information :   Key Interaction UI Controller
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class KeyInteractionUIController : MonoBehaviour // Data Field
    {
        [Header("References")]
        [SerializeField] private Camera mainCamera;      // 3D 오브젝트를 실제로 렌더링하는 메인 카메라
        [SerializeField] private Camera uiCamera;        // UI 전용 카메라 (Canvas에 할당되어 있음)
        [SerializeField] private Transform targetObject; // 월드에서 추적하고 싶은 오브젝트
        [SerializeField] private Canvas mainCanvas;      // 최상위 Canvas (Render Mode: Screen Space - Camera)
        [SerializeField] private RectTransform uiElement; // 해당 UI 이미지의 RectTransform
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class KeyInteractionUIController : MonoBehaviour // Main
    {
        private void Update()
        {
            // 1) 월드 좌표를 '메인 카메라' 기준 스크린 좌표로 변환
            Vector3 screenPos = mainCamera.WorldToScreenPoint(MainSystem.Instance.PlayerManager.MainPlayer.transform.position);

            // 3) 스크린 좌표를 Canvas 로컬 좌표(anchoredPosition)로 변환
            RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();

            Vector2 localPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                screenPos,
                uiCamera,  // Canvas에 할당된 UI 카메라
                out localPos))
            {
                // 4) UI 요소의 위치 갱신
                uiElement.anchoredPosition = localPos;
            }
        }
    }
}
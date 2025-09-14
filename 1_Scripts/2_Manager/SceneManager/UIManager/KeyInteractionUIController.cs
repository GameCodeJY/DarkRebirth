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
        [SerializeField] private Camera mainCamera;      // 3D ������Ʈ�� ������ �������ϴ� ���� ī�޶�
        [SerializeField] private Camera uiCamera;        // UI ���� ī�޶� (Canvas�� �Ҵ�Ǿ� ����)
        [SerializeField] private Transform targetObject; // ���忡�� �����ϰ� ���� ������Ʈ
        [SerializeField] private Canvas mainCanvas;      // �ֻ��� Canvas (Render Mode: Screen Space - Camera)
        [SerializeField] private RectTransform uiElement; // �ش� UI �̹����� RectTransform
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class KeyInteractionUIController : MonoBehaviour // Main
    {
        private void Update()
        {
            // 1) ���� ��ǥ�� '���� ī�޶�' ���� ��ũ�� ��ǥ�� ��ȯ
            Vector3 screenPos = mainCamera.WorldToScreenPoint(MainSystem.Instance.PlayerManager.MainPlayer.transform.position);

            // 3) ��ũ�� ��ǥ�� Canvas ���� ��ǥ(anchoredPosition)�� ��ȯ
            RectTransform canvasRect = mainCanvas.GetComponent<RectTransform>();

            Vector2 localPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                screenPos,
                uiCamera,  // Canvas�� �Ҵ�� UI ī�޶�
                out localPos))
            {
                // 4) UI ����� ��ġ ����
                uiElement.anchoredPosition = localPos;
            }
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 15.
 *  Information :   Detect Base Player Trigger
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class DetectBasePlayerTrigger : MonoBehaviour // Data Field
    {
        /// <summary> OnTriggerEnter�� �ܺο� ����Ǵ� �̺�Ʈ </summary>
        [SerializeField] protected UnityEvent UnityEventTriggerEnter = default;
        /// <summary> OnTriggerExit�� �ܺο� ����Ǵ� �̺�Ʈ </summary>
        [SerializeField] protected UnityEvent UnityEventTriggerExit = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class DetectBasePlayerTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            if (CheckBasePlayerExist(other)) // Base Player�� �����Ѵٸ�
            {
                UnityEventTriggerEnter.Invoke(); // UnityEvent�� ����
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (CheckBasePlayerExist(other)) // Base Player�� �����Ѵٸ�
            {
                UnityEventTriggerExit.Invoke(); // UnityEvent�� ����
            }
        }
    }

    /// <summary>
    /// Check
    /// </summary>
    public partial class DetectBasePlayerTrigger : MonoBehaviour // Check
    {
        /// <summary>
        /// Base Player ���� üũ
        /// </summary>
        private bool CheckBasePlayerExist(Collider other)
        {
            if (other.gameObject.GetComponent<BasePlayer>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
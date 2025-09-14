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
        /// <summary> OnTriggerEnter시 외부와 연결되는 이벤트 </summary>
        [SerializeField] protected UnityEvent UnityEventTriggerEnter = default;
        /// <summary> OnTriggerExit시 외부와 연결되는 이벤트 </summary>
        [SerializeField] protected UnityEvent UnityEventTriggerExit = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class DetectBasePlayerTrigger : MonoBehaviour // Main
    {
        private void OnTriggerEnter(Collider other)
        {
            if (CheckBasePlayerExist(other)) // Base Player가 존재한다면
            {
                UnityEventTriggerEnter.Invoke(); // UnityEvent를 실행
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (CheckBasePlayerExist(other)) // Base Player가 존재한다면
            {
                UnityEventTriggerExit.Invoke(); // UnityEvent를 실행
            }
        }
    }

    /// <summary>
    /// Check
    /// </summary>
    public partial class DetectBasePlayerTrigger : MonoBehaviour // Check
    {
        /// <summary>
        /// Base Player 존재 체크
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
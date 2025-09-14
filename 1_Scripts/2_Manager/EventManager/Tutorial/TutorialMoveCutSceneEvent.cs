/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   Tutorial Move Cut Scene Event
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class TutorialMoveCutSceneEvent : MonoBehaviour // Data Field
    {
    }

    /// <summary>
    /// Signal
    /// </summary>
    public partial class TutorialMoveCutSceneEvent : MonoBehaviour // Signal
    {
        /// <summary>
        /// 움직임 튜토리얼 
        /// </summary>
        public void SendSignalEndMoveTutorial()
        {
            MainSystem.Instance.ProcessManager.EndMoveTutorial();
        }
    }
}
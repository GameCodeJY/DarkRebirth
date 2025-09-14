/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   Ʃ�丮�󿡼� ����ϴ� Ʈ���� �׷� ���ѷ�
 */

namespace MainSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Data Field
    /// </summary>
    public partial class TutorialTriggerGroupController : MonoBehaviour // Data Field
    {
        /// <summary> Ʃ�丮�� Ʈ���� ����Ʈ </summary>
        [SerializeField] private List<GameObject> ListGameObjectTutorialTrigger = default;
        /// <summary> ���� Ʃ�丮�� Ʈ���� ��ȣ </summary>
        private int NumTutorialTrigger = 0;
    }

    /// <summary>
    /// Tutorial Event
    /// </summary>
    public partial class TutorialTriggerGroupController : MonoBehaviour // Tutorial Event
    {
        /// <summary>
        /// Ʃ�丮�� Ʈ���� �ٲٱ�
        /// </summary>
        public void ChangeTutorialTrigger()
        {
            ListGameObjectTutorialTrigger[NumTutorialTrigger].SetActive(false); // ���� Ʈ���� ��Ȱ��ȭ
            NumTutorialTrigger += 1; // ���� Ʈ���� ��ȣ�� �ٲٱ�
            ListGameObjectTutorialTrigger[NumTutorialTrigger].SetActive(true); // ���� Ʈ���� Ȱ��ȭ
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   Tutorial Throw Rock Trigger
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
    public partial class TutorialThrowRockTrigger : DetectBasePlayerTrigger // Data Field
    {
        #region Const Value
        /// <summary> ���� ������ ���� ��Ʈ </summary>
        private const string TEXT_TUTORIAL_THROW_ROCK_EVENT = "������ ���Ϸ��� \'A\'�� \'D\'�� ����������.";
        #endregion

        /// <summary> Ʃ�丮�� ���� �׷� ��Ʈ�ѷ� </summary>
        [SerializeField] private TutorialRockGroupController TutorialRockGroupController = default;
        /// <summary> Ʃ�丮�� ���� Text </summary>
        [SerializeField] private Text TextTutorialExplanation = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class TutorialThrowRockTrigger : DetectBasePlayerTrigger // Main
    {
        private void Awake()
        {
            UnityEventTriggerEnter.AddListener(StartThrowRock);
        }
    }

    /// <summary>
    /// Tutorial Event
    /// </summary>
    public partial class TutorialThrowRockTrigger : DetectBasePlayerTrigger // Tutorial Event
    {
        /// <summary>
        /// ���� ������ ����
        /// </summary>
        private void StartThrowRock()
        {
            TextTutorialExplanation.text = TEXT_TUTORIAL_THROW_ROCK_EVENT;
            TutorialRockGroupController.StartDropRocks(); //���� �׷� ��Ʈ�ѷ����� ���� �������� ���
        }
    }
}
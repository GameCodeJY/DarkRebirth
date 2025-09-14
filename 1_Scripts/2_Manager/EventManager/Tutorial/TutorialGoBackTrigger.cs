/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   Tutorial Go Back Trigger
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
    public partial class TutorialGoBackTrigger : DetectBasePlayerTrigger // Data Field
    {
        #region Const Value
        /// <summary> �ڷ� ���� Ʃ�丮�� ��Ʈ </summary>
        private const string TEXT_TUTORIAL_GO_BACK_EVENT = "���������� ���� ã�ƺ�����.";
        #endregion

        /// <summary> Ʃ�丮�� ���� Text </summary>
        [SerializeField] private Text TextTutorialExplanation = default;
        /// <summary> ������ �������� </summary>
        [SerializeField] private GameObject GameObjectWood = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class TutorialGoBackTrigger : DetectBasePlayerTrigger // Main
    {
        private void Awake()
        {
            UnityEventTriggerEnter.AddListener(StartGoBackTutorial);
        }
    }

    /// <summary>
    /// Tutorial Event
    /// </summary>
    public partial class TutorialGoBackTrigger : DetectBasePlayerTrigger // Tutorial Event
    {
        /// <summary>
        /// �ڷ� ���� Ʃ�丮�� ����
        /// </summary>
        public void StartGoBackTutorial()
        {
            TextTutorialExplanation.text = TEXT_TUTORIAL_GO_BACK_EVENT;
            GameObjectWood.SetActive(false); // �� �����ִ� ���� ġ���
        }
    }
}
/*
 *  Coder       :   JY
 *  Last Update :   2025. 05. 16.
 *  Information :   Tutorial Pig Fall Cut Scene Trigger
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
    public partial class TutorialPigFallCutSceneTrigger : DetectBasePlayerTrigger // Data Field
    {
        /// <summary> Ʃ�丮�� Text GameObject </summary>
        [SerializeField] private GameObject GameObjectTutorialText = default;
        /// <summary> ���� �������� �̺�Ʈ �ƾ� GameObject </summary>
        [SerializeField] private GameObject GameObjectPigFallEventCutScene = default;
    }

    /// <summary>
    /// Main
    /// </summary>
    public partial class TutorialPigFallCutSceneTrigger : DetectBasePlayerTrigger // Main
    {
        private void Start()
        {
            UnityEventTriggerEnter.AddListener(StartPigFallCutScene);
        }
    }

    /// <summary>
    /// Tutorial Event
    /// </summary>
    public partial class TutorialPigFallCutSceneTrigger : DetectBasePlayerTrigger // Tutorial Event
    {
        /// <summary>
        /// ���� ���� �ƾ� ����
        /// </summary>
        public void StartPigFallCutScene()
        {
            GameObjectTutorialText.SetActive(false); // Ʃ�丮�� �ؽ�Ʈ off
            MainSystem.Instance.PlayerManager.MainPlayer.gameObject.SetActive(false);
            GameObjectPigFallEventCutScene.SetActive(true); // Ʃ�丮�� �̺�Ʈ �ƾ� ����
        }
    }
}
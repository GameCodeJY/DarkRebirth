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
        /// <summary> Æ©Åä¸®¾ó Text GameObject </summary>
        [SerializeField] private GameObject GameObjectTutorialText = default;
        /// <summary> µÅÁö ¶³¾îÁö´Â ÀÌº¥Æ® ÄÆ¾À GameObject </summary>
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
        /// µÅÁö ³«ÇÏ ÄÆ¾À ½ÃÀÛ
        /// </summary>
        public void StartPigFallCutScene()
        {
            GameObjectTutorialText.SetActive(false); // Æ©Åä¸®¾ó ÅØ½ºÆ® off
            MainSystem.Instance.PlayerManager.MainPlayer.gameObject.SetActive(false);
            GameObjectPigFallEventCutScene.SetActive(true); // Æ©Åä¸®¾ó ÀÌº¥Æ® ÄÆ¾À ½ÇÇà
        }
    }
}
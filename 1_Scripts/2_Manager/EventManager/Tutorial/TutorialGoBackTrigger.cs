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
        /// <summary> 뒤로 가기 튜토리얼 멘트 </summary>
        private const string TEXT_TUTORIAL_GO_BACK_EVENT = "빠져나가는 길을 찾아보세요.";
        #endregion

        /// <summary> 튜토리얼 설명 Text </summary>
        [SerializeField] private Text TextTutorialExplanation = default;
        /// <summary> 나무로 막혀있음 </summary>
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
        /// 뒤로 가기 튜토리얼 시작
        /// </summary>
        public void StartGoBackTutorial()
        {
            TextTutorialExplanation.text = TEXT_TUTORIAL_GO_BACK_EVENT;
            GameObjectWood.SetActive(false); // 길 막고있는 나무 치우기
        }
    }
}